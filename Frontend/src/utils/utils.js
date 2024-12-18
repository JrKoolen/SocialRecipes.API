const fs = require('fs');
const path = require('path');

function waitForSession(fileName, timeout = 10000, interval = 100) {
  return new Promise((resolve, reject) => {
    const startTime = Date.now();
    const filePath = path.join(process.cwd(), 'sessions', fileName); 
    const checkFile = () => {
      if (fs.existsSync(`${filePath}.json`)) {
        resolve(true); 
      } else if (Date.now() - startTime > timeout) {
        reject(new Error(`Timeout: File ${filePath} was not found within ${timeout}ms`));
      } else {
        setTimeout(checkFile, interval); 
      }
    };
    checkFile();
  });
}

module.exports = {
  waitForSession,
};
