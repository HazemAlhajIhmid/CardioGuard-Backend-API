using Microsoft.ML.Data;

namespace HeartDiseaseAPI.Models
{
    public class HeartDiseaseData
    {
        [LoadColumn(0)]
        public float Age { get; set; }

        [LoadColumn(1)]
        public float Sex { get; set; }

        [LoadColumn(2)]
        public float CP { get; set; }

        [LoadColumn(3)]
        public float TrestBPS { get; set; }

        [LoadColumn(4)]
        public float Chol { get; set; }

        [LoadColumn(5)]
        public float FBS { get; set; }

        [LoadColumn(6)]
        public float RestECG { get; set; }

        [LoadColumn(7)]
        public float Thalach { get; set; }

        [LoadColumn(8)]
        public float Exang { get; set; }

        [LoadColumn(9)]
        public float Oldpeak { get; set; }

        [LoadColumn(10)]
        public float Slope { get; set; }

        [LoadColumn(11)]
        public float CA { get; set; }

        [LoadColumn(12)]
        public float Thal { get; set; }

        [LoadColumn(13)]
        [ColumnName("Label")]
        public bool Target { get; set; }
    }

    public class HeartDiseasePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }

    public class PredictionRequest
    {
        public float Age { get; set; }
        public int Sex { get; set; }
        public int CP { get; set; }
        public float TrestBPS { get; set; }
        public float Chol { get; set; }
        public int FBS { get; set; }
        public int RestECG { get; set; }
        public float Thalach { get; set; }
        public int Exang { get; set; }
        public float Oldpeak { get; set; }
        public int Slope { get; set; }
        public int CA { get; set; }
        public int Thal { get; set; }
    }

    public class PredictionResponse
    {
        public KNNResult KNN { get; set; } = new();
        public NaiveBayesResult NaiveBayes { get; set; } = new();
        public DecisionTreeResult DecisionTree { get; set; } = new();
        public EnsembleResult Ensemble { get; set; } = new();
    }

    public class ModelResult
    {
        public bool Prediction { get; set; }
        public double Confidence { get; set; }
        public int Accuracy { get; set; }
    }

    public class KNNResult : ModelResult
    {
        public KNNResult()
        {
            Accuracy = 82;
        }
    }

    public class NaiveBayesResult : ModelResult
    {
        public NaiveBayesResult()
        {
            Accuracy = 82;
        }
    }

    public class DecisionTreeResult : ModelResult
    {
        public DecisionTreeResult()
        {
            Accuracy = 70;
        }
    }

    public class EnsembleResult
    {
        public bool Prediction { get; set; }
        public string RiskLevel { get; set; } = "low";
        public double RiskScore { get; set; }
    }

    public class ModelMetrics
    {
        public string ModelName { get; set; } = string.Empty;
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
    }
}
