import fs from 'fs';
import lighthouse from 'lighthouse';
import * as chromeLauncher from 'chrome-launcher';

async function runLighthouse(url, options = {}, config = null) {
    let chrome;
    try {
        chrome = await chromeLauncher.launch({ chromeFlags: ['--headless'] });
        options = {
            logLevel: 'info',
            output: 'json',
            onlyCategories: ['performance'],
            port: chrome.port,
            ...options,
        };

        const runnerResult = await lighthouse(url, options, config);

        const report = runnerResult.report;
        fs.writeFileSync('lighthouse-report.json', report);
        console.log('Lighthouse report saved to lighthouse-report.json');

        const { categories, audits } = runnerResult.lhr;

        console.log('Performance score:', categories.performance.score);

        const tti = audits['interactive']?.displayValue || 'N/A';
        const cls = audits['cumulative-layout-shift']?.displayValue || 'N/A';
        const fid = audits['first-input-delay']?.displayValue || 'N/A';

        console.log('Time to Interactive:', tti);
        console.log('Cumulative Layout Shift:', cls);
        console.log('First Input Delay:', fid);

        return runnerResult.lhr;
    } catch (err) {
        console.error('Error running Lighthouse:', err.message);
        console.error(err.stack);
    } finally {
        if (chrome) await chrome.kill();
    }
}

const urlToTest = 'http://localhost:3000/recipes';
runLighthouse(urlToTest)
    .then(() => console.log('Lighthouse testing completed.'))
    .catch(err => console.error('Unhandled error:', err));
