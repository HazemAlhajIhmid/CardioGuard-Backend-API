# Deployment Guide ☁️

## CardioGuard Backend API - دليل النشر الشامل

### 📋 جدول المحتويات

1. [نظرة عامة](#نظرة-عامة)
2. [النشر على Azure](#النشر-على-azure)
3. [النشر على خادم Linux](#النشر-على-خادم-linux)
4. [النشر على Windows Server](#النشر-على-windows-server)
5. [النشر باستخدام Docker](#النشر-باستخدام-docker)
6. [إعدادات الإنتاج](#إعدادات-الإنتاج)
7. [CI/CD Pipeline](#cicd-pipeline)
8. [المراقبة والصيانة](#المراقبة-والصيانة)

---

## نظرة عامة

هذا الدليل يشرح كيفية نشر CardioGuard Backend API على منصات مختلفة.

### متطلبات النشر

- .NET 8.0 Runtime
- 512 MB RAM (الحد الأدنى)، يوصى بـ 1 GB
- 500 MB مساحة تخزين
- SQL Server (اختياري)
- HTTPS Certificate (للإنتاج)

---

## النشر على Azure

### الطريقة 1: Azure CLI (موصى بها) ✅

#### 1. تسجيل الدخول إلى Azure

```bash
# تسجيل الدخول
az login

# التحقق من الاشتراك
az account show
```

#### 2. إنشاء Resource Group

```bash
az group create \
  --name CardioGuard-RG \
  --location "East US"
```

#### 3. إنشاء App Service Plan

```bash
az appservice plan create \
  --name CardioGuard-Plan \
  --resource-group CardioGuard-RG \
  --sku B1 \
  --is-linux
```

#### 4. إنشاء Web App

```bash
az webapp create \
  --resource-group CardioGuard-RG \
  --plan CardioGuard-Plan \
  --name cardioguard-api \
  --runtime "DOTNET|8.0"
```

#### 5. بناء ونشر التطبيق

```bash
# من مجلد المشروع
cd HeartDiseaseAPI

# بناء المشروع للإنتاج
dotnet publish -c Release -o ./publish

# نشر إلى Azure
cd publish
zip -r ../deploy.zip .
az webapp deploy \
  --resource-group CardioGuard-RG \
  --name cardioguard-api \
  --src-path ../deploy.zip \
  --type zip
```

#### 6. تكوين CORS

```bash
az webapp cors add \
  --resource-group CardioGuard-RG \
  --name cardioguard-api \
  --allowed-origins \
    "https://master-thesis-cardio-guard-early-de.vercel.app" \
    "http://localhost:5173"
```

#### 7. تكوين SSL/HTTPS

```bash
# تفعيل HTTPS فقط
az webapp update \
  --resource-group CardioGuard-RG \
  --name cardioguard-api \
  --https-only true
```

#### 8. إعداد المتغيرات البيئية

```bash
az webapp config appsettings set \
  --resource-group CardioGuard-RG \
  --name cardioguard-api \
  --settings \
    ASPNETCORE_ENVIRONMENT=Production \
    AllowedHosts=*
```

### الطريقة 2: Azure Portal (واجهة رسومية)

#### الخطوات:

1. **تسجيل الدخول إلى Azure Portal**
   - افتح [portal.azure.com](https://portal.azure.com)
   - سجل الدخول بحسابك

2. **إنشاء Web App**
   - اضغط "Create a resource"
   - اختر "Web App"
   - املأ البيانات:
     - **Name:** cardioguard-api
     - **Runtime:** .NET 8
     - **Region:** East US
     - **Plan:** B1 (Basic)
   - اضغط "Create"

3. **نشر التطبيق**
   - افتح الـ Web App
   - اذهب إلى "Deployment Center"
   - اختر "GitHub" أو "Local Git"
   - اتبع التعليمات

4. **تكوين الإعدادات**
   - افتح "Configuration"
   - أضف CORS origins
   - فعّل HTTPS
   - احفظ التغييرات

### الطريقة 3: GitHub Actions (CI/CD) ✅

#### إعداد GitHub Actions

**الملف:** `.github/workflows/azure-deploy.yml`

```yaml
name: Deploy to Azure

on:
  push:
    branches:
      - main

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'

    - name: Restore dependencies
      run: dotnet restore
      working-directory: ./HeartDiseaseAPI

    - name: Build
      run: dotnet build --configuration Release --no-restore
      working-directory: ./HeartDiseaseAPI

    - name: Test
      run: dotnet test --no-restore --verbosity normal
      working-directory: ./HeartDiseaseAPI

    - name: Publish
      run: dotnet publish -c Release -o ./publish
      working-directory: ./HeartDiseaseAPI

    - name: Deploy to Azure Web App
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'cardioguard-api'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: './HeartDiseaseAPI/publish'
```

#### الحصول على Publish Profile:

1. في Azure Portal، افتح الـ Web App
2. اضغط "Get publish profile"
3. افتح الملف الذي تم تنزيله
4. انسخ المحتوى بالكامل
5. في GitHub، اذهب إلى Settings > Secrets
6. أضف Secret جديد:
   - Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
   - Value: (المحتوى الذي نسخته)

---

## النشر على خادم Linux

### المتطلبات:

- Ubuntu 20.04 أو أحدث
- Nginx (كـ Reverse Proxy)
- .NET 8 Runtime

### الخطوات:

#### 1. تثبيت .NET Runtime

```bash
# تحديث النظام
sudo apt update
sudo apt upgrade -y

# إضافة مستودع Microsoft
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# تثبيت .NET Runtime
sudo apt update
sudo apt install -y aspnetcore-runtime-8.0
```

#### 2. نشر التطبيق

```bash
# على جهازك المحلي
cd HeartDiseaseAPI
dotnet publish -c Release -o ./publish

# نقل الملفات إلى الخادم
scp -r ./publish user@your-server-ip:/var/www/cardioguard-api

# على الخادم
sudo chown -R www-data:www-data /var/www/cardioguard-api
sudo chmod -R 755 /var/www/cardioguard-api
```

#### 3. إنشاء Systemd Service

**الملف:** `/etc/systemd/system/cardioguard-api.service`

```ini
[Unit]
Description=CardioGuard API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/cardioguard-api
ExecStart=/usr/bin/dotnet /var/www/cardioguard-api/HeartDiseaseAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=cardioguard-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

#### 4. تفعيل وتشغيل الخدمة

```bash
# إعادة تحميل systemd
sudo systemctl daemon-reload

# تفعيل الخدمة
sudo systemctl enable cardioguard-api

# تشغيل الخدمة
sudo systemctl start cardioguard-api

# التحقق من الحالة
sudo systemctl status cardioguard-api
```

#### 5. تكوين Nginx

**الملف:** `/etc/nginx/sites-available/cardioguard-api`

```nginx
server {
    listen 80;
    server_name your-domain.com;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

#### 6. تفعيل الموقع

```bash
# إنشاء رابط رمزي
sudo ln -s /etc/nginx/sites-available/cardioguard-api /etc/nginx/sites-enabled/

# اختبار التكوين
sudo nginx -t

# إعادة تشغيل Nginx
sudo systemctl restart nginx
```

#### 7. إعداد SSL مع Let's Encrypt

```bash
# تثبيت Certbot
sudo apt install -y certbot python3-certbot-nginx

# الحصول على شهادة SSL
sudo certbot --nginx -d your-domain.com

# سيتم تجديد الشهادة تلقائياً
sudo systemctl status certbot.timer
```

---

## النشر على Windows Server

### المتطلبات:

- Windows Server 2019 أو أحدث
- IIS 10 أو أحدث
- .NET 8 Hosting Bundle

### الخطوات:

#### 1. تثبيت .NET Hosting Bundle

```powershell
# تنزيل Hosting Bundle
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/.../dotnet-hosting-8.0-win.exe" -OutFile "dotnet-hosting.exe"

# تثبيت
.\dotnet-hosting.exe /quiet /norestart

# إعادة تشغيل IIS
iisreset
```

#### 2. بناء ونشر التطبيق

```powershell
# بناء المشروع
cd HeartDiseaseAPI
dotnet publish -c Release -o C:\inetpub\cardioguard-api
```

#### 3. إنشاء Application Pool في IIS

```powershell
# استيراد WebAdministration
Import-Module WebAdministration

# إنشاء Application Pool
New-WebAppPool -Name "CardioGuardAPI"

# تكوين Application Pool
Set-ItemProperty IIS:\AppPools\CardioGuardAPI -Name managedRuntimeVersion -Value ""
Set-ItemProperty IIS:\AppPools\CardioGuardAPI -Name processModel.identityType -Value LocalSystem
```

#### 4. إنشاء Website في IIS

```powershell
# إنشاء الموقع
New-WebSite -Name "CardioGuard API" `
  -Port 80 `
  -PhysicalPath "C:\inetpub\cardioguard-api" `
  -ApplicationPool "CardioGuardAPI"

# تفعيل التصفح المجهول
Set-WebConfigurationProperty -Filter "/system.webServer/security/authentication/anonymousAuthentication" `
  -Name enabled -Value true -PSPath "IIS:\Sites\CardioGuard API"
```

#### 5. تكوين HTTPS

```powershell
# ربط شهادة SSL
New-WebBinding -Name "CardioGuard API" -Protocol https -Port 443

# تحديد الشهادة (يجب أن تكون موجودة في Certificate Store)
$cert = Get-ChildItem -Path Cert:\LocalMachine\My | Where-Object {$_.Subject -like "*your-domain.com*"}
(Get-WebBinding -Name "CardioGuard API" -Protocol https).AddSslCertificate($cert.Thumbprint, "my")
```

---

## النشر باستخدام Docker

### Dockerfile

**الملف:** `Dockerfile`

```dockerfile
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملف المشروع
COPY ["HeartDiseaseAPI.csproj", "./"]
RUN dotnet restore "HeartDiseaseAPI.csproj"

# نسخ باقي الملفات والبناء
COPY . .
RUN dotnet build "HeartDiseaseAPI.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "HeartDiseaseAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# تعريف المنفذ
EXPOSE 80
EXPOSE 443

# نقطة الدخول
ENTRYPOINT ["dotnet", "HeartDiseaseAPI.dll"]
```

### Docker Compose

**الملف:** `docker-compose.yml`

```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: cardioguard-api
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:80;https://+:443
    volumes:
      - ./Data:/app/Data
    restart: unless-stopped
    networks:
      - cardioguard-network

networks:
  cardioguard-network:
    driver: bridge
```

### بناء وتشغيل

```bash
# بناء الصورة
docker build -t cardioguard-api:latest .

# تشغيل Container
docker run -d \
  -p 5000:80 \
  -p 5001:443 \
  --name cardioguard-api \
  cardioguard-api:latest

# أو باستخدام Docker Compose
docker-compose up -d

# عرض السجلات
docker logs -f cardioguard-api

# إيقاف Container
docker stop cardioguard-api

# إعادة التشغيل
docker restart cardioguard-api
```

---

## إعدادات الإنتاج

### appsettings.Production.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "HeartDiseaseDB": "Server=your-server;Database=HeartDisease;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  },
  "Cors": {
    "AllowedOrigins": [
      "https://your-frontend-domain.com",
      "https://master-thesis-cardio-guard-early-de.vercel.app"
    ]
  }
}
```

### تكوين CORS في Program.cs

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production",
        policy =>
        {
            policy.WithOrigins(
                "https://your-frontend-domain.com",
                "https://master-thesis-cardio-guard-early-de.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// في middleware
app.UseCors("Production");
```

---

## CI/CD Pipeline

### GitHub Actions - Complete Pipeline

```yaml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

env:
  DOTNET_VERSION: '8.0.x'
  AZURE_WEBAPP_NAME: cardioguard-api

jobs:
  # Build and Test
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --configuration Release --no-restore
    
    - name: Test
      run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage"
    
    - name: Code Coverage Report
      uses: codecov/codecov-action@v3
      with:
        files: ./coverage.cobertura.xml
        flags: unittests
        name: codecov-umbrella

  # Deploy to Azure
  deploy:
    needs: build
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}
    
    - name: Publish
      run: dotnet publish -c Release -o ./publish
    
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ./publish
```

---

## المراقبة والصيانة

### مراقبة Azure Application Insights

```bash
# إضافة Application Insights
az extension add --name application-insights

# إنشاء Application Insights
az monitor app-insights component create \
  --app cardioguard-api-insights \
  --resource-group CardioGuard-RG \
  --location eastus

# ربط مع Web App
az webapp config appsettings set \
  --resource-group CardioGuard-RG \
  --name cardioguard-api \
  --settings APPLICATIONINSIGHTS_CONNECTION_STRING="[InsertConnectionString]"
```

### فحص السجلات على Linux

```bash
# عرض سجلات systemd
sudo journalctl -u cardioguard-api -f

# عرض آخر 100 سطر
sudo journalctl -u cardioguard-api -n 100

# عرض الأخطاء فقط
sudo journalctl -u cardioguard-api -p err
```

### فحص الحالة

```bash
# فحص حالة الخدمة
curl http://localhost:5000/api/prediction/health

# يجب أن يعيد:
# {"status":"healthy","timestamp":"...","version":"1.0.0"}
```

---

## استكشاف الأخطاء

### المشكلة: التطبيق لا يبدأ

**الحل:**
```bash
# التحقق من السجلات
sudo journalctl -u cardioguard-api -n 50

# التحقق من المنفذ
sudo netstat -tlnp | grep :5000

# إعادة تشغيل الخدمة
sudo systemctl restart cardioguard-api
```

### المشكلة: خطأ CORS

**الحل:**
- تأكد من إضافة Frontend URL في CORS
- تحقق من إعدادات `Program.cs`
- تأكد من استخدام HTTPS

### المشكلة: بطء في الأداء

**الحل:**
- تحقق من موارد الخادم (CPU, RAM)
- راقب استخدام الذاكرة
- قم بتحسين النماذج
- استخدم Caching

---

## الأمان

### التوصيات:

1. **استخدام HTTPS فقط في الإنتاج**
2. **تقييد CORS للنطاقات المعروفة فقط**
3. **تفعيل Rate Limiting**
4. **مراقبة السجلات بانتظام**
5. **تحديث التبعيات (NuGet Packages)**
6. **استخدام Secrets Management (Azure Key Vault)**

---

## الدعم والمساعدة

- **GitHub Issues:** [افتح Issue جديد](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- **البريد الإلكتروني:** Hazem_82763@svuonline.org

---

**آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0.0
