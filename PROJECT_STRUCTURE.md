# Project Structure 🏗️

## CardioGuard Backend API - البنية التفصيلية للمشروع

### 📋 جدول المحتويات

1. [نظرة عامة](#نظرة-عامة)
2. [هيكل المجلدات](#هيكل-المجلدات)
3. [Controllers](#controllers)
4. [Services](#services)
5. [Models](#models)
6. [Data](#data)
7. [Tests](#tests)
8. [المعمارية](#المعمارية)

---

## نظرة عامة

المشروع يتبع معمارية **Clean Architecture** و **Layered Architecture** لضمان:
- فصل المسؤوليات (Separation of Concerns)
- سهولة الاختبار (Testability)
- سهولة الصيانة (Maintainability)
- قابلية التوسع (Scalability)

---

## هيكل المجلدات

```
📁 HeartDiseaseAPI/
│
├── 📁 Controllers/                    # طبقة العرض (Presentation Layer)
│   └── 📄 PredictionController.cs     # التحكم بـ API Endpoints
│
├── 📁 Services/                       # طبقة المنطق (Business Logic Layer)
│   ├── 📄 PredictionService.cs        # خدمة التنبؤ الرئيسية
│   ├── 📄 KNNModelService.cs          # خدمة نموذج KNN
│   ├── 📄 NaiveBayesModelService.cs   # خدمة نموذج Naive Bayes
│   └── 📄 DecisionTreeModelService.cs # خدمة نموذج Decision Tree
│
├── 📁 Models/                         # نماذج البيانات (Data Models)
│   └── 📄 HeartDiseaseData.cs         # نموذج بيانات القلب
│
├── 📁 Data/                           # طبقة البيانات (Data Layer)
│   ├── 📄 HeartDiseaseContext.cs      # سياق قاعدة البيانات
│   ├── 📄 heart_balanced.csv          # بيانات متوازنة للتدريب
│   ├── 📄 heart.csv                   # بيانات أصلية
│   └── 📄 clean_and_balance_heart.py  # سكريبت معالجة البيانات
│
├── 📁 Tests/                          # الاختبارات (Tests)
│   ├── 📄 ModelServicesTests.cs       # اختبارات خدمات النماذج
│   └── 📄 RiskEvaluationTests.cs      # اختبارات تقييم المخاطر
│
├── 📁 Properties/                     # خصائص المشروع
│   └── 📄 launchSettings.json         # إعدادات التشغيل
│
├── 📁 bin/                            # الملفات المُجمّعة
│   ├── Debug/                         # بناء التطوير
│   └── Release/                       # بناء الإنتاج
│
├── 📁 obj/                            # ملفات مؤقتة للبناء
│
├── 📁 publish/                        # ملفات النشر
│
├── 📄 Program.cs                      # نقطة البداية الرئيسية
├── 📄 appsettings.json                # إعدادات التطبيق
├── 📄 HeartDiseaseAPI.csproj          # ملف مشروع .NET
├── 📄 README.md                       # الملف التعريفي
├── 📄 API_DOCUMENTATION.md            # توثيق الـ API
├── 📄 TESTING_DOCUMENTATION.md        # دليل الاختبار
├── 📄 DEPLOYMENT_GUIDE.md             # دليل النشر
└── 📄 PROJECT_STRUCTURE.md            # هذا الملف
```

---

## Controllers

### PredictionController.cs

**الموقع:** `Controllers/PredictionController.cs`

**الدور:** يتعامل مع HTTP Requests ويعيد HTTP Responses

**الـ Endpoints:**

| Endpoint | Method | الوصف |
|----------|--------|-------|
| `/api/prediction/predict` | POST | التنبؤ بمخاطر أمراض القلب |
| `/api/prediction/metrics` | GET | الحصول على مقاييس أداء النماذج |
| `/api/prediction/health` | GET | فحص صحة الـ API |

**البنية:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class PredictionController : ControllerBase
{
    private readonly PredictionService _predictionService;
    private readonly ILogger<PredictionController> _logger;

    // Constructor
    public PredictionController(
        PredictionService predictionService,
        ILogger<PredictionController> logger)
    {
        _predictionService = predictionService;
        _logger = logger;
    }

    // Endpoints
    [HttpPost("predict")]
    public ActionResult<PredictionResponse> Predict([FromBody] PredictionRequest request)
    { ... }

    [HttpGet("metrics")]
    public ActionResult<List<ModelMetrics>> GetMetrics()
    { ... }

    [HttpGet("health")]
    public ActionResult<object> HealthCheck()
    { ... }
}
```

**المسؤوليات:**
- ✅ استقبال HTTP Requests
- ✅ التحقق من صحة البيانات (Validation)
- ✅ استدعاء خدمات المنطق (Business Logic)
- ✅ معالجة الأخطاء
- ✅ تسجيل الأحداث (Logging)
- ✅ إرجاع HTTP Responses

---

## Services

### 1. PredictionService.cs

**الموقع:** `Services/PredictionService.cs`

**الدور:** خدمة رئيسية تجمع نتائج النماذج الثلاثة

**البنية:**

```csharp
public class PredictionService
{
    private readonly KNNModelService _knnService;
    private readonly NaiveBayesModelService _naiveBayesService;
    private readonly DecisionTreeModelService _decisionTreeService;

    public PredictionService(
        KNNModelService knnService,
        NaiveBayesModelService naiveBayesService,
        DecisionTreeModelService decisionTreeService)
    {
        _knnService = knnService;
        _naiveBayesService = naiveBayesService;
        _decisionTreeService = decisionTreeService;
    }

    public PredictionResponse PredictHeartDisease(PredictionRequest request)
    {
        // 1. تحويل Request إلى HeartDiseaseData
        var input = ConvertToHeartDiseaseData(request);

        // 2. الحصول على تنبؤات النماذج الثلاثة
        var knnResult = _knnService.Predict(input);
        var nbResult = _naiveBayesService.Predict(input);
        var dtResult = _decisionTreeService.Predict(input);

        // 3. حساب Ensemble
        var ensembleResult = CalculateEnsemble(knnResult, nbResult, dtResult);

        // 4. إنشاء الاستجابة
        return new PredictionResponse
        {
            Ensemble = ensembleResult,
            KNN = knnResult,
            NaiveBayes = nbResult,
            DecisionTree = dtResult
        };
    }
}
```

**المسؤوليات:**
- ✅ تنسيق عمل النماذج الثلاثة
- ✅ حساب نتيجة Ensemble
- ✅ تحويل البيانات بين الأشكال المختلفة
- ✅ حساب مستوى المخاطر

---

### 2. KNNModelService.cs

**الموقع:** `Services/KNNModelService.cs`

**الدور:** تنفيذ خوارزمية K-Nearest Neighbors

**البنية:**

```csharp
public class KNNModelService
{
    private PredictionEngine<HeartDiseaseData, HeartDiseasePrediction> _predictionEngine;

    public KNNModelService()
    {
        LoadModel();
    }

    private void LoadModel()
    {
        var mlContext = new MLContext(seed: 0);
        
        // تحميل البيانات
        var dataView = mlContext.Data.LoadFromTextFile<HeartDiseaseData>(
            "Data/heart_balanced.csv",
            separatorChar: ',',
            hasHeader: true
        );

        // إنشاء Pipeline
        var pipeline = mlContext.Transforms.Concatenate("Features", ...)
            .Append(mlContext.BinaryClassification.Trainers.KNearestNeighbors(...));

        // تدريب النموذج
        var model = pipeline.Fit(dataView);

        // إنشاء Prediction Engine
        _predictionEngine = mlContext.Model.CreatePredictionEngine<...>(model);
    }

    public ModelResult Predict(HeartDiseaseData input)
    {
        var prediction = _predictionEngine.Predict(input);
        
        return new ModelResult
        {
            Prediction = prediction.Prediction,
            Confidence = prediction.Probability * 100,
            Accuracy = 82 // الدقة من التدريب
        };
    }
}
```

**الخصائص:**
- **الدقة:** 82%
- **Recall:** 94% (الأفضل للكشف المبكر)
- **K:** 15 (عدد الجيران)

---

### 3. NaiveBayesModelService.cs

**الموقع:** `Services/NaiveBayesModelService.cs`

**الدور:** تنفيذ خوارزمية Naive Bayes

**الخصائص:**
- **الدقة:** 82%
- **F1-Score:** 0.82
- **سريع:** أسرع من KNN

---

### 4. DecisionTreeModelService.cs

**الموقع:** `Services/DecisionTreeModelService.cs`

**الدور:** تنفيذ خوارزمية Decision Tree

**الخصائص:**
- **الدقة:** 70%
- **سهل التفسير:** شجرة قرارات واضحة
- **Max Depth:** 10

---

## Models

### HeartDiseaseData.cs

**الموقع:** `Models/HeartDiseaseData.cs`

**الدور:** نموذج البيانات المستخدم في التنبؤ

**البنية:**

```csharp
public class HeartDiseaseData
{
    [LoadColumn(0)]
    public float Age { get; set; }              // العمر

    [LoadColumn(1)]
    public float Sex { get; set; }              // الجنس (0: أنثى, 1: ذكر)

    [LoadColumn(2)]
    public float CP { get; set; }               // نوع ألم الصدر

    [LoadColumn(3)]
    public float TrestBPS { get; set; }         // ضغط الدم

    [LoadColumn(4)]
    public float Chol { get; set; }             // الكوليسترول

    [LoadColumn(5)]
    public float FBS { get; set; }              // سكر الدم

    [LoadColumn(6)]
    public float RestECG { get; set; }          // تخطيط القلب

    [LoadColumn(7)]
    public float Thalach { get; set; }          // معدل ضربات القلب

    [LoadColumn(8)]
    public float Exang { get; set; }            // الذبحة الصدرية

    [LoadColumn(9)]
    public float Oldpeak { get; set; }          // انخفاض ST

    [LoadColumn(10)]
    public float Slope { get; set; }            // ميل ST

    [LoadColumn(11)]
    public float CA { get; set; }               // عدد الأوعية

    [LoadColumn(12)]
    public float Thal { get; set; }             // الثلاسيميا

    [LoadColumn(13)]
    [ColumnName("Label")]
    public bool Target { get; set; }            // النتيجة (0: سليم, 1: مريض)
}
```

**نماذج أخرى:**

```csharp
// طلب التنبؤ
public class PredictionRequest
{
    public int Age { get; set; }
    public int Sex { get; set; }
    // ... باقي الحقول
}

// استجابة التنبؤ
public class PredictionResponse
{
    public EnsembleResult Ensemble { get; set; }
    public ModelResult KNN { get; set; }
    public ModelResult NaiveBayes { get; set; }
    public ModelResult DecisionTree { get; set; }
}

// نتيجة النموذج
public class ModelResult
{
    public bool Prediction { get; set; }
    public float Confidence { get; set; }
    public int Accuracy { get; set; }
}

// نتيجة Ensemble
public class EnsembleResult
{
    public float RiskScore { get; set; }
    public string RiskLevel { get; set; }
    public bool Prediction { get; set; }
    public float Confidence { get; set; }
}
```

---

## Data

### 1. heart.csv

**الوصف:** البيانات الأصلية من UCI Machine Learning Repository

**الحجم:** 303 سجل

**المصدر:** [UCI Heart Disease Dataset](https://archive.ics.uci.edu/ml/datasets/Heart+Disease)

### 2. heart_balanced.csv

**الوصف:** بيانات محسّنة ومتوازنة

**الحجم:** 920 سجل بعد Oversampling

**التوازن:**
- حالات مرضية: 460 (50%)
- حالات سليمة: 460 (50%)

### 3. clean_and_balance_heart.py

**الوصف:** سكريبت Python لمعالجة وموازنة البيانات

```python
import pandas as pd
from imblearn.over_sampling import SMOTE

# قراءة البيانات
df = pd.read_csv('heart.csv')

# فصل Features و Target
X = df.drop('target', axis=1)
y = df['target']

# تطبيق SMOTE
smote = SMOTE(random_state=42)
X_resampled, y_resampled = smote.fit_resample(X, y)

# حفظ البيانات المتوازنة
balanced_df = pd.concat([X_resampled, y_resampled], axis=1)
balanced_df.to_csv('heart_balanced.csv', index=False)
```

### 4. HeartDiseaseContext.cs

**الوصف:** سياق Entity Framework Core (اختياري)

```csharp
public class HeartDiseaseContext : DbContext
{
    public HeartDiseaseContext(DbContextOptions<HeartDiseaseContext> options)
        : base(options)
    {
    }

    public DbSet<HeartDiseaseData> HeartDiseaseRecords { get; set; }
}
```

---

## Tests

### 1. ModelServicesTests.cs

**الوصف:** اختبارات لخدمات النماذج

```csharp
public class ModelServicesTests
{
    [Fact]
    public void KNN_LowRisk_ShouldPredict_NoDisease()
    { ... }

    [Fact]
    public void KNN_HighRisk_ShouldPredict_Disease()
    { ... }

    [Fact]
    public void NaiveBayes_ShouldPredict_Correctly()
    { ... }
}
```

### 2. RiskEvaluationTests.cs

**الوصف:** اختبارات لنظام تقييم المخاطر

```csharp
public class RiskEvaluationTests
{
    [Theory]
    [InlineData(0.15f, "low")]
    [InlineData(0.45f, "moderate")]
    [InlineData(0.85f, "high")]
    public void RiskLevel_ShouldEvaluate_Correctly(float score, string expected)
    { ... }
}
```

---

## المعمارية

### Layered Architecture

```
┌─────────────────────────────────────┐
│     Presentation Layer              │
│  (Controllers)                      │
│  - PredictionController             │
└────────────┬────────────────────────┘
             │
             ▼
┌─────────────────────────────────────┐
│     Business Logic Layer            │
│  (Services)                         │
│  - PredictionService                │
│  - KNNModelService                  │
│  - NaiveBayesModelService           │
│  - DecisionTreeModelService         │
└────────────┬────────────────────────┘
             │
             ▼
┌─────────────────────────────────────┐
│     Data Access Layer               │
│  (Data, Models)                     │
│  - HeartDiseaseContext              │
│  - HeartDiseaseData                 │
│  - CSV Files                        │
└─────────────────────────────────────┘
```

### Dependency Injection

```csharp
// في Program.cs
builder.Services.AddSingleton<KNNModelService>();
builder.Services.AddSingleton<NaiveBayesModelService>();
builder.Services.AddSingleton<DecisionTreeModelService>();
builder.Services.AddScoped<PredictionService>();
```

**لماذا Singleton للنماذج؟**
- ✅ تحميل النموذج مرة واحدة فقط
- ✅ توفير الذاكرة
- ✅ أداء أفضل

**لماذا Scoped للـ PredictionService؟**
- ✅ لكل طلب HTTP نسخة جديدة
- ✅ Thread-safe

---

## تدفق البيانات (Data Flow)

```
1. HTTP Request
   │
   ▼
2. PredictionController
   │
   ▼
3. PredictionService
   │
   ├─────────┬─────────┬─────────┐
   │         │         │         │
   ▼         ▼         ▼         ▼
4. KNN      NB        DT    Calculate Ensemble
   │         │         │         │
   └─────────┴─────────┴─────────┘
   │
   ▼
5. PredictionResponse
   │
   ▼
6. HTTP Response (JSON)
```

---

## أفضل الممارسات

### 1. استخدام Interfaces

```csharp
public interface IModelService
{
    ModelResult Predict(HeartDiseaseData input);
}

public class KNNModelService : IModelService
{
    public ModelResult Predict(HeartDiseaseData input)
    { ... }
}
```

### 2. Exception Handling

```csharp
try
{
    var result = _predictionService.PredictHeartDisease(request);
    return Ok(result);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error occurred");
    return StatusCode(500, new { error = "Internal error" });
}
```

### 3. Logging

```csharp
_logger.LogInformation("Received prediction request for age: {Age}", request.Age);
_logger.LogWarning("Invalid input detected");
_logger.LogError(ex, "Error occurred while making prediction");
```

---

## الإضافات المستقبلية

- ✅ إضافة Authentication & Authorization
- ✅ إضافة Rate Limiting
- ✅ حفظ التنبؤات في قاعدة البيانات
- ✅ إضافة نماذج أخرى (Random Forest, SVM)
- ✅ تحسين نموذج Decision Tree
- ✅ إضافة Caching
- ✅ إضافة API Versioning

---

## الموارد الإضافية

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [ML.NET Documentation](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

**آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0.0
