FROM node:16-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . /app

EXPOSE 3000

CMD ["node", "index.js"]

# docker build -t socialrecipesfrontend .

# docker run -p 3000:3000 socialrecipesfrontend

