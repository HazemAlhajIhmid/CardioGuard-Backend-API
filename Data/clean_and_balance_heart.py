import pandas as pd
from sklearn.utils import resample

# تحميل البيانات
df = pd.read_csv('heart.csv')

# --- تصحيح يدوي لبعض الحالات الخطرة ---
# إذا كان العمر >= 60 وعدد الأوعية 2 أو 3 أو الثاليوم 2 أو 3 أو ميل ST هابط، غيّر target إلى 1
dangerous = (
    (df['age'] >= 60) & ((df['ca'] >= 2) | (df['thal'] >= 2) | (df['slope'] == 2))
)
df.loc[dangerous, 'target'] = 1

# يمكنك إضافة شروط أخرى حسب خبرتك الطبية أو منطقك

# --- موازنة البيانات ---
df_majority = df[df.target == 0]
df_minority = df[df.target == 1]

df_minority_upsampled = resample(
    df_minority,
    replace=True,
    n_samples=len(df_majority),
    random_state=42
)

df_balanced = pd.concat([df_majority, df_minority_upsampled])

# خلط البيانات بعد الدمج
df_balanced = df_balanced.sample(frac=1, random_state=42).reset_index(drop=True)

# حفظ البيانات الجديدة
df_balanced.to_csv('heart_balanced.csv', index=False)

print('تم تنظيف وموازنة البيانات وحفظها في heart_balanced.csv')