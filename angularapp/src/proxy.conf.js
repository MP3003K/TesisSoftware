
const PROXY_CONFIG = [
  {
    context: [
      "/unidad",
      "/preguntasPsicologicas",
      "/weatherforecast",
      "/bank",
    ],
    target: "https://localhost:7040",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
