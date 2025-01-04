ls -l
echo "Pulling latest changes from master branch..."
git_output=$(git pull origin master)
echo "$git_output"

if [[ "$git_output" == *"Already up to date."* ]]; then
    echo "Repository is already up to date. Continuing with Docker restart..."
else
    echo "Repository updated. Proceeding with Docker restart..."
fi

echo "Rebuilding and restarting Docker containers..."
docker-compose down
docker-compose up --build -d

echo "Checking running containers..."
docker ps

echo "Press Enter to exit..."
read -r
