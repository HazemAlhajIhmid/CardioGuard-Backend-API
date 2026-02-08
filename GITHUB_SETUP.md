# 🚀 دليل رفع المشروع على GitHub

## إعداد Backend API على GitHub

هذا الدليل يشرح خطوة بخطوة كيفية رفع الـ Backend API إلى GitHub وربطه مع المشاريع الأخرى.

---

## 📋 جدول المحتويات

1. [التحضير الأولي](#-التحضير-الأولي)
2. [إنشاء المستودع](#-إنشاء-المستودع-على-github)
3. [رفع الملفات](#-رفع-الملفات-إلى-github)
4. [ربط المستودعات](#-ربط-المستودعات-الثلاثة)
5. [إعداد GitHub Actions](#-إعداد-github-actions-للـ-cicd)
6. [التوثيق والـ README](#-تحديث-التوثيق)

---

## ✅ التحضير الأولي

### 1. تثبيت Git (إذا لم يكن مثبتاً)

```powershell
# تحقق من تثبيت Git
git --version

# إذا لم يكن مثبتاً، حمّله من: https://git-scm.com/
```

### 2. إعداد Git (أول مرة فقط)

```powershell
# ضع اسمك
git config --global user.name "Hazem Al Haj Ihmid"

# ضع بريدك الإلكتروني
git config --global user.email "Hazem_82763@svuonline.org"

# اختياري: ضع المحرر المفضل
git config --global core.editor "code --wait"
```

### 3. تنظيف المشروع (مهم جداً!)

قبل رفع المشروع، يجب حذف الملفات غير الضرورية:

```powershell
# انتقل إلى مجلد المشروع
cd "c:\Users\hazal\Hazem\Master Thesis- CardioGuard - Early Detection of Heart Disease System\Ny mappe\heart-disease-detection\backend\HeartDiseaseAPI"

# احذف مجلد bin
Remove-Item -Recurse -Force .\bin -ErrorAction SilentlyContinue

# احذف مجلد obj
Remove-Item -Recurse -Force .\obj -ErrorAction SilentlyContinue

# احذف مجلد publish (المنشور)
Remove-Item -Recurse -Force .\publish -ErrorAction SilentlyContinue

# احذف أي ملفات مؤقتة أخرى
Remove-Item -Recurse -Force .\.vs -ErrorAction SilentlyContinue
```

---

## 🌐 إنشاء المستودع على GitHub

### 1. إنشاء مستودع جديد

1. اذهب إلى: https://github.com/HazemAlhajIhmid
2. اضغط على **"New"** أو **"New repository"**
3. املأ المعلومات:
   - **Repository name**: `CardioGuard-Backend-API`
   - **Description**: `🏥 Backend API for CardioGuard Heart Disease Detection System | ASP.NET Core 8.0 + ML.NET`
   - **Public** ✅ (للمشاريع العامة والأكاديمية)
   - **DON'T** check "Initialize with README" ❌ (لأن لديك ملف README جاهز)
4. اضغط **"Create repository"**

---

## 📤 رفع الملفات إلى GitHub

### 1. إنشاء ملف .gitignore

أولاً، انشئ ملف `.gitignore` لتجنب رفع ملفات غير ضرورية:

```powershell
# انشئ ملف .gitignore
@"
# Build results
bin/
obj/
publish/
out/

# Visual Studio
.vs/
*.user
*.suo
*.userosscache
*.sln.docstates

# .NET Core
project.lock.json
project.fragment.lock.json
artifacts/

# Test results
TestResults/
[Tt]est[Rr]esult*/
*.trx

# Logs
logs/
*.log

# Database
*.db
*.db-shm
*.db-wal

# OS files
.DS_Store
Thumbs.db

# Azure
*.azurePubxml
*.pubxml
*.publishproj

# VS Code
.vscode/

# NuGet
*.nupkg
*.snupkg
.nuget/
"@ | Out-File -FilePath .gitignore -Encoding utf8
```

### 2. تهيئة Git في المشروع

```powershell
# انتقل إلى مجلد المشروع
cd "c:\Users\hazal\Hazem\Master Thesis- CardioGuard - Early Detection of Heart Disease System\Ny mappe\heart-disease-detection\backend\HeartDiseaseAPI"

# ابدأ Git repository
git init

# أضف الـ remote (ضع رابط المستودع الخاص بك)
git remote add origin https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git
```

### 3. إضافة الملفات وعمل Commit

```powershell
# أضف جميع الملفات
git add .

# افحص الملفات المضافة
git status

# اعمل commit
git commit -m "🎉 Initial commit: CardioGuard Backend API v1.0.0

- ASP.NET Core 8.0 Backend API
- Three ML.NET models (KNN, Naive Bayes, Decision Tree)
- Ensemble voting system
- Swagger/OpenAPI documentation
- Comprehensive unit tests
- Azure deployment ready
- Arabic & English documentation"
```

### 4. رفع الملفات إلى GitHub

```powershell
# تأكد من اسم الـ branch الرئيسي
git branch -M main

# ارفع الملفات
git push -u origin main
```

---

## 🔗 ربط المستودعات الثلاثة

الآن بعد رفع الـ Backend، يجب ربط المشاريع الثلاثة معاً.

### استراتيجية الربط

نظراً لأنك لا تستطيع جمع المشاريع الثلاثة في مستودع واحد (بسبب قيود فيركل للـ Frontend)، سنستخدم استراتيجية **Hub & Spoke**:

```
        ┌─────────────────────┐
        │   CardioGuard Hub   │ ← المستودع الرئيسي
        │   (Main Project)    │
        └─────────┬───────────┘
                  │
        ┌─────────┼─────────┐
        │         │         │
    ┌───▼───┐ ┌──▼───┐ ┌───▼───┐
    │Frontend│ │Backend│ │Android│
    │Web App │ │ API  │ │  App  │
    └────────┘ └──────┘ └───────┘
```

### 1. إنشاء المستودع الرئيسي (Hub)

أنشئ مستودع جديد يربط المشاريع الثلاثة:

```
Repository: CardioGuard-Hub
Description: 🏥 CardioGuard - Heart Disease Detection System | Master Thesis Project
Type: Public
```

### 2. محتويات مستودع Hub

سيحتوي على:
- `README.md` - شامل يشرح المشروع بالكامل
- `ARCHITECTURE.md` - معمارية النظام
- `DOCUMENTATION.md` - روابط للتوثيق
- `SCREENSHOTS/` - صور من التطبيق
- `RESEARCH/` - ملفات البحث (إن أمكن)

---

## 🔄 إعداد GitHub Actions للـ CI/CD

### إنشاء workflow للنشر على Azure

أنشئ ملف `.github/workflows/azure-deploy.yml`:

```powershell
# أنشئ المجلدات
New-Item -ItemType Directory -Path ".github\workflows" -Force

# أنشئ ملف الـ workflow
@"
name: Deploy to Azure

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Test
      run: dotnet test --no-build --configuration Release --verbosity normal
  
  deploy:
    needs: build-and-test
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Publish
      run: dotnet publish -c Release -o ./publish
    
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'cardioguard-api'
        publish-profile: \${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ./publish
"@ | Out-File -FilePath .github/workflows/azure-deploy.yml -Encoding utf8
```

### إضافة Azure secrets

1. اذهب إلى Azure Portal
2. احصل على Publishing Profile
3. في GitHub: **Settings** → **Secrets and variables** → **Actions**
4. أضف secret جديد: `AZURE_WEBAPP_PUBLISH_PROFILE`

---

## 📝 تحديث التوثيق

### 1. تحديث README.md

تأكد من أن README يحتوي على:

```markdown
### 🔗 المشاريع المرتبطة

- 🌐 [Frontend Web App](https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System)
- 📱 [Android App](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App)
- 🖥️ [Backend API](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API) (هذا المشروع)
- 🏠 [Project Hub](https://github.com/HazemAlhajIhmid/CardioGuard-Hub) (المستودع الرئيسي)
```

### 2. إضافة Badges

أضف badges في أعلى README:

```markdown
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge)
![Build](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/workflows/Deploy%20to%20Azure/badge.svg)
![Azure](https://img.shields.io/badge/Azure-Deployed-0078D4?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)
```

---

## ✅ الخطوات النهائية

### 1. تحديث جميع المستودعات بالروابط

في كل مستودع (Frontend, Backend, Android):

**أضف في README.md قسم "المشاريع المرتبطة":**

```markdown
## 🔗 المشاريع المرتبطة

يعمل هذا المشروع ضمن نظام CardioGuard المتكامل:

| المشروع | الوصف | الرابط | الحالة |
|---------|-------|--------|--------|
| 🌐 **Frontend** | تطبيق ويب - SvelteKit | [Repository](link) | ✅ Live |
| 📱 **Android** | تطبيق أصلي - Kotlin | [Repository](link) | ✅ Live |
| 🖥️ **Backend** | API - ASP.NET Core | [Repository](link) | ✅ Live |
| 🏠 **Hub** | المستودع الرئيسي | [Repository](link) | 📚 Docs |
```

### 2. إنشاء مستودع Hub

```powershell
# أنشئ مجلد جديد للـ Hub
New-Item -ItemType Directory -Path "CardioGuard-Hub"
cd CardioGuard-Hub

# ابدأ Git
git init

# أنشئ README شامل (سنعطيك المحتوى)
# (انظر HUB_README_TEMPLATE.md)

# Commit وارفع
git add .
git commit -m "📚 Initial commit: CardioGuard Hub Documentation"
git branch -M main
git remote add origin https://github.com/HazemAlhajIhmid/CardioGuard-Hub.git
git push -u origin main
```

---

## 🎯 المخطط النهائي للمستودعات

```
GitHub.com/HazemAlhajIhmid/
│
├── 📦 CardioGuard-Hub (NEW)
│   └── المستودع الرئيسي - توثيق شامل
│
├── 🌐 Master-Thesis--CardioGuard--...
│   └── Frontend - SvelteKit
│   └── مُنشر على Vercel
│
├── 📱 CardioGuard-Android-App
│   └── تطبيق Android - Kotlin
│   └── مُنشر على Play Store
│
└── 🖥️ CardioGuard-Backend-API (NEW)
    └── Backend API - ASP.NET Core
    └── مُنشر على Azure
```

---

## 🔍 فحص وتصحيح المشاكل الشائعة

### المشكلة: "Permission denied"

```powershell
# حل: استخدم HTTPS بدل SSH
git remote set-url origin https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git
```

### المشكلة: "Large files"

```powershell
# حل: تأكد من .gitignore صحيح ولم ترفع bin/obj
git rm -r --cached bin obj publish
git commit -m "Remove build artifacts"
git push
```

### المشكلة: "Failed to push"

```powershell
# حل: اسحب التغييرات أولاً
git pull origin main --rebase
git push origin main
```

---

## 📞 الدعم

إذا واجهت أي مشكلة:

1. راجع هذا الدليل مرة أخرى
2. ابحث في GitHub Docs: https://docs.github.com
3. افتح Issue في المستودع
4. تواصل عبر: Hazem_82763@svuonline.org

---

## ✅ قائمة التحقق النهائية

- [ ] تم إنشاء مستودع Backend على GitHub
- [ ] تم رفع جميع الملفات بنجاح
- [ ] .gitignore موجود وصحيح
- [ ] README محدّث بالروابط
- [ ] GitHub Actions مُعد ويعمل
- [ ] تم إنشاء مستودع Hub
- [ ] جميع المستودعات مربوطة ببعضها
- [ ] التوثيق كامل وواضح

---

**التاريخ:** 8 فبراير 2026  
**الإصدار:** 1.0  
**الحالة:** ✅ جاهز للتنفيذ

