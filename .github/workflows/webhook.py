import requests
import subprocess
import time
import json
import os

REPO_OWNER = "JrKoolen"
REPO_NAME = "SocialRecipes.API"
WORKFLOW_FILE_NAME = "CD%20%26%20CD%20pipeline.yml" 
STATE_FILE = "last_commit.json"  
CHECK_INTERVAL = 10 * 60  

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
                    print(f"Workflow is still in progress for commit {commit_hash}. Checking again later.")
                else:
                    save_commit(commit_hash)
                    print(f"New Workflow Run Detected:")
                    print(f"Commit: {commit_hash}")
                    print(f"Status: {status}")
                    print(f"Conclusion: {conclusion}")
                    print(f"Details: {html_url}")

                    if status == "completed" and conclusion == "success":
                        execute_shell_file()
            else:
                print(f"No new workflow runs detected. {time.strftime('%Y-%m-%d %H:%M:%S')}, {data.get('last_commit')}")
        else:
            print("No workflow runs found.")
    else:
        print(f"Failed to fetch workflow runs: {response.status_code} {response.text}")

def is_new_commit(commit_hash):
    if not os.path.exists(STATE_FILE):
        return True  

    with open(STATE_FILE, "r") as file:
        data = json.load(file)
        return data.get("last_commit") != commit_hash

def save_commit(commit_hash):
    with open(STATE_FILE, "w") as file:
        json.dump({"last_commit": commit_hash}, file)

def execute_shell_file():
    script_path = r"C:\Users\Jeroe\Documents\GitHub\SocialRecipes.API\update-Docker.sh"
    if not os.path.exists(script_path):
        print(f"Shell script not found at: {script_path}")
        return

    bash_path = r"C:\Program Files\Git\bin\bash.exe"  
    if not os.path.exists(bash_path):
        print(f"Bash executable not found at: {bash_path}")
        return

    try:
        subprocess.run([bash_path, script_path], check=True)
        print("Shell script executed successfully.")
    except subprocess.CalledProcessError as e:
        print(f"Error executing shell script: {e}")

if __name__ == "__main__":
    while True:
        get_latest_workflow_status()
        time.sleep(CHECK_INTERVAL)
