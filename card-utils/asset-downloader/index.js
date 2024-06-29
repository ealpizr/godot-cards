const fs = require("fs");
const fsPromises = require("fs").promises;
const path = require("path");

const jsonFilePath = "urls.json";

async function readUrls(filePath) {
  const data = await fsPromises.readFile(filePath, "utf-8");
  return JSON.parse(data);
}

const downloadDir = path.join(__dirname, "download");

async function ensureDownloadDir(directory) {
  if (!fs.existsSync(directory)) {
    await fsPromises.mkdir(directory);
  }
}

async function downloadImage(url, downloadPath) {
  const response = await fetch(url);
  const arrayBuffer = await response.arrayBuffer();
  const buffer = Buffer.from(arrayBuffer);
  await fsPromises.writeFile(downloadPath, buffer);
}

(async () => {
  try {
    const urls = await readUrls(jsonFilePath);
    await ensureDownloadDir(downloadDir);

    for (let i = 0; i < urls.length; i++) {
      const url = urls[i];
      const fileName = `image_${i}.png`;
      const downloadPath = path.join(downloadDir, fileName);

      console.log(`Downloading ${url} to ${downloadPath}`);
      await downloadImage(url, downloadPath);
      console.log(`Downloaded ${url} to ${downloadPath}`);
    }

    console.log("All card assets downloaded successfully.");
  } catch (error) {
    console.error("Error downloading card assets:", error);
  }
})();
