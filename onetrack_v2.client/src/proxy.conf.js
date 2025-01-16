const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "http://localhost:7249",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
