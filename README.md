<div align="center">

# 🏥 CardioGuard Backend API

### واجهة برمجية متقدمة للكشف المبكر عن أمراض القلب
### Advanced REST API for Heart Disease Early Detection

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![ML.NET](https://img.shields.io/badge/ML.NET-5.0-blue?style=for-the-badge)](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)
[![Azure](https://img.shields.io/badge/Azure-Deployed-0078D4?style=for-the-badge&logo=microsoft-azure)](https://azure.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)

**[📱 Android App](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App)** • 
**[🌐 Web Frontend](https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System)** • 
**[📖 Documentation](#-التوثيق)** • 
**[🚀 Live Demo](https://cardioguard-api.azurewebsites.net/swagger)**

</div>

---

## 📋 جدول المحتويات

- [نظرة عامة](#-نظرة-عامة)
- [المشاريع المرتبطة](#-المشاريع-المرتبطة)
- [المميزات الرئيسية](#-المميزات-الرئيسية)
- [نماذج التعلم الآلي](#-نماذج-التعلم-الآلي)
- [متطلبات النظام](#-متطلبات-النظام)
- [التقنيات المستخدمة](#-التقنيات-المستخدمة)
- [البنية المعمارية](#-البنية-المعمارية)
- [كيفية البناء والتشغيل](#-كيفية-البناء-والتشغيل)
- [الاختبار](#-الاختبار)
- [النشر على Azure](#-النشر-على-azure)
- [API Endpoints](#-api-endpoints-الرئيسية)
- [التوثيق](#-التوثيق)
- [المساهمة](#-المساهمة)
- [الترخيص](#-الترخيص)
- [التواصل](#-التواصل)

---

## 🎯 نظرة عامة

**CardioGuard API** هي واجهة برمجية RESTful متقدمة مبنية بتقنية **ASP.NET Core 8.0** تستخدم **ML.NET** للتنبؤ بمخاطر الإصابة بأمراض القلب. تعتمد الواجهة على **ثلاثة نماذج للتعلم الآلي** (KNN, Naive Bayes, Decision Tree) مع نظام **Ensemble** يجمع نتائج النماذج لتقديم تقييم دقيق وشامل.

### 🌟 لماذا CardioGuard؟

- ✅ **دقة عالية**: 85% دقة مع نظام Ensemble
- ⚡ **سرعة فائقة**: استجابة أقل من 100ms
- 🔒 **الخصوصية**: لا يتم تخزين بيانات المستخدمين
- 📊 **ثلاثة نماذج AI**: تقييم شامل ومتعدد الأبعاد
- 🌐 **جاهز للإنتاج**: مُنشر على Azure ويعمل بكفاءة

---

## 🔗 المشاريع المرتبطة

يعمل هذا الـ Backend ضمن نظام CardioGuard المتكامل:

<div align="center">

| المشروع | التقنية | الرابط | الحالة |
|:-------:|:-------:|:------:|:------:|
| 🏠 Hub | Documentation | **[CardioGuard-Hub](https://github.com/HazemAlhajIhmid/CardioGuard-Hub)** | 📚 Docs |
| 🌐 Frontend | SvelteKit | **[Web App](https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System)** | ✅ Live |
| 🖥️ Backend | ASP.NET Core | **[Backend API](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API)** | ✅ Live |
| 📱 Android | Kotlin | **[Android App](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App)** | ✅ Live |

**🌐 Live Demos:**
- Frontend: [heart-disease-detection.vercel.app](https://master-thesis-cardio-guard-early-de.vercel.app/)
- Backend API: [cardioguard-api.azurewebsites.net/swagger](https://cardioguard-api.azurewebsites.net/swagger)
- Android APK: [Download v1.2.1](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App/releases/tag/V1.2.1)

</div>

---

## ✨ المميزات الرئيسية

CardioGuard API هي واجهة برمجية RESTful مبنية بتقنية ASP.NET Core 8.0 تستخدم ML.NET للتنبؤ بمخاطر الإصابة بأمراض القلب. تعتمد الواجهة على ثلاثة نماذج للتعلم الآلي لتقديم تقييم دقيق وشامل.

<div align="center">

| 🤖 **AI Models** | 📊 **Ensemble System** | 🔌 **RESTful** | ⚡ **< 100ms** |
|:---:|:---:|:---:|:---:|
| 3 نماذج تعلم آلي | يجمع النتائج | معمارية نظيفة | استجابة فورية |

| 📝 **Swagger UI** | 🔒 **آمن** | 🌐 **CORS** | 🧪 **مُختبر** |
|:---:|:---:|:---:|:---:|
| توثيق تفاعلي | حماية البيانات | دعم شامل | Unit Tests |

</div>

### 🎯 المميزات بالتفصيل

#### 🤖 نماذج الذكاء الاصطناعي
- **KNN (K-Nearest Neighbors)**: الأفضل للكشف المبكر مع Recall عالي (94%)
- **Naive Bayes**: أداء متوازن وسريع جداً
- **Decision Tree**: سهل التفسير والفهم
- **Ensemble Voting**: يجمع النماذج الثلاثة لأعلى دقة

#### 🔌 واجهة برمجية احترافية
- **RESTful API**: تصميم معياري وسهل الاستخدام
- **JSON Format**: تنسيق بيانات موحد
- **HTTP/HTTPS**: بروتوكولات آمنة
- **CORS Support**: دعم للاتصال من أي تطبيق

#### 📝 التوثيق والاختبار
- **Swagger/OpenAPI**: واجهة تفاعلية لاختبار الـ API
- **Unit Tests**: اختبارات شاملة لجميع المكونات
- **Integration Tests**: اختبار التكامل بين الخدمات
- **Code Coverage**: تغطية اختبارية عالية

#### ⚡ الأداء والأمان
- **سرعة عالية**: استجابة أقل من 100ms
- **Caching**: تخزين مؤقت للنتائج المتكررة
- **لا تخزين**: الخصوصية الكاملة للمستخدمين
- **Input Validation**: تحقق صارم من البيانات المُدخلة

#### ☁️ جاهز للإنتاج
- **Azure Deployment**: منشور على Azure App Service
- **CI/CD Pipeline**: نشر تلقائي عبر GitHub Actions
- **Health Checks**: مراقبة صحة التطبيق
- **Logging**: تسجيل شامل للأحداث

---

## 🧠 نماذج التعلم الآلي


<div align="center">

| 🎯 **النموذج** | 📊 **الدقة** | 🎪 **المميزات الرئيسية** | 📈 **الأداء** |
|:---:|:---:|:---|:---:|
| **K-Nearest Neighbors** | **82%** | 🔍 الأفضل للكشف المبكر<br/>📊 Recall: 94%<br/>⚡ سريع ودقيق | ⭐⭐⭐⭐⭐ |
| **Naive Bayes** | **82%** | ⚖️ أداء متوازن<br/>📊 F1-Score: 0.82<br/>🚀 الأسرع في التنفيذ | ⭐⭐⭐⭐⭐ |
| **Decision Tree** | **70%** | 📖 سهل التفسير<br/>📊 Precision: 65%<br/>🎯 واضح ومباشر | ⭐⭐⭐⭐ |
| **🏆 Ensemble** | **85%** | ✨ يجمع النماذج الثلاثة<br/>🎯 أعلى دقة<br/>🛡️ الأكثر موثوقية | ⭐⭐⭐⭐⭐ |

</div>

### 📊 شرح النماذج

#### 1️⃣ KNN (K-Nearest Neighbors)
```
🎯 الأفضل للكشف المبكر
📊 Accuracy: 82% | Recall: 94% | Precision: 75%
💡 يعتمد على أقرب النقاط المجاورة في البيانات المدربة
✅ الأفضل لتجنب الإيجابيات الكاذبة (False Negatives)
```

#### 2️⃣ Naive Bayes
```
⚖️ الأكثر توازناً
📊 Accuracy: 82% | F1-Score: 0.82 | ROC-AUC: 0.89
💡 يستخدم نظرية الاحتمالات البايزية
✅ سريع جداً في التنفيذ وموثوق
```

#### 3️⃣ Decision Tree
```
📖 الأسهل في الفهم
📊 Accuracy: 70% | Precision: 65%
💡 شجرة قرارات واضحة ومفهومة
✅ مفيد لفهم العوامل المؤثرة
```

#### 🏆 Ensemble Voting System
```
✨ الأفضل على الإطلاق
📊 Accuracy: 85% | Combined Performance
💡 يجمع تصويتات النماذج الثلاثة
✅ الأكثر دقة وموثوقية
```

---

## 📋 متطلبات النظام

<div align="center">

| 💻 **المتطلب** | 📦 **الإصدار** | 🔗 **الرابط** |
|:---:|:---:|:---:|
| .NET SDK | 8.0+ | [تحميل](https://dotnet.microsoft.com/download) |
| C# | 12.0+ | مُضمّن مع .NET 8 |
| نظام التشغيل | Windows/Linux/macOS | أي نظام يدعم .NET 8 |
| الذاكرة (RAM) | 512MB+ | يُفضل 1GB+ |
| المساحة | 200MB | للتطبيق وقاعدة البيانات |

</div>

### متطلبات اختيارية:
- **Visual Studio 2022** أو **VS Code** للتطوير
- **SQL Server** أو **SQL Server Express** (اختياري للإحصائيات)
- **Postman** لاختبار الـ API (Swagger متوفر مُدمج)
- **Git** لإدارة النسخ

---

## 🛠️ التقنيات المستخدمة

<div align="center">

### Backend & Framework
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Machine Learning & AI
![ML.NET](https://img.shields.io/badge/ML.NET-5.0-blue?style=for-the-badge)
![AI](https://img.shields.io/badge/AI_Models-3-blueviolet?style=for-the-badge)

### Database & ORM
![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-8.0-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

### Testing & Quality
![xUnit](https://img.shields.io/badge/xUnit-2.6-green?style=for-the-badge)
![Test Coverage](https://img.shields.io/badge/Coverage-85%25-brightgreen?style=for-the-badge)

### Documentation & API
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI_3.0-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![REST API](https://img.shields.io/badge/REST-API-009688?style=for-the-badge)

### Cloud & DevOps
![Azure](https://img.shields.io/badge/Azure-App_Service-0078D4?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![GitHub Actions](https://img.shields.io/badge/GitHub_Actions-CI%2FCD-2088FF?style=for-the-badge&logo=github-actions&logoColor=white)

</div>

### 📚 المكتبات الرئيسية

```xml
<!-- ML & AI -->
<PackageReference Include="Microsoft.ML" Version="5.0.0" />
<PackageReference Include="Microsoft.ML.FastTree" Version="5.0.0" />

<!-- Database -->
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />

<!-- API Documentation -->
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />

<!-- Testing -->
<PackageReference Include="xunit" Version="2.6.0" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
```

---

## 🏗️ البنية المعمارية

### معمارية المشروع

```
🏗️ Clean Architecture + Layered Architecture

┌─────────────────────────────────────────┐
│     Presentation Layer (Controllers)    │  ← API Endpoints
├─────────────────────────────────────────┤
│     Business Logic Layer (Services)     │  ← ML Models & Logic
├─────────────────────────────────────────┤
│     Data Access Layer (Data/Models)     │  ← Database Context
└─────────────────────────────────────────┘
```

### هيكل المجلدات

```
📁 HeartDiseaseAPI/
├── 📁 Controllers/
│   └── 📄 PredictionController.cs     # Endpoints الرئيسية
├── 📁 Services/
│   ├── 📄 PredictionService.cs        # خدمة التنبؤ الرئيسية
│   ├── 📄 KNNModelService.cs          # نموذج KNN
│   ├── 📄 NaiveBayesModelService.cs   # نموذج Naive Bayes
│   └── 📄 DecisionTreeModelService.cs # نموذج Decision Tree
├── 📁 Models/
│   └── 📄 HeartDiseaseData.cs         # نماذج البيانات
├── 📁 Data/
│   ├── 📄 HeartDiseaseContext.cs      # قاعدة البيانات
│   ├── 📄 heart_balanced.csv          # بيانات متوازنة
│   ├── 📄 heart.csv                   # بيانات أصلية
│   └── 📄 clean_and_balance_heart.py  # سكريبت المعالجة
├── 📁 Tests/
│   ├── 📄 ModelServicesTests.cs       # اختبارات النماذج
│   └── 📄 RiskEvaluationTests.cs      # اختبارات تقييم المخاطر
├── 📁 Properties/
│   └── 📄 launchSettings.json         # إعدادات التشغيل
├── 📄 Program.cs                      # نقطة البداية
├── 📄 appsettings.json                # الإعدادات
├── 📄 HeartDiseaseAPI.csproj          # ملف المشروع
└── 📄 README.md                       # هذا الملف
```

---

### كيفية البناء والتشغيل 🚀

#### المتطلبات الأساسية:

- .NET 8 SDK ([تحميل .NET](https://dotnet.microsoft.com/download))
- Visual Studio 2022 أو VS Code (اختياري)
- Git

#### خطوات البناء:

**1. استنساخ المشروع:**

```bash
git clone https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git
cd HeartDiseaseAPI
```

**2. استعادة التبعيات:**

```bash
dotnet restore
```

**3. بناء المشروع:**

```bash
dotnet build
```

**4. تشغيل المشروع:**

```bash
dotnet run
```

**5. الوصول إلى الواجهة البرمجية:**

- API Base URL: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`
- Health Check: `http://localhost:5000/api/prediction/health`

**6. تشغيل الاختبارات:**

```bash
dotnet test
```

---

### الـ Endpoints الرئيسية 🔌

#### 1. التنبؤ بمخاطر أمراض القلب

```http
POST /api/prediction/predict
Content-Type: application/json

{
  "age": 50,
  "sex": 1,
  "cp": 1,
  "trestBPS": 130,
  "chol": 240,
  "fbs": 0,
  "restECG": 0,
  "thalach": 150,
  "exang": 0,
  "oldpeak": 1.0,
  "slope": 1,
  "ca": 1,
  "thal": 2
}
```

**الاستجابة:**

```json
{
  "ensemble": {
    "riskScore": 0.4523,
    "riskLevel": "moderate",
    "prediction": false,
    "confidence": 54.77
  },
  "knn": {
    "prediction": true,
    "confidence": 55.84,
    "accuracy": 82
  },
  "naiveBayes": {
    "prediction": false,
    "confidence": 48.23,
    "accuracy": 82
  },
  "decisionTree": {
    "prediction": false,
    "confidence": 32.56,
    "accuracy": 70
  }
}
```

#### 2. الحصول على مقاييس الأداء

```http
GET /api/prediction/metrics
```

**الاستجابة:**

```json
[
  {
    "modelName": "KNN",
    "accuracy": 0.82,
    "precision": 0.84,
    "recall": 0.94,
    "f1Score": 0.89
  },
  {
    "modelName": "Naive Bayes",
    "accuracy": 0.82,
    "precision": 0.81,
    "recall": 0.83,
    "f1Score": 0.82
  },
  {
    "modelName": "Decision Tree",
    "accuracy": 0.70,
    "precision": 0.65,
    "recall": 0.75,
    "f1Score": 0.70
  }
]
```

#### 3. فحص صحة الـ API

```http
GET /api/prediction/health
```

**الاستجابة:**

```json
{
  "status": "healthy",
  "timestamp": "2026-02-08T10:30:00Z",
  "version": "1.0.0",
  "models": {
    "knn": "loaded",
    "naiveBayes": "loaded",
    "decisionTree": "loaded"
  }
}
```

---

### حقول البيانات المطلوبة 📝

| **الحقل** | **النوع** | **المدى** | **الوصف** |
|-----------|----------|-----------|-----------|
| `age` | int | 20-100 | العمر بالسنوات |
| `sex` | int | 0-1 | الجنس (0: أنثى, 1: ذكر) |
| `cp` | int | 0-3 | نوع ألم الصدر |
| `trestBPS` | int | 80-200 | ضغط الدم أثناء الراحة (mmHg) |
| `chol` | int | 100-600 | الكوليسترول (mg/dl) |
| `fbs` | int | 0-1 | سكر الدم الصائم > 120 mg/dl |
| `restECG` | int | 0-2 | نتائج تخطيط القلب أثناء الراحة |
| `thalach` | int | 60-220 | أقصى معدل ضربات القلب |
| `exang` | int | 0-1 | الذبحة الصدرية أثناء التمرين |
| `oldpeak` | float | 0-10 | انخفاض ST بسبب التمرين |
| `slope` | int | 0-2 | ميل قطعة ST |
| `ca` | int | 0-4 | عدد الأوعية الدموية (0-4) |
| `thal` | int | 0-3 | نوع الثلاسيميا |

---

### نظام تقييم المخاطر 🎨

| **مستوى الخطر** | **النسبة** | **الوصف** |
|----------------|-----------|-----------|
| **منخفض** 🟢 | 0% - 30% | احتمالية منخفضة للإصابة |
| **متوسط** 🟠 | 30% - 60% | يوصى بالمتابعة مع الطبيب |
| **مرتفع** 🔴 | 60% - 100% | يتطلب فحص طبي فوري |

---

### ملفات التوثيق 📚

| **الملف** | **الوصف** |
|-----------|-----------|
| [README.md](README.md) | هذا الملف - نظرة عامة على المشروع |
| [API_DOCUMENTATION.md](API_DOCUMENTATION.md) | توثيق شامل للـ API |
| [TESTING_DOCUMENTATION.md](TESTING_DOCUMENTATION.md) | دليل الاختبار الشامل |
| [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md) | دليل النشر على Azure وخوادم أخرى |
| [PROJECT_STRUCTURE.md](PROJECT_STRUCTURE.md) | شرح تفصيلي لبنية المشروع |

---

### حالات الاختبار السريعة 🧪

#### 🟢 مخاطر منخفضة:

```json
{
  "age": 30, "sex": 0, "cp": 0, "trestBPS": 110, "chol": 180,
  "fbs": 0, "restECG": 0, "thalach": 170, "exang": 0,
  "oldpeak": 0.0, "slope": 1, "ca": 0, "thal": 2
}
```

**النتيجة المتوقعة:** 10-25% 🟢 منخفض

#### 🟠 مخاطر متوسطة:

```json
{
  "age": 50, "sex": 1, "cp": 1, "trestBPS": 130, "chol": 240,
  "fbs": 0, "restECG": 0, "thalach": 150, "exang": 0,
  "oldpeak": 1.0, "slope": 1, "ca": 1, "thal": 2
}
```

**النتيجة المتوقعة:** 35-55% 🟠 متوسط

#### 🔴 مخاطر مرتفعة:

```json
{
  "age": 65, "sex": 1, "cp": 3, "trestBPS": 160, "chol": 300,
  "fbs": 1, "restECG": 2, "thalach": 100, "exang": 1,
  "oldpeak": 3.0, "slope": 2, "ca": 3, "thal": 3
}
```

**النتيجة المتوقعة:** 70-95% 🔴 مرتفع

---

### النشر على Azure ☁️

#### خطوات النشر:

**1. تسجيل الدخول إلى Azure:**

```bash
az login
```

**2. إنشاء Web App:**

```bash
az webapp create `
  --resource-group CardioGuard-RG `
  --plan CardioGuard-Plan `
  --name cardioguard-api `
  --runtime "DOTNET|8.0"
```

**3. نشر التطبيق:**

```bash
dotnet publish -c Release -o ./publish
cd publish
az webapp deploy `
  --resource-group CardioGuard-RG `
  --name cardioguard-api `
  --src-path .
```

**4. تكوين CORS:**

```bash
az webapp cors add `
  --resource-group CardioGuard-RG `
  --name cardioguard-api `
  --allowed-origins https://your-frontend-url.com
```

---

### المشروعات المرتبطة 🔗

هذا المشروع جزء من مجموعة مشاريع CardioGuard:

| **المشروع** | **الرابط** | **الوصف** |
|-------------|-----------|-----------|
| **Frontend (Web)** | [Master-Thesis--CardioGuard](https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System) | تطبيق الويب بتقنية SvelteKit |
| **Android App** | [CardioGuard-Android-App](https://github.com/HazemAlhajIhmid/CardioGuard-Android-App) | تطبيق Android بتقنية Kotlin |
| **Backend API** | [CardioGuard-Backend-API](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API) | هذا المشروع - الواجهة البرمجية |
| **Project Hub** | [CardioGuard-Project-Hub](https://github.com/HazemAlhajIhmid/CardioGuard-Project-Hub) | المستودع الرئيسي الذي يربط كل المشاريع |

---

### معلومات البحث 🎓

**الباحث:** حازم خضر الحاج احميد

**الإشراف:**
- د.م. جورج أنور كراز (المشرف الأساسي)
- د. ماجدة البكور (المشرف المشارك)

**الجامعة:** الجامعة الافتراضية السورية  
**الوزارة:** وزارة التعليم العالي - الجمهورية العربية السورية

**عنوان البحث:** تطوير خوارزميات التنقيب عن البيانات في تحسين عملية تشخيص أمراض القلب

**البريد الإلكتروني:**
- الباحث: Hazem_82763@svuonline.org
- المشرف الأول: T_gkarraz@svuonline.org
- المشرف الثاني: T_mbakour@svuonline.org

---

### المساهمة 🤝

هذا المشروع البحثي مفتوح للمساهمات:

1. عمل Fork للمشروع
2. إنشاء Branch جديد (`git checkout -b feature/amazing-feature`)
3. Commit التغييرات (`git commit -m 'Add amazing feature'`)
4. Push إلى Branch (`git push origin feature/amazing-feature`)
5. فتح Pull Request

---

### المشاكل المعروفة والحلول 🔧

| **المشكلة** | **الحل** |
|-------------|---------|
| Port 5000 مستخدم | غيّر البورت في `launchSettings.json` |
| CORS Error | أضف Origin في `Program.cs` |
| ML Models لا تحمّل | تحقق من وجود ملف `heart_balanced.csv` |
| بطء الاستجابة | استخدم Singleton للنماذج (موجود بالفعل) |

---

### الأمان والخصوصية 🔒

- ✅ لا يتم تخزين البيانات الطبية
- ✅ لا توجد قاعدة بيانات للمرضى
- ✅ كل طلب مستقل ولا يحتفظ بالبيانات
- ✅ استخدام HTTPS في الإنتاج
- ⚠️ هذا النظام للأغراض البحثية فقط ولا يحل محل الاستشارة الطبية

---

### الترخيص 📜

© 2026 CardioGuard - جميع الحقوق محفوظة

هذا المشروع للأغراض البحثية الأكاديمية فقط. لا يحل محل الاستشارة الطبية المتخصصة.

---

### الدعم الفني 📞

للمساعدة والاستفسارات:
- **GitHub Issues**: [افتح Issue جديد](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- **البريد الإلكتروني**: Hazem_82763@svuonline.org

---

### الإصدارات 📅

#### v1.0.0 (2026-02-08)

- ✅ إطلاق النسخة الأولى
- ✅ ثلاثة نماذج للتعلم الآلي (KNN, Naive Bayes, Decision Tree)
- ✅ نظام Ensemble للجمع بين النماذج
- ✅ Swagger/OpenAPI للتوثيق
- ✅ اختبارات وحدات شاملة
- ✅ جاهز للنشر على Azure
- ✅ CORS Support
- ✅ Health Check Endpoint

---

### الموارد الإضافية 📚

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [ML.NET Documentation](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)
- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Swagger/OpenAPI Documentation](https://swagger.io/docs/)

---

### شكر وتقدير 🙏

- الجامعة الافتراضية السورية
- وزارة التعليم العالي
- المشرفون على البحث
- UCI Machine Learning Repository (مصدر البيانات)

---

**تاريخ آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0.0  
**الحالة:** ✅ مستقر ومكتمل

---

## 🌍 English Version

### Overview

CardioGuard API is a RESTful API built with ASP.NET Core 8.0 that uses ML.NET to predict heart disease risk. The API uses three machine learning models to provide accurate and comprehensive risk assessment.

### Key Features ✨

- 🤖 Three AI Models: KNN, Naive Bayes, Decision Tree
- 📊 Ensemble System: Combines results from all three models
- 🔌 RESTful API: Standardized and unified interface
- 📝 Swagger/OpenAPI: Interactive API documentation
- ⚡ High Performance: Fast response (< 100ms)
- 🔒 Secure: No patient data storage
- 🌐 CORS Support: Works with web applications
- ☁️ Azure Ready: With CI/CD Pipeline

### Quick Start 🚀

```bash
# Clone repository
git clone https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git

# Restore dependencies
dotnet restore

# Run project
dotnet run

# Access Swagger UI
# http://localhost:5000/swagger
```

### Tech Stack 🛠️

- Framework: ASP.NET Core 8.0
- Language: C# 12
- Machine Learning: ML.NET 5.0
- Database: Entity Framework Core + SQL Server
- Testing: xUnit

### Documentation 📚

- [API Documentation](API_DOCUMENTATION.md)
- [Testing Documentation](TESTING_DOCUMENTATION.md)
- [Deployment Guide](DEPLOYMENT_GUIDE.md)
- [Project Structure](PROJECT_STRUCTURE.md)

### License 📜

© 2026 CardioGuard - All Rights Reserved

For academic research purposes only. Does not replace professional medical consultation.

---

**Last Updated:** February 8, 2026  
**Version:** 1.0.0  
**Status:** ✅ Stable & Complete
