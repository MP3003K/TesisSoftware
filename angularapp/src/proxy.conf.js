
const PROXY_CONFIG = [
  {
    context: [
      "/unidad",
      "/preguntasPsicologicas",
      "/aula",
      "/usuario",
      "/respuestasPsicologicas",
      "/estudiante"
    ],
    target: "https://localhost:7040",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
