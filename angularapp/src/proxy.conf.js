const PROXY_CONFIG = [
  {
    context: ["/weatherforecast", "/bank"],
    target: "https://localhost:7040",
    secure: false,
  },
];

module.exports = PROXY_CONFIG;
