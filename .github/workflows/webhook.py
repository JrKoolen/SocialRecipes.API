import requests
import time
import json
import os

REPO_OWNER = "JrKoolen"
REPO_NAME = "SocialRecipes.API"
WORKFLOW_FILE_NAME = "CD%20%26%20CD%20pipeline.yml" 
STATE_FILE = "last_commit.json" 
PENDING_FILE = "pending_commits.json"  
CHECK_INTERVAL = 5 * 60  


def get_latest_workflow_status():
    url = f"https://api.github.com/repos/{REPO_OWNER}/{REPO_NAME}/actions/workflows/{WORKFLOW_FILE_NAME}/runs"
    response = requests.get(url)
    
    if response.status_code == 200:
        data = response.json()
        if "workflow_runs" in data and len(data["workflow_runs"]) > 0:
            latest_run = data["workflow_runs"][0]
            commit_hash = latest_run["head_sha"]
            status = latest_run["status"]
            conclusion = latest_run["conclusion"]
            html_url = latest_run["html_url"]

            if is_new_commit(commit_hash):
                if status == "in_progress":
                    save_pending_commit(commit_hash)
                    print(f"Workflow Run In Progress:")
                    print(f"  Commit: {commit_hash}")
                    print(f"  Status: {status}")
                    print(f"  Details: {html_url}")
                elif status == "completed":
                    save_commit(commit_hash)
                    print(f"New Workflow Run Detected:")
                    print(f"  Commit: {commit_hash}")
                    print(f"  Status: {status}")
                    print(f"  Conclusion: {conclusion}")
                    print(f"  Details: {html_url}")
            else:
                print("No new workflow runs detected.")
        else:
            print("No workflow runs found.")
    else:
        print(f"Failed to fetch workflow runs: {response.status_code} {response.text}")


def is_new_commit(commit_hash):
    if os.path.exists(STATE_FILE):
        with open(STATE_FILE, "r") as file:
            data = json.load(file)
            if data.get("last_commit") == commit_hash:
                return False

    if os.path.exists(PENDING_FILE):
        with open(PENDING_FILE, "r") as file:
            pending = json.load(file)
            if commit_hash in pending.get("pending_commits", []):
                return False

    return True


def save_commit(commit_hash):
    with open(STATE_FILE, "w") as file:
        json.dump({"last_commit": commit_hash}, file)


def save_pending_commit(commit_hash):
    if os.path.exists(PENDING_FILE):
        with open(PENDING_FILE, "r") as file:
            pending = json.load(file)
    else:
        pending = {"pending_commits": []}

    if commit_hash not in pending["pending_commits"]:
        pending["pending_commits"].append(commit_hash)

    with open(PENDING_FILE, "w") as file:
        json.dump(pending, file)


def check_pending_commits():
    if not os.path.exists(PENDING_FILE):
        return

    with open(PENDING_FILE, "r") as file:
        pending = json.load(file)

    updated_pending = []
    for commit in pending.get("pending_commits", []):
        url = f"https://api.github.com/repos/{REPO_OWNER}/{REPO_NAME}/actions/runs"
        response = requests.get(url, params={"head_sha": commit})

        if response.status_code == 200:
            data = response.json()
            if "workflow_runs" in data and len(data["workflow_runs"]) > 0:
                run = data["workflow_runs"][0]
                status = run["status"]
                conclusion = run["conclusion"]
                html_url = run["html_url"]

                if status == "completed":
                    save_commit(commit)
                    print(f"Pending Workflow Completed:")
                    print(f"  Commit: {commit}")
                    print(f"  Status: {status}")
                    print(f"  Conclusion: {conclusion}")
                    print(f"  Details: {html_url}")
                else:
                    updated_pending.append(commit)
            else:
                updated_pending.append(commit)  
        else:
            updated_pending.append(commit)  

    with open(PENDING_FILE, "w") as file:
        json.dump({"pending_commits": updated_pending}, file)


if __name__ == "__main__":
    while True:
        get_latest_workflow_status()
        check_pending_commits()
        time.sleep(CHECK_INTERVAL)
