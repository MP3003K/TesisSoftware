
const PROXY_CONFIG = [
  {
    context: [
      "/unidad",
      "/preguntasPsicologicas",
      "/aula",
      "/weatherforecast",
      "/bank",
      "/usuario",
      "/respuestasPsicologicas"
    ],
    target: "https://localhost:7040",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
