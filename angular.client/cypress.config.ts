import { defineConfig } from "cypress";

export default defineConfig({
    e2e: {
        baseUrl: 'http://26.97.164.113/',
        setupNodeEvents(on, config) {
            // implement node event listeners here
        },
    },
});
