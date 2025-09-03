# HTTPS Development Setup

This project is configured to use HTTPS for local development to match the production API requirements.

## Quick Start

**Step 1: Start without SSL first (for testing):**
```bash
ng serve --configuration=local --ssl=false --port 4204
```

**Step 2: Test API connectivity at:**
```
http://localhost:4204
```

**Step 3: If working, enable SSL:**
```bash
ng serve --configuration=local --ssl --port 4200
```

### Alternative: Use Proxy Without Frontend SSL
```bash
# Start Angular with HTTP (proxy handles HTTPS to backend)
ng serve --configuration=local --ssl=false

# Access at: http://localhost:4200
# API calls go to: /api/* (proxied to https://localhost:5181/api/*)
```

## Manual Setup (if script fails)

### Prerequisites
- Windows 10/11
- PowerShell with execution policy allowing scripts

### Steps

1. **Download mkcert:**
   ```powershell
   Invoke-WebRequest -Uri "https://github.com/FiloSottile/mkcert/releases/latest/download/mkcert-v1.4.4-windows-amd64.exe" -OutFile "mkcert.exe"
   ```

2. **Install certificate authority:**
   ```powershell
   .\mkcert.exe -install
   ```

3. **Generate certificates:**
   ```powershell
   .\mkcert.exe -key-file ssl/server.key -cert-file ssl/server.crt localhost 127.0.0.1 ::1
   ```

4. **Start the server:**
   ```bash
   ng serve --configuration=local
   ```

## Configuration Details

- **SSL certificates:** Stored in `ssl/` directory (ignored by Git)
- **Certificate validity:** 3 months (auto-renewable)
- **Supported domains:** localhost, 127.0.0.1, ::1
- **Angular configuration:** See `angular.json` serve options

## Troubleshooting

### Browser Security Warnings
- **Chrome/Edge:** Click "Advanced" → "Proceed to localhost (unsafe)"
- **Firefox:** Click "Advanced" → "Accept the Risk and Continue"

### Certificate Expired
```powershell
.\mkcert.exe -key-file ssl/server.key -cert-file ssl/server.crt localhost 127.0.0.1 ::1
```

### Port Already in Use
```bash
ng serve --configuration=local --port 4201
```

## Alternative: Quick HTTPS (without certificates)
```bash
ng serve --ssl --configuration=local
```
This uses Angular's built-in self-signed certificates but will show security warnings.

## Team Setup

Each team member needs to run the SSL setup once:
1. Clone the repository
2. Run `.\setup-ssl.ps1`
3. Start developing with `ng serve --configuration=local`

## Security Notes

- ⚠️ **Development only:** These certificates are for local development
- 🔒 **Not for production:** Production should use proper CA-signed certificates
- 📁 **Git ignored:** SSL files are not committed to the repository
- 🔄 **Auto-renewal:** Certificates expire every 3 months and need regeneration
