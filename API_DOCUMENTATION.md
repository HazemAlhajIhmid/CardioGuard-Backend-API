# API Documentation 📝

## CardioGuard Backend API - توثيق شامل للواجهة البرمجية

### 📋 جدول المحتويات

1. [نظرة عامة](#نظرة-عامة)
2. [قاعدة الـ URL](#قاعدة-الـ-url)
3. [المصادقة والأمان](#المصادقة-والأمان)
4. [الـ Endpoints](#الـ-endpoints)
5. [نماذج البيانات](#نماذج-البيانات)
6. [رموز الحالة](#رموز-الحالة)
7. [أمثلة عملية](#أمثلة-عملية)
8. [معالجة الأخطاء](#معالجة-الأخطاء)

---

## نظرة عامة

CardioGuard API هي واجهة برمجية RESTful تقدم خدمات التنبؤ بمخاطر أمراض القلب باستخدام ثلاثة نماذج للتعلم الآلي.

### المعلومات الأساسية

- **الإصدار:** v1.0.0
- **النوع:** RESTful API
- **التنسيق:** JSON
- **البروتوكول:** HTTP/HTTPS
- **المنفذ الافتراضي:** 5000

---

## قاعدة الـ URL

### بيئة التطوير (Development)
```
http://localhost:5000
```

### بيئة الإنتاج (Production)
```
https://cardio-guard-api-prod-b2a0cfdbe9czbkgx.norwayeast-01.azurewebsites.net
```

### Swagger UI
```
http://localhost:5000/swagger        # Development
https://cardio-guard-api-prod-b2a0cfdbe9czbkgx.norwayeast-01.azurewebsites.net/swagger/index.html  # Production
```

---

## المصادقة والأمان

حالياً، الـ API لا تتطلب مصادقة (Authentication) للأغراض البحثية.

### CORS Policy

الـ API تدعم CORS للمصادر التالية:
- `http://localhost:3000`
- `http://localhost:5173`
- يمكن إضافة المزيد في `Program.cs`

### اعتبارات الأمان

⚠️ **ملاحظة مهمة:**
- لا يتم تخزين أي بيانات للمرضى
- كل طلب مستقل تماماً
- لا توجد جلسات (Sessions)
- يوصى باستخدام HTTPS في الإنتاج

---

## الـ Endpoints

### 1. التنبؤ بمخاطر أمراض القلب

**Endpoint:** `POST /api/prediction/predict`

**الوصف:** يتنبأ بمخاطر الإصابة بأمراض القلب باستخدام ثلاثة نماذج للتعلم الآلي.

#### الطلب (Request)

**Headers:**
```http
Content-Type: application/json
```

**Body:**
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

#### الاستجابة (Response)

**حالة النجاح (200):**
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
    "accuracy": 82,
    "probabilityOfDisease": 0.5584
  },
  "naiveBayes": {
    "prediction": false,
    "confidence": 48.23,
    "accuracy": 82,
    "probabilityOfDisease": 0.4823
  },
  "decisionTree": {
    "prediction": false,
    "confidence": 32.56,
    "accuracy": 70,
    "probabilityOfDisease": 0.3256
  }
}
```

**حالة الخطأ (400):**
```json
{
  "error": "Age must be between 20 and 100"
}
```

**حالة خطأ الخادم (500):**
```json
{
  "error": "An error occurred while processing your request"
}
```

#### قواعد التحقق (Validation Rules)

| الحقل | النوع | المدى | إلزامي |
|-------|------|-------|--------|
| age | int | 20-100 | ✅ |
| sex | int | 0-1 | ✅ |
| cp | int | 0-3 | ✅ |
| trestBPS | int | 80-200 | ✅ |
| chol | int | 100-600 | ✅ |
| fbs | int | 0-1 | ✅ |
| restECG | int | 0-2 | ✅ |
| thalach | int | 60-220 | ✅ |
| exang | int | 0-1 | ✅ |
| oldpeak | float | 0-10 | ✅ |
| slope | int | 0-2 | ✅ |
| ca | int | 0-4 | ✅ |
| thal | int | 0-3 | ✅ |

#### مثال cURL

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

#### مثال JavaScript (Fetch API)

```javascript
const predictHeartDisease = async (patientData) => {
  try {
    const response = await fetch('http://localhost:5000/api/prediction/predict', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(patientData)
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const result = await response.json();
    console.log('Prediction Result:', result);
    return result;
  } catch (error) {
    console.error('Error:', error);
  }
};

// استخدام الدالة
const patientData = {
  age: 50,
  sex: 1,
  cp: 1,
  trestBPS: 130,
  chol: 240,
  fbs: 0,
  restECG: 0,
  thalach: 150,
  exang: 0,
  oldpeak: 1.0,
  slope: 1,
  ca: 1,
  thal: 2
};

predictHeartDisease(patientData);
```

#### مثال C#

```csharp
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class PredictionClient
{
    private readonly HttpClient _httpClient;

    public PredictionClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
    }

    public async Task<PredictionResponse> PredictAsync(PredictionRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/prediction/predict", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PredictionResponse>(responseJson);
    }
}
```

#### مثال Python

```python
import requests
import json

def predict_heart_disease(patient_data):
    url = "http://localhost:5000/api/prediction/predict"
    headers = {"Content-Type": "application/json"}
    
    response = requests.post(url, headers=headers, json=patient_data)
    
    if response.status_code == 200:
        return response.json()
    else:
        raise Exception(f"Error: {response.status_code} - {response.text}")

# استخدام الدالة
patient_data = {
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

result = predict_heart_disease(patient_data)
print(json.dumps(result, indent=2))
```

---

### 2. الحصول على مقاييس الأداء

**Endpoint:** `GET /api/prediction/metrics`

**الوصف:** يعيد مقاييس الأداء لجميع النماذج الثلاثة.

#### الطلب (Request)

```http
GET /api/prediction/metrics HTTP/1.1
Host: localhost:5000
```

#### الاستجابة (Response)

**حالة النجاح (200):**
```json
[
  {
    "modelName": "KNN",
    "accuracy": 0.82,
    "precision": 0.84,
    "recall": 0.94,
    "f1Score": 0.89,
    "truePositives": 89,
    "trueNegatives": 74,
    "falsePositives": 15,
    "falseNegatives": 6
  },
  {
    "modelName": "Naive Bayes",
    "accuracy": 0.82,
    "precision": 0.81,
    "recall": 0.83,
    "f1Score": 0.82,
    "truePositives": 79,
    "trueNegatives": 72,
    "falsePositives": 17,
    "falseNegatives": 16
  },
  {
    "modelName": "Decision Tree",
    "accuracy": 0.70,
    "precision": 0.65,
    "recall": 0.75,
    "f1Score": 0.70,
    "truePositives": 71,
    "trueNegatives": 58,
    "falsePositives": 31,
    "falseNegatives": 24
  }
]
```

#### مثال cURL

```bash
curl -X GET "http://localhost:5000/api/prediction/metrics"
```

#### مثال JavaScript

```javascript
const getMetrics = async () => {
  try {
    const response = await fetch('http://localhost:5000/api/prediction/metrics');
    const metrics = await response.json();
    console.log('Model Metrics:', metrics);
    return metrics;
  } catch (error) {
    console.error('Error:', error);
  }
};
```

---

### 3. فحص صحة الـ API

**Endpoint:** `GET /api/prediction/health`

**الوصف:** يفحص حالة الـ API والنماذج المحملة.

#### الطلب (Request)

```http
GET /api/prediction/health HTTP/1.1
Host: localhost:5000
```

#### الاستجابة (Response)

**حالة النجاح (200):**
```json
{
  "status": "healthy",
  "timestamp": "2026-02-08T10:30:00.123Z",
  "version": "1.0.0",
  "models": {
    "knn": "loaded",
    "naiveBayes": "loaded",
    "decisionTree": "loaded"
  },
  "uptime": "2d 5h 32m 15s"
}
```

#### مثال cURL

```bash
curl -X GET "http://localhost:5000/api/prediction/health"
```

---

## نماذج البيانات

### PredictionRequest

```csharp
public class PredictionRequest
{
    public int Age { get; set; }           // العمر (20-100)
    public int Sex { get; set; }           // الجنس (0: أنثى, 1: ذكر)
    public int CP { get; set; }            // نوع ألم الصدر (0-3)
    public int TrestBPS { get; set; }      // ضغط الدم (80-200)
    public int Chol { get; set; }          // الكوليسترول (100-600)
    public int FBS { get; set; }           // سكر الدم الصائم (0-1)
    public int RestECG { get; set; }       // تخطيط القلب (0-2)
    public int Thalach { get; set; }       // معدل ضربات القلب (60-220)
    public int Exang { get; set; }         // الذبحة الصدرية (0-1)
    public float Oldpeak { get; set; }     // انخفاض ST (0-10)
    public int Slope { get; set; }         // ميل ST (0-2)
    public int CA { get; set; }            // عدد الأوعية (0-4)
    public int Thal { get; set; }          // الثلاسيميا (0-3)
}
```

### PredictionResponse

```csharp
public class PredictionResponse
{
    public EnsembleResult Ensemble { get; set; }
    public ModelResult KNN { get; set; }
    public ModelResult NaiveBayes { get; set; }
    public ModelResult DecisionTree { get; set; }
}

public class EnsembleResult
{
    public float RiskScore { get; set; }      // 0.0 - 1.0
    public string RiskLevel { get; set; }     // "low", "moderate", "high"
    public bool Prediction { get; set; }      // true = مرض, false = سليم
    public float Confidence { get; set; }     // نسبة الثقة
}

public class ModelResult
{
    public bool Prediction { get; set; }      // true = مرض, false = سليم
    public float Confidence { get; set; }     // نسبة الثقة
    public int Accuracy { get; set; }         // دقة النموذج (%)
    public float ProbabilityOfDisease { get; set; }  // احتمالية المرض
}
```

### ModelMetrics

```csharp
public class ModelMetrics
{
    public string ModelName { get; set; }
    public double Accuracy { get; set; }
    public double Precision { get; set; }
    public double Recall { get; set; }
    public double F1Score { get; set; }
    public int TruePositives { get; set; }
    public int TrueNegatives { get; set; }
    public int FalsePositives { get; set; }
    public int FalseNegatives { get; set; }
}
```

---

## رموز الحالة

| الرمز | الحالة | الوصف |
|-------|--------|-------|
| 200 | OK | الطلب نجح |
| 400 | Bad Request | خطأ في البيانات المدخلة |
| 404 | Not Found | الـ Endpoint غير موجود |
| 500 | Internal Server Error | خطأ في الخادم |

---

## أمثلة عملية

### مثال 1: حالة مريض بخطر منخفض

**الطلب:**
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

**الاستجابة:**
```json
{
  "ensemble": {
    "riskScore": 0.15,
    "riskLevel": "low",
    "prediction": false,
    "confidence": 85.0
  },
  "knn": {
    "prediction": false,
    "confidence": 88.5,
    "accuracy": 82
  },
  "naiveBayes": {
    "prediction": false,
    "confidence": 82.3,
    "accuracy": 82
  },
  "decisionTree": {
    "prediction": false,
    "confidence": 84.2,
    "accuracy": 70
  }
}
```

### مثال 2: حالة مريض بخطر مرتفع

**الطلب:**
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

**الاستجابة:**
```json
{
  "ensemble": {
    "riskScore": 0.85,
    "riskLevel": "high",
    "prediction": true,
    "confidence": 91.5
  },
  "knn": {
    "prediction": true,
    "confidence": 94.2,
    "accuracy": 82
  },
  "naiveBayes": {
    "prediction": true,
    "confidence": 89.8,
    "accuracy": 82
  },
  "decisionTree": {
    "prediction": true,
    "confidence": 90.5,
    "accuracy": 70
  }
}
```

---

## معالجة الأخطاء

### أنواع الأخطاء

#### 1. خطأ في التحقق من البيانات (400)

```json
{
  "error": "Age must be between 20 and 100"
}
```

**الأسباب المحتملة:**
- العمر خارج المدى المقبول
- ضغط الدم خارج المدى المقبول
- الكوليسترول خارج المدى المقبول

#### 2. خطأ في الخادم (500)

```json
{
  "error": "An error occurred while processing your request"
}
```

**الأسباب المحتملة:**
- فشل في تحميل النموذج
- خطأ في المعالجة الداخلية

### أفضل الممارسات

1. **التحقق من البيانات على جانب العميل:**
```javascript
function validatePatientData(data) {
  if (data.age < 20 || data.age > 100) {
    throw new Error('Age must be between 20 and 100');
  }
  if (data.trestBPS < 80 || data.trestBPS > 200) {
    throw new Error('Blood pressure must be between 80 and 200');
  }
  if (data.chol < 100 || data.chol > 600) {
    throw new Error('Cholesterol must be between 100 and 600');
  }
  // ... المزيد من التحققات
}
```

2. **معالجة الأخطاء بشكل صحيح:**
```javascript
try {
  const result = await predictHeartDisease(patientData);
  displayResults(result);
} catch (error) {
  if (error.response?.status === 400) {
    displayValidationError(error.response.data.error);
  } else {
    displayGenericError('حدث خطأ غير متوقع');
  }
}
```

3. **إعادة المحاولة عند الفشل:**
```javascript
async function predictWithRetry(data, maxRetries = 3) {
  for (let i = 0; i < maxRetries; i++) {
    try {
      return await predictHeartDisease(data);
    } catch (error) {
      if (i === maxRetries - 1) throw error;
      await new Promise(resolve => setTimeout(resolve, 1000 * (i + 1)));
    }
  }
}
```

---

## حدود الاستخدام (Rate Limits)

حالياً، لا توجد حدود للاستخدام لأن الـ API للأغراض البحثية.

⚠️ **ملاحظة:** في الإنتاج، يوصى بإضافة:
- Rate Limiting (مثل 100 طلب/دقيقة)
- API Keys للمصادقة
- Throttling للحماية من الهجمات

---

## Postman Collection

يمكنك استيراد مجموعة Postman التالية للاختبار:

```json
{
  "info": {
    "name": "CardioGuard API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Predict Heart Disease",
      "request": {
        "method": "POST",
        "header": [
          {
            "key": "Content-Type",
            "value": "application/json"
          }
        ],
        "body": {
          "mode": "raw",
          "raw": "{\n  \"age\": 50,\n  \"sex\": 1,\n  \"cp\": 1,\n  \"trestBPS\": 130,\n  \"chol\": 240,\n  \"fbs\": 0,\n  \"restECG\": 0,\n  \"thalach\": 150,\n  \"exang\": 0,\n  \"oldpeak\": 1.0,\n  \"slope\": 1,\n  \"ca\": 1,\n  \"thal\": 2\n}"
        },
        "url": {
          "raw": "http://localhost:5000/api/prediction/predict",
          "protocol": "http",
          "host": ["localhost"],
          "port": "5000",
          "path": ["api", "prediction", "predict"]
        }
      }
    },
    {
      "name": "Get Model Metrics",
      "request": {
        "method": "GET",
        "url": {
          "raw": "http://localhost:5000/api/prediction/metrics",
          "protocol": "http",
          "host": ["localhost"],
          "port": "5000",
          "path": ["api", "prediction", "metrics"]
        }
      }
    },
    {
      "name": "Health Check",
      "request": {
        "method": "GET",
        "url": {
          "raw": "http://localhost:5000/api/prediction/health",
          "protocol": "http",
          "host": ["localhost"],
          "port": "5000",
          "path": ["api", "prediction", "health"]
        }
      }
    }
  ]
}
```

---

## الدعم والمساعدة

- **GitHub Issues:** [افتح Issue جديد](https://github.com/HazemAlhajIhmid/CardioGuard-Backend-API/issues)
- **البريد الإلكتروني:** Hazem_82763@svuonline.org
- **Swagger UI:** http://localhost:5000/swagger

---

**آخر تحديث:** 8 فبراير 2026  
**الإصدار:** 1.0.0
