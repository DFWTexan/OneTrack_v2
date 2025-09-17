const PROXY_CONFIG = [
  {
    context: [
      "/api/**",
    ],
    target: "https://ftapiq101/OneTrakV2",
    secure: false,
    changeOrigin: true,
    logLevel: "debug"
  }
]

module.exports = PROXY_CONFIG;