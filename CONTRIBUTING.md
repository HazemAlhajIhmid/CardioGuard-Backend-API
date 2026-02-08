# 🤝 دليل المساهمة في CardioGuard

## Contributing Guide

مرحباً بك في مشروع CardioGuard! نحن سعداء باهتمامك بالمساهمة في هذا المشروع البحثي.

---

## 📋 جدول المحتويات

- [قواعد السلوك](#-قواعد-السلوك)
- [كيفية المساهمة](#-كيفية-المساهمة)
- [أنواع المساهمات](#-أنواع-المساهمات-المرحب-بها)
- [إعداد بيئة التطوير](#-إعداد-بيئة-التطوير)
- [عملية الـ Pull Request](#-عملية-الـ-pull-request)
- [معايير الكود](#-معايير-الكود)
- [الاختبارات](#-الاختبارات)
- [التوثيق](#-التوثيق)
- [الإبلاغ عن Bugs](#-الإبلاغ-عن-bugs)
- [طلب ميزات جديدة](#-طلب-ميزات-جديدة)

---

## 📜 قواعد السلوك

### يجب على جميع المساهمين:

- ✅ الاحترام المتبادل
- ✅ التواصل البنّاء
- ✅ قبول النقد البنّاء
- ✅ التركيز على الأهداف البحثية
- ❌ عدم التمييز أو التنمر
- ❌ عدم نشر معلومات شخصية

---

## 🚀 كيفية المساهمة

### 1. Fork المشروع

اذهب إلى المستودع المناسب واضغط "Fork":

- **Backend API**: https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API
- **Frontend Web**: https://github.com/HazemAlhajIhmid/Master-Thesis--CardioGuard---Early-Detection-of-Heart-Disease-System
- **Android App**: https://github.com/HazemAlhajIhmid/CardioGuard-Android-App

### 2. Clone المستودع

```bash
# Clone مستودعك (الـ Fork)
git clone https://github.com/YOUR-USERNAME/CardioGuard-Backend-API.git
cd CardioGuard-Backend-API

# أضف upstream
git remote add upstream https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API.git
```

### 3. أنشئ Branch جديد

```bash
# احصل على آخر التحديثات
git checkout main
git pull upstream main

# أنشئ branch جديد
git checkout -b feature/your-feature-name

# أمثلة على أسماء Branches:
# - feature/add-neural-network-model
# - fix/api-timeout-issue
# - docs/improve-readme
# - test/add-unit-tests
```

### 4. قم بالتعديلات

- اكتب كود نظيف ومُنظم
- اتبع معايير الكود (انظر القسم أدناه)
- أضف اختبارات للميزات الجديدة
- حدّث التوثيق

### 5. Commit التغييرات

```bash
# أضف الملفات
git add .

# Commit مع رسالة واضحة
git commit -m "✨ Add: Neural Network model implementation

- Implemented NN model with TensorFlow.NET
- Added unit tests for NN predictions
- Updated API documentation
- Improved accuracy to 88%

Closes #123"
```

#### رسائل Commit المفضلة:

- `✨ Add: ميزة جديدة`
- `🐛 Fix: إصلاح bug`
- `📚 Docs: تحديث التوثيق`
- `♻️ Refactor: إعادة هيكلة`
- `✅ Test: إضافة اختبارات`
- `🎨 Style: تحسينات UI/UX`
- `⚡ Perf: تحسين الأداء`
- `🔒 Security: تحسينات أمنية`

### 6. Push للـ Fork

```bash
git push origin feature/your-feature-name
```

### 7. افتح Pull Request

1. اذهب إلى مستودعك على GitHub
2. اضغط "Compare & pull request"
3. املأ نموذج الـ PR (انظر القالب أدناه)
4. انتظر المراجعة

---

## 🎯 أنواع المساهمات المرحب بها

### 1. 🤖 نماذج Machine Learning جديدة

```csharp
// مثال: إضافة نموذج Random Forest
public class RandomForestModelService : IModelService
{
    public PredictionResult Predict(HeartDiseaseData input)
    {
        // Implementation
    }
}
```

**متطلبات:**
- دقة لا تقل عن 70%
- Confusion Matrix
- مقاييس الأداء (Accuracy, Precision, Recall)
- Unit Tests شاملة

### 2. 🐛 إصلاح Bugs

- ابحث في [Issues](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- اختر issue مع label `bug`
- تأكد من عدم وجود PR مفتوح لنفس الـ bug
- اكتب اختبارات تثبت الإصلاح

### 3. 📚 تحسين التوثيق

- إصلاح أخطاء إملائية
- توضيح أقسام غير واضحة
- إضافة أمثلة عملية
- ترجمة لغات جديدة

### 4. ✅ إضافة اختبارات

```csharp
[Fact]
public void Predict_HighRiskPatient_ReturnsHighRisk()
{
    // Arrange
    var service = new KNNModelService();
    var input = new HeartDiseaseData { Age = 65, /* ... */ };
    
    // Act
    var result = service.Predict(input);
    
    // Assert
    Assert.True(result.RiskScore > 0.6);
}
```

### 5. 🌐 ترجمات

- إضافة لغات جديدة للواجهة
- ترجمة التوثيق
- ضمان الدقة الطبية للمصطلحات

### 6. 🎨 تحسينات UI/UX

- تصميمات محسّنة
- تجربة مستخدم أفضل
- إمكانية الوصول (Accessibility)

---

## 💻 إعداد بيئة التطوير

### Backend API (.NET 8.0)

```powershell
# المتطلبات
# - .NET SDK 8.0+
# - Visual Studio 2022 or VS Code

# Clone المشروع
git clone https://github.com/YOUR-USERNAME/CardioGuard-Backend-API.git
cd CardioGuard-Backend-API/HeartDiseaseAPI

# استعادة التبعيات
dotnet restore

# بناء المشروع
dotnet build

# تشغيل الاختبارات
dotnet test

# تشغيل API
dotnet run

# الوصول
# API: http://localhost:5000
# Swagger: http://localhost:5000/swagger
```

### Frontend Web (SvelteKit)

```bash
# المتطلبات
# - Node.js 20+
# - npm or pnpm

# Clone المشروع
git clone https://github.com/YOUR-USERNAME/Master-Thesis--CardioGuard--....git
cd frontend

# تثبيت التبعيات
npm install

# تشغيل Dev Server
npm run dev

# الوصول: http://localhost:5173
```

### Android App (Kotlin)

```bash
# المتطلبات
# - Android Studio
# - JDK 17+
# - Kotlin 2.0+

# Clone المشروع
git clone https://github.com/YOUR-USERNAME/CardioGuard-Android-App.git

# افتح في Android Studio
# انتظر Gradle Sync
# اضغط Run ▶️
```

---

## 🔄 عملية الـ Pull Request

### قالب Pull Request

```markdown
## 📝 الوصف
وصف واضح للتغييرات

## 🎯 نوع التغيير
- [ ] ✨ ميزة جديدة
- [ ] 🐛 إصلاح bug
- [ ] 📚 تحديث توثيق
- [ ] ♻️ Refactoring
- [ ] ✅ إضافة اختبارات
- [ ] ⚡ تحسين أداء

## 🧪 الاختبار
كيف تم اختبار التغييرات؟

- [ ] Unit Tests
- [ ] Integration Tests
- [ ] Manual Testing

## 📸 لقطات شاشة (إن وجدت)

## ✅ Checklist
- [ ] الكود يتبع معايير المشروع
- [ ] تم إضافة/تحديث الاختبارات
- [ ] تم تحديث التوثيق
- [ ] جميع الاختبارات تعمل
- [ ] لا توجد warnings
```

### مراحل المراجعة

1. **Auto Checks**: GitHub Actions تفحص الكود
2. **Code Review**: المشرفون يراجعون الكود
3. **Testing**: اختبار الميزات الجديدة
4. **Approval**: موافقة وDerge

### معايير القبول

- ✅ جميع الاختبارات تعمل
- ✅ لا توجد Merge Conflicts
- ✅ الكود نظيف ومُوثّق
- ✅ يتبع معايير المشروع
- ✅ حُدّث التوثيق

---

## 📏 معايير الكود

### C# (Backend)

```csharp
// ✅ جيد
public class PredictionService
{
    private readonly IKNNService _knnService;
    
    public PredictionService(IKNNService knnService)
    {
        _knnService = knnService ?? throw new ArgumentNullException(nameof(knnService));
    }
    
    /// <summary>
    /// Predicts heart disease risk based on patient data
    /// </summary>
    /// <param name="data">Patient medical data</param>
    /// <returns>Prediction result with risk score</returns>
    public async Task<PredictionResult> PredictAsync(HeartDiseaseData data)
    {
        // Validate input
        if (data == null) throw new ArgumentNullException(nameof(data));
        
        // Perform prediction
        var result = await _knnService.PredictAsync(data);
        
        return result;
    }
}
```

**القواعد:**
- PascalCase للـ Classes, Methods, Properties
- camelCase للـ parameters, local variables
- `_camelCase` للـ private fields
- XML comments للـ public methods
- استخدم `async/await` للعمليات غير المتزامنة

### TypeScript (Frontend)

```typescript
// ✅ جيد
interface PredictionRequest {
  age: number;
  sex: number;
  // ... other fields
}

async function predictHeartDisease(data: PredictionRequest): Promise<PredictionResult> {
  try {
    const response = await fetch(`${API_URL}/predict`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });
    
    if (!response.ok) {
      throw new Error(`API Error: ${response.statusText}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Prediction failed:', error);
    throw error;
  }
}
```

**القواعد:**
- camelCase للـ functions, variables
- PascalCase للـ interfaces, types, classes
- استخدم Types بشكل صريح
- معالجة الأخطاء دائماً

### Kotlin (Android)

```kotlin
// ✅ جيد
class PredictionViewModel : ViewModel() {
    private val _predictionResult = MutableStateFlow<PredictionResult?>(null)
    val predictionResult: StateFlow<PredictionResult?> = _predictionResult.asStateFlow()
    
    /**
     * Predicts heart disease risk
     */
    fun predictRisk(data: HeartDiseaseData) {
        viewModelScope.launch {
            try {
                val result = apiService.predict(data)
                _predictionResult.value = result
            } catch (e: Exception) {
                Log.e(TAG, "Prediction failed", e)
                // Handle error
            }
        }
    }
    
    companion object {
        private const val TAG = "PredictionViewModel"
    }
}
```

**القواعد:**
- camelCase للـ functions, properties
- PascalCase للـ classes
- استخدم Kotlin idioms (`?.`, `?.let`, etc.)
- Coroutines للعمليات غير المتزامنة

---

## 🧪 الاختبارات

### Backend Tests

```csharp
[Fact]
public void KNN_Predict_HighRisk_ReturnsCorrectResult()
{
    // Arrange
    var service = new KNNModelService();
    var input = new HeartDiseaseData
    {
        Age = 65,
        Sex = 1, 
        CP = 3,
        TrestBPS = 160,
        // ... high risk values
    };
    
    // Act
    var result = service.Predict(input);
    
    // Assert
    Assert.True(result.Prediction);
    Assert.InRange(result.Confidence, 70, 100);
}
```

### تشغيل الاختبارات

```bash
# جميع الاختبارات
dotnet test

# اختبار محدد
dotnet test --filter "FullyQualifiedName~KNN"

# مع Coverage
dotnet test /p:CollectCoverage=true
```

---

## 📚 التوثيق

### متطلبات التوثيق

1. **XML Comments** لجميع public methods
2. **README** محدّث
3. **API Documentation** في Swagger
4. **Code Comments** للأجزاء المعقدة

### مثال توثيق API

```csharp
/// <summary>
/// Predicts heart disease risk using ensemble of three models
/// </summary>
/// <param name="data">Patient medical data (13 features)</param>
/// <returns>
/// Prediction result containing:
/// - Risk score (0-1)
/// - Risk level (low/moderate/high)
/// - Individual model predictions
/// </returns>
/// <response code="200">Successfully predicted</response>
/// <response code="400">Invalid input data</response>
/// <response code="500">Internal server error</response>
[HttpPost("predict")]
[ProducesResponseType(typeof(PredictionResult), 200)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
public async Task<IActionResult> Predict([FromBody] HeartDiseaseData data)
{
    // Implementation
}
```

---

## 🐛 الإبلاغ عن Bugs

### قبل الإبلاغ

- ابحث في [Issues الموجودة](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- تأكد أنه bug وليس سوء استخدام

### قالب Bug Report

```markdown
## 🐛 وصف Bug
وصف واضح وموجز للمشكلة

## 📋 خطوات إعادة الإنتاج
1. اذهب إلى '...'
2. اضغط على '...'
3. أدخل '...'
4. لاحظ الخطأ

## ✅ السلوك المتوقع
ما كنت تتوقع أن يحدث

## ❌ السلوك الفعلي
ما حدث فعلاً

## 📸 لقطات شاشة
إن وجدت

## 💻 البيئة
- OS: [e.g. Windows 11]
- .NET Version: [e.g. 8.0.1]
- Browser: [e.g. Chrome 120]

## 📝 معلومات إضافية
أي سياق يمكن أن يساعد
```

---

## ✨ طلب ميزات جديدة

### قالب Feature Request

```markdown
## 🚀 الميزة المقترحة
وصف واضح للميزة

## 🎯 المشكلة المُحلّة
ما المشكلة التي ستحلها هذه الميزة؟

## 💡 الحل المقترح  
كيف تتصور تنفيذ هذه الميزة؟

## 🔄 البدائل
هل فكرت في بدائل أخرى؟

## 📊 الأثر
- على المستخدمين:
- على الأداء:
- على المعمارية:

## ✅ معايير القبول
- [ ] معيار 1
- [ ] معيار 2
```

---

## 🏆 المساهمون المميزون

نشكر جميع المساهمين في المشروع! 🙏

<div align="center">

<!-- سيتم إضافتهم automatically من GitHub -->
<a href="https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=HazemAlhajIhmid/CardioGuard-Backend-API" />
</a>

</div>

---

## 📞 تحتاج مساعدة؟

- 💬 **GitHub Discussions**: للأسئلة العامة
- 🐛 **GitHub Issues**: للمشاكل التقنية
- 📧 **Email**: Hazem_82763@svuonline.org

---

## 🙏 شكراً لك!

مساهمتك تساعد في تحسين الرعاية الصحية وإنقاذ الأرواح ❤️

---

**تاريخ آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0
