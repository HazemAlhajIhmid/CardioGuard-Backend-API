# 🚀 دليل البداية السريعة - رفع Backend على GitHub

## خطوات سريعة لرفع المشروع

---

## ⚡ الخطوات (5-10 دقائق)

### 1️⃣ تنظيف المشروع (مهم!)

```powershell
# افتح PowerShell في مجلد المشروع
cd "c:\Users\hazal\Hazem\Master Thesis- CardioGuard - Early Detection of Heart Disease System\Ny mappe\heart-disease-detection\backend\HeartDiseaseAPI"

# احذف الملفات غير الضرورية
Remove-Item -Recurse -Force .\bin -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force .\obj -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force .\publish -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force .\.vs -ErrorAction SilentlyContinue
```

### 2️⃣ إنشاء مستودع على GitHub

1. اذهب إلى: https://github.com/HazemAlhajIhmid
2. اضغط **New repository**
3. املأ:
   - **Name**: `CardioGuard-Backend-API`
   - **Description**: `🏥 Backend API for CardioGuard | ASP.NET Core 8.0 + ML.NET`
   - **Public** ✅
   - **DON'T** initialize with README ❌
4. اضغط **Create repository**

### 3️⃣ رفع المشروع

```powershell
# تهيئة Git
git init

# إضافة remote
git remote add origin https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git

# إضافة الملفات
git add .

# Commit
git commit -m "🎉 Initial commit: CardioGuard Backend API v1.0.0

- ASP.NET Core 8.0 Backend API
- Three ML.NET models (KNN, Naive Bayes, Decision Tree)
- Ensemble voting system
- Swagger/OpenAPI documentation
- Comprehensive unit tests
- Azure deployment ready
- Arabic & English documentation"

# رفع
git branch -M main
git push -u origin main
```

### 4️⃣ إنشاء مستودع Hub

```powershell
# أنشئ مجلد جديد
New-Item -ItemType Directory -Path "CardioGuard-Hub"
cd CardioGuard-Hub

# انسخ محتوى HUB_README_TEMPLATE.md إلى README.md
Copy-Item "../HeartDiseaseAPI/HUB_README_TEMPLATE.md" -Destination "README.md"

# تهيئة Git
git init

# أنشئ مستودع جديد على GitHub باسم "CardioGuard-Hub"

# رفع
git add .
git commit -m "📚 Initial commit: CardioGuard Hub Documentation"
git remote add origin https://github.com/HazemAlhajIhmid/CardioGuard-Hub.git
git branch -M main
git push -u origin main
```

### 5️⃣ تحديث الروابط في المستودعات الأخرى

أضف في README كل مشروع:

```markdown
## 🔗 المشاريع المرتبطة

| المشروع | الوصف | الرابط |
|---------|-------|--------|
| 🌐 Frontend | SvelteKit Web App | [Repository](https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System) |
| 📱 Android | Kotlin Native App | [Repository](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App) |
| 🖥️ Backend | ASP.NET Core API | [Repository](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API) |
| 🏠 Hub | Main Documentation | [Repository](https://github.com/HazemAlhajIhmid/CardioGuard-Hub) |
```

---

## ✅ التحقق من النجاح

- [ ] تم إنشاء مستودع Backend
- [ ] تم رفع جميع الملفات
- [ ] تم إنشاء مستودع Hub
- [ ] تم تحديث الروابط في جميع المشاريع
- [ ] جميع الملفات README محدّثة

---

## 🎯 الخلاصة

لديك الآن:
1. ✅ **Backend API** على GitHub
2. ✅ **Hub Repository** يربط كل المشاريع
3. ✅ **توثيق شامل** لكل جزء
4. ✅ **روابط متبادلة** بين المشاريع

---

## 📞 مشاكل؟

راجع [GITHUB_SETUP.md](GITHUB_SETUP.md) للدليل المفصل.

---

**⏱️ الوقت المتوقع:** 5-10 دقائق  
**📅 التاريخ:** 8 فبراير 2026  
