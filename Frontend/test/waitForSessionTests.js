const { waitForSession } = require('../src/utils/utils');
const fs = require('fs');
const path = require('path');

QUnit.module('waitForSession Tests', (hooks) => {
    hooks.beforeEach(() => {
        this.originalExistsSync = fs.existsSync;
    });

    hooks.afterEach(() => {
        fs.existsSync = this.originalExistsSync;
    });

    QUnit.test('should resolve when the session file exists', async (assert) => {
        assert.expect(1);
        const done = assert.async();

        //act
        fs.existsSync = (filePath) => filePath === path.join(process.cwd(), 'sessions', 'test-session.json');

        const result = await waitForSession('test-session');
        assert.ok(result, 'Resolved successfully');
        done();
    });

    QUnit.test('should reject when the session file does not exist', async (assert) => {
        assert.expect(1);
        const done = assert.async();

        fs.existsSync = () => false;

        try {
            await waitForSession('test-session', 1000, 100);
        } catch (error) {
            assert.ok(error instanceof Error, 'Rejected when the file does not exist');
            done();
        }
    });
});
