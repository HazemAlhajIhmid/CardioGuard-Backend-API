# Testing Documentation 🧪

## CardioGuard Backend API - دليل الاختبار الشامل

### 📋 جدول المحتويات

1. [نظرة عامة](#نظرة-عامة)
2. [أنواع الاختبارات](#أنواع-الاختبارات)
3. [متطلبات الاختبار](#متطلبات-الاختبار)
4. [اختبارات الوحدات](#اختبارات-الوحدات)
5. [اختبارات التكامل](#اختبارات-التكامل)
6. [حالات الاختبار](#حالات-الاختبار)
7. [تشغيل الاختبارات](#تشغيل-الاختبارات)
8. [تغطية الكود](#تغطية-الكود)

---

## نظرة عامة

هذا الدليل يوفر معلومات شاملة حول اختبار الـ Backend API لنظام CardioGuard.

### أهداف الاختبار

- ✅ التأكد من صحة نماذج التعلم الآلي
- ✅ التحقق من دقة التنبؤات
- ✅ اختبار معالجة الأخطاء
- ✅ التأكد من صحة التحقق من البيانات
- ✅ قياس أداء النماذج

---

## أنواع الاختبارات

### 1. Unit Tests (اختبارات الوحدات) ✅

اختبار كل مكون بشكل منفصل:
- خدمات النماذج (KNN, Naive Bayes, Decision Tree)
- خدمة التنبؤ الرئيسية (PredictionService)
- تقييم المخاطر (Risk Evaluation)

### 2. Integration Tests (اختبارات التكامل) ✅

اختبار تكامل المكونات معاً:
- API Controllers
- التكامل مع قاعدة البيانات
- نظام Ensemble

### 3. Manual Testing (الاختبار اليدوي) ✅

- اختبار عبر Swagger UI
- اختبار عبر Postman
- اختبار عبر cURL

---

## متطلبات الاختبار

### الأدوات المطلوبة

```bash
# مكتبات الاختبار
dotnet add package xunit
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio

# اختياري: للتغطية
dotnet tool install --global coverlet.console
```

### البيانات المطلوبة

- ملف `heart_balanced.csv` في مجلد `Data/`
- حالات اختبار محددة مسبقاً

---

## اختبارات الوحدات

### 1. اختبار KNN Model Service

**الملف:** `Tests/ModelServicesTests.cs`

#### الاختبار 1: التحقق من تحميل النموذج

```csharp
[Fact]
public void KNNModelService_ShouldLoadSuccessfully()
{
    // Arrange & Act
    var service = new KNNModelService();

    // Assert
    Assert.NotNull(service);
}
```

#### الاختبار 2: التنبؤ بحالة معروفة - خطر منخفض

```csharp
[Fact]
public void KNNModelService_LowRiskPatient_ShouldPredictNoDisease()
{
    // Arrange
    var service = new KNNModelService();
    var input = new HeartDiseaseData
    {
        Age = 30,
        Sex = 0,
        CP = 0,
        TrestBPS = 110,
        Chol = 180,
        FBS = 0,
        RestECG = 0,
        Thalach = 170,
        Exang = 0,
        Oldpeak = 0.0f,
        Slope = 1,
        CA = 0,
        Thal = 2
    };

    // Act
    var result = service.Predict(input);

    // Assert
    Assert.False(result.Prediction);
    Assert.True(result.Confidence < 30); // خطر منخفض
}
```

#### الاختبار 3: التنبؤ بحالة معروفة - خطر مرتفع

```csharp
[Fact]
public void KNNModelService_HighRiskPatient_ShouldPredictDisease()
{
    // Arrange
    var service = new KNNModelService();
    var input = new HeartDiseaseData
    {
        Age = 65,
        Sex = 1,
        CP = 3,
        TrestBPS = 160,
        Chol = 300,
        FBS = 1,
        RestECG = 2,
        Thalach = 100,
        Exang = 1,
        Oldpeak = 3.0f,
        Slope = 2,
        CA = 3,
        Thal = 3
    };

    // Act
    var result = service.Predict(input);

    // Assert
    Assert.True(result.Prediction);
    Assert.True(result.Confidence > 70); // خطر مرتفع
}
```

### 2. اختبار Naive Bayes Model Service

```csharp
[Fact]
public void NaiveBayesModelService_ShouldPredictCorrectly()
{
    // Arrange
    var service = new NaiveBayesModelService();
    var input = new HeartDiseaseData
    {
        Age = 50,
        Sex = 1,
        CP = 1,
        TrestBPS = 130,
        Chol = 240,
        FBS = 0,
        RestECG = 0,
        Thalach = 150,
        Exang = 0,
        Oldpeak = 1.0f,
        Slope = 1,
        CA = 1,
        Thal = 2
    };

    // Act
    var result = service.Predict(input);

    // Assert
    Assert.NotNull(result);
    Assert.InRange(result.Confidence, 0, 100);
}
```

### 3. اختبار Decision Tree Model Service

```csharp
[Fact]
public void DecisionTreeModelService_ShouldPredictCorrectly()
{
    // Arrange
    var service = new DecisionTreeModelService();
    var input = new HeartDiseaseData
    {
        Age = 45,
        Sex = 0,
        CP = 2,
        TrestBPS = 120,
        Chol = 220,
        FBS = 0,
        RestECG = 1,
        Thalach = 160,
        Exang = 0,
        Oldpeak = 0.5f,
        Slope = 1,
        CA = 0,
        Thal = 2
    };

    // Act
    var result = service.Predict(input);

    // Assert
    Assert.NotNull(result);
    Assert.InRange(result.Confidence, 0, 100);
}
```

### 4. اختبار Prediction Service (نظام Ensemble)

```csharp
[Fact]
public void PredictionService_ShouldCombineAllModels()
{
    // Arrange
    var knnService = new KNNModelService();
    var naiveBayesService = new NaiveBayesModelService();
    var decisionTreeService = new DecisionTreeModelService();
    var predictionService = new PredictionService(
        knnService, 
        naiveBayesService, 
        decisionTreeService
    );

    var request = new PredictionRequest
    {
        Age = 50,
        Sex = 1,
        CP = 1,
        TrestBPS = 130,
        Chol = 240,
        FBS = 0,
        RestECG = 0,
        Thalach = 150,
        Exang = 0,
        Oldpeak = 1.0f,
        Slope = 1,
        CA = 1,
        Thal = 2
    };

    // Act
    var result = predictionService.PredictHeartDisease(request);

    // Assert
    Assert.NotNull(result);
    Assert.NotNull(result.Ensemble);
    Assert.NotNull(result.KNN);
    Assert.NotNull(result.NaiveBayes);
    Assert.NotNull(result.DecisionTree);
}
```

---

## اختبارات تقييم المخاطر

**الملف:** `Tests/RiskEvaluationTests.cs`

### الاختبار 1: تقييم الخطر المنخفض

```csharp
[Fact]
public void RiskEvaluation_LowRiskScore_ShouldReturnLowLevel()
{
    // Arrange
    float lowRisk = 0.15f; // 15%

    // Act
    var riskLevel = RiskEvaluator.GetRiskLevel(lowRisk);

    // Assert
    Assert.Equal("low", riskLevel);
}
```

### الاختبار 2: تقييم الخطر المتوسط

```csharp
[Fact]
public void RiskEvaluation_ModerateRiskScore_ShouldReturnModerateLevel()
{
    // Arrange
    float moderateRisk = 0.45f; // 45%

    // Act
    var riskLevel = RiskEvaluator.GetRiskLevel(moderateRisk);

    // Assert
    Assert.Equal("moderate", riskLevel);
}
```

### الاختبار 3: تقييم الخطر المرتفع

```csharp
[Fact]
public void RiskEvaluation_HighRiskScore_ShouldReturnHighLevel()
{
    // Arrange
    float highRisk = 0.85f; // 85%

    // Act
    var riskLevel = RiskEvaluator.GetRiskLevel(highRisk);

    // Assert
    Assert.Equal("high", riskLevel);
}
```

### الاختبار 4: حدود تقييم المخاطر

```csharp
[Theory]
[InlineData(0.00f, "low")]
[InlineData(0.30f, "moderate")]
[InlineData(0.60f, "high")]
[InlineData(1.00f, "high")]
public void RiskEvaluation_BoundaryValues_ShouldEvaluateCorrectly(
    float riskScore, 
    string expectedLevel)
{
    // Act
    var riskLevel = RiskEvaluator.GetRiskLevel(riskScore);

    // Assert
    Assert.Equal(expectedLevel, riskLevel);
}
```

---

## حالات الاختبار الشاملة

### حالة 1: مريض بخطر منخفض 🟢

**البيانات:**
```json
{
  "age": 30,
  "sex": 0,
  "cp": 0,
  "trestBPS": 110,
  "chol": 180,
  "fbs": 0,
  "restECG": 0,
  "thalach": 170,
  "exang": 0,
  "oldpeak": 0.0,
  "slope": 1,
  "ca": 0,
  "thal": 2
}
```

**النتيجة المتوقعة:**
- Risk Score: 10-25%
- Risk Level: low
- Prediction: false (سليم)

**اختبار التحقق:**
```csharp
[Fact]
public void TestCase1_LowRiskPatient()
{
    var result = PredictHeartDisease(GetTestCase1Data());
    
    Assert.Equal("low", result.Ensemble.RiskLevel);
    Assert.False(result.Ensemble.Prediction);
    Assert.InRange(result.Ensemble.RiskScore, 0.10f, 0.25f);
}
```

### حالة 2: مريض بخطر متوسط 🟠

**البيانات:**
```json
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

**النتيجة المتوقعة:**
- Risk Score: 35-55%
- Risk Level: moderate
- Prediction: متباين (قد يكون true أو false)

**اختبار التحقق:**
```csharp
[Fact]
public void TestCase2_ModerateRiskPatient()
{
    var result = PredictHeartDisease(GetTestCase2Data());
    
    Assert.Equal("moderate", result.Ensemble.RiskLevel);
    Assert.InRange(result.Ensemble.RiskScore, 0.35f, 0.55f);
}
```

### حالة 3: مريض بخطر مرتفع 🔴

**البيانات:**
```json
{
  "age": 65,
  "sex": 1,
  "cp": 3,
  "trestBPS": 160,
  "chol": 300,
  "fbs": 1,
  "restECG": 2,
  "thalach": 100,
  "exang": 1,
  "oldpeak": 3.0,
  "slope": 2,
  "ca": 3,
  "thal": 3
}
```

**النتيجة المتوقعة:**
- Risk Score: 70-95%
- Risk Level: high
- Prediction: true (مريض)

**اختبار التحقق:**
```csharp
[Fact]
public void TestCase3_HighRiskPatient()
{
    var result = PredictHeartDisease(GetTestCase3Data());
    
    Assert.Equal("high", result.Ensemble.RiskLevel);
    Assert.True(result.Ensemble.Prediction);
    Assert.InRange(result.Ensemble.RiskScore, 0.70f, 0.95f);
}
```

---

## اختبارات التحقق من البيانات

### اختبار العمر

```csharp
[Theory]
[InlineData(19)] // أقل من الحد الأدنى
[InlineData(101)] // أكثر من الحد الأقصى
public void Validation_InvalidAge_ShouldReturnBadRequest(int age)
{
    // Arrange
    var request = GetValidRequest();
    request.Age = age;

    // Act & Assert
    Assert.Throws<ValidationException>(() => 
        ValidateRequest(request));
}
```

### اختبار ضغط الدم

```csharp
[Theory]
[InlineData(79)]  // أقل من الحد الأدنى
[InlineData(201)] // أكثر من الحد الأقصى
public void Validation_InvalidBloodPressure_ShouldReturnBadRequest(int bp)
{
    // Arrange
    var request = GetValidRequest();
    request.TrestBPS = bp;

    // Act & Assert
    Assert.Throws<ValidationException>(() => 
        ValidateRequest(request));
}
```

### اختبار الكوليسترول

```csharp
[Theory]
[InlineData(99)]  // أقل من الحد الأدنى
[InlineData(601)] // أكثر من الحد الأقصى
public void Validation_InvalidCholesterol_ShouldReturnBadRequest(int chol)
{
    // Arrange
    var request = GetValidRequest();
    request.Chol = chol;

    // Act & Assert
    Assert.Throws<ValidationException>(() => 
        ValidateRequest(request));
}
```

---

## تشغيل الاختبارات

### تشغيل جميع الاختبارات

```bash
# من مجلد المشروع
cd HeartDiseaseAPI

# تشغيل جميع الاختبارات
dotnet test

# Output:
# Starting test execution, please wait...
# Passed!  - Failed:     0, Passed:    25, Skipped:     0, Total:    25
```

### تشغيل اختبارات محددة

```bash
# اختبارات نموذج KNN فقط
dotnet test --filter "FullyQualifiedName~KNNModelService"

# اختبارات تقييم المخاطر فقط
dotnet test --filter "FullyQualifiedName~RiskEvaluation"

# اختبارات الـ Controller فقط
dotnet test --filter "FullyQualifiedName~PredictionController"
```

### تشغيل الاختبارات مع تفاصيل

```bash
# مع معلومات مفصلة
dotnet test --logger "console;verbosity=detailed"

# مع تقرير HTML
dotnet test --logger "html;LogFileName=TestReport.html"
```

---

## تغطية الكود (Code Coverage)

### تثبيت أدوات التغطية

```bash
dotnet tool install --global coverlet.console
dotnet tool install --global dotnet-reportgenerator-globaltool
```

### قياس التغطية

```bash
# تشغيل الاختبارات مع قياس التغطية
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# إنشاء تقرير HTML
reportgenerator -reports:coverage.opencover.xml -targetdir:coverage -reporttypes:Html

# فتح التقرير
start coverage/index.html
```

### الأهداف المطلوبة للتغطية

| المكون | التغطية المستهدفة |
|--------|-------------------|
| Services | > 90% |
| Controllers | > 85% |
| Models | > 95% |
| الإجمالي | > 85% |

---

## الاختبار اليدوي عبر Swagger

### الخطوات:

1. تشغيل المشروع:
```bash
dotnet run
```

2. فتح Swagger UI:
```
http://localhost:5000/swagger
```

3. اختبار Endpoint التنبؤ:
   - افتح `POST /api/prediction/predict`
   - اضغط "Try it out"
   - أدخل البيانات
   - اضغط "Execute"
   - راجع النتيجة

### بيانات اختبار جاهزة:

**حالة 1 - خطر منخفض:**
```json
{"age":30,"sex":0,"cp":0,"trestBPS":110,"chol":180,"fbs":0,"restECG":0,"thalach":170,"exang":0,"oldpeak":0.0,"slope":1,"ca":0,"thal":2}
```

**حالة 2 - خطر متوسط:**
```json
{"age":50,"sex":1,"cp":1,"trestBPS":130,"chol":240,"fbs":0,"restECG":0,"thalach":150,"exang":0,"oldpeak":1.0,"slope":1,"ca":1,"thal":2}
```

**حالة 3 - خطر مرتفع:**
```json
{"age":65,"sex":1,"cp":3,"trestBPS":160,"chol":300,"fbs":1,"restECG":2,"thalach":100,"exang":1,"oldpeak":3.0,"slope":2,"ca":3,"thal":3}
```

---

## الاختبار عبر Postman

### استيراد المجموعة:

1. افتح Postman
2. File > Import
3. استورد المجموعة من [API_DOCUMENTATION.md](API_DOCUMENTATION.md)

### اختبارات تلقائية في Postman:

```javascript
// في تبويب Tests لكل طلب

// اختبار رمز الحالة
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

// اختبار وقت الاستجابة
pm.test("Response time is less than 500ms", function () {
    pm.expect(pm.response.responseTime).to.be.below(500);
});

// اختبار البنية
pm.test("Response has correct structure", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('ensemble');
    pm.expect(jsonData).to.have.property('knn');
    pm.expect(jsonData).to.have.property('naiveBayes');
    pm.expect(jsonData).to.have.property('decisionTree');
});

// اختبار القيم
pm.test("Risk score is between 0 and 1", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.ensemble.riskScore).to.be.at.least(0);
    pm.expect(jsonData.ensemble.riskScore).to.be.at.most(1);
});
```

---

## الاختبار عبر cURL

### حالة اختبار سريعة:

```bash
curl -X POST "http://localhost:5000/api/prediction/predict" \
  -H "Content-Type: application/json" \
  -d '{
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
  }'
```

---

## مؤشرات الأداء

### الاختبارات المطلوبة:

- ✅ جميع الاختبارات تنجح (Pass Rate: 100%)
- ✅ وقت التنفيذ < 5 ثوانٍ لجميع الاختبارات
- ✅ تغطية الكود > 85%
- ✅ لا توجد اختبارات متجاهلة (Skipped: 0)

### الأداء المتوقع:

| النموذج | وقت التنفيذ |
|---------|-------------|
| KNN | < 50ms |
| Naive Bayes | < 30ms |
| Decision Tree | < 20ms |
| Ensemble | < 100ms |

---

## استكشاف الأخطاء

### المشكلة: الاختبارات تفشل بشكل عشوائي

**الحل:**
- تأكد من وجود ملف `heart_balanced.csv`
- تحقق من مسار الملف
- أعد تدريب النماذج إذا لزم الأمر

### المشكلة: الاختبارات بطيئة جداً

**الحل:**
- استخدم Singleton للنماذج
- قلل حجم بيانات الاختبار
- شغل الاختبارات بالتوازي:
```bash
dotnet test --parallel
```

---

## الدعم والمساعدة

- **GitHub Issues:** [افتح Issue جديد](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- **البريد الإلكتروني:** Hazem_82763@svuonline.org

---

**آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0.0
