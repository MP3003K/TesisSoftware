/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'regal-blue': '#243c5a',
        'h1': '#1E293B',
        'h1.2': '#6366F1',
      }
    },
  },
  plugins: [require('@tailwindcss/typography'),],
}

