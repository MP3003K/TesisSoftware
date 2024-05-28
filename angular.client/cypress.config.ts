import { defineConfig } from "cypress";

export default defineConfig({
    e2e: {
        baseUrl: 'https://127.0.0.1:4200/',
        setupNodeEvents(on, config) {
            // implement node event listeners here
        },
    },
});
