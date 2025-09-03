const PROXY_CONFIG = [
  {
    context: [
      "/api/**",
    ],
    target: "https://ftapid101/OneTrakV2",
    secure: false,
    changeOrigin: true,
    logLevel: "debug"
  }
]

module.exports = PROXY_CONFIG;