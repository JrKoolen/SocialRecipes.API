import lighthouse from 'lighthouse';
import * as chromeLauncher from 'chrome-launcher';

async function runLighthouse(url, options = {}, config = null) {
    const chrome = await chromeLauncher.launch({ chromeFlags: ['--headless'] });
    options = {
        logLevel: 'info',
        output: 'json',
        onlyCategories: ['performance'], 
        port: chrome.port,
        ...options,
    };

    const runnerResult = await lighthouse(url, options, config);

    const report = runnerResult.report;
    console.log(report);

    const { categories, audits } = runnerResult.lhr;

    console.log('Performance score:', categories.performance.score);

    const tti = audits['interactive']?.displayValue || 'N/A';
    const cls = audits['cumulative-layout-shift']?.displayValue || 'N/A';
    const fid = audits['first-input-delay']?.displayValue || 'N/A';

    console.log('Time to Interactive:', tti);
    console.log('Cumulative Layout Shift:', cls);
    console.log('First Input Delay:', fid);

    await chrome.kill();

    return runnerResult.lhr;
}

const urlToTest = 'http://localhost:3000/recipes';
runLighthouse(urlToTest)
    .then(() => console.log('Lighthouse testing completed.'))
    .catch(err => console.error('Error running Lighthouse:', err));
