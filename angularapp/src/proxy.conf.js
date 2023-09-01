const PROXY_CONFIG = [
    {
        context: [
            "/weatherforecast",
            "/usuario",
            "/unidad",
            "/respuestasPsicologicas",
            "/preguntasPsicologicas",
            "/estudiante",
            "/aula",
        ],
        target: "http://localhost:5000",
        secure: false,
    },
];

module.exports = PROXY_CONFIG;
