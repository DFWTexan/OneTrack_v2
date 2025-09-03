# SSL Setup Script for OneTrack v2 Client
# This script sets up HTTPS certificates for local development

Write-Host "Setting up SSL certificates for OneTrack v2 development..." -ForegroundColor Green

# Create ssl directory if it doesn't exist
if (!(Test-Path "ssl")) {
    New-Item -ItemType Directory -Name "ssl"
    Write-Host "Created ssl directory" -ForegroundColor Yellow
}

# Download mkcert if it doesn't exist
if (!(Test-Path "mkcert.exe")) {
    Write-Host "Downloading mkcert..." -ForegroundColor Yellow
    Invoke-WebRequest -Uri "https://github.com/FiloSottile/mkcert/releases/latest/download/mkcert-v1.4.4-windows-amd64.exe" -OutFile "mkcert.exe"
    Write-Host "mkcert downloaded successfully" -ForegroundColor Green
}

# Install certificate authority
Write-Host "Installing certificate authority..." -ForegroundColor Yellow
.\mkcert.exe -install

# Generate certificates
Write-Host "Generating SSL certificates..." -ForegroundColor Yellow
.\mkcert.exe -key-file ssl/server.key -cert-file ssl/server.crt localhost 127.0.0.1 ::1

Write-Host "SSL setup complete! 🎉" -ForegroundColor Green
Write-Host "You can now run: ng serve --configuration=local" -ForegroundColor Cyan
Write-Host "Your app will be available at: https://localhost:4200" -ForegroundColor Cyan
