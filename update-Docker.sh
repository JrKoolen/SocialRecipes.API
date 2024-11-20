#!/bin/bash

echo "Pulling latest changes from master branch..."
git pull origin master

PROJECT_DIR="/path/to/your/project"
cd $PROJECT_DIR || exit

echo "Rebuilding and restarting Docker containers..."
docker-compose down
docker-compose up --build -d

echo "Checking running containers..."
docker ps
