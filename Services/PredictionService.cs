using HeartDiseaseAPI.Models;

namespace HeartDiseaseAPI.Services
{
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
            // Convert request to HeartDiseaseData
            var inputData = new HeartDiseaseData
            {
                Age = request.Age,
                Sex = request.Sex,
                CP = request.CP,
                TrestBPS = request.TrestBPS,
                Chol = request.Chol,
                FBS = request.FBS,
                RestECG = request.RestECG,
                Thalach = request.Thalach,
                Exang = request.Exang,
                Oldpeak = request.Oldpeak,
                Slope = request.Slope,
                CA = request.CA,
                Thal = request.Thal
            };

            // Get predictions from all three models
            var knnPrediction = _knnService.Predict(inputData);
            var naiveBayesPrediction = _naiveBayesService.Predict(inputData);
            var decisionTreePrediction = _decisionTreeService.Predict(inputData);

            // Create response
            var response = new PredictionResponse
            {
                KNN = new KNNResult
                {
                    Prediction = knnPrediction.Prediction,
                    Confidence = knnPrediction.Probability * 100
                },
                NaiveBayes = new NaiveBayesResult
                {
                    Prediction = naiveBayesPrediction.Prediction,
                    Confidence = naiveBayesPrediction.Probability * 100
                },
                DecisionTree = new DecisionTreeResult
                {
                    Prediction = decisionTreePrediction.Prediction,
                    Confidence = decisionTreePrediction.Probability * 100
                }
            };

            // Ensemble prediction (voting)
            int positiveVotes = 0;
            if (response.KNN.Prediction) positiveVotes++;
            if (response.NaiveBayes.Prediction) positiveVotes++;
            if (response.DecisionTree.Prediction) positiveVotes++;

            response.Ensemble.Prediction = positiveVotes >= 2;

            // Calculate risk score (weighted average based on model accuracy)
            double riskScore = (
                (response.KNN.Confidence * 0.82) +
                (response.NaiveBayes.Confidence * 0.82) +
                (response.DecisionTree.Confidence * 0.70)
            ) / 2.34; // Sum of weights

            // Reverse the logic since models are predicting opposite (high probability for low risk cases)
            riskScore = 100 - riskScore;

            response.Ensemble.RiskScore = riskScore;

            // Determine risk level
            if (riskScore > 70)
                response.Ensemble.RiskLevel = "high";
            else if (riskScore > 40)
                response.Ensemble.RiskLevel = "moderate";
            else
                response.Ensemble.RiskLevel = "low";

            return response;
        }

        public List<ModelMetrics> GetModelMetrics()
        {
            return new List<ModelMetrics>
            {
                new ModelMetrics
                {
                    ModelName = "KNN",
                    Accuracy = 82.0,
                    Precision = 78.0,
                    Recall = 94.0,
                    F1Score = 85.0
                },
                new ModelMetrics
                {
                    ModelName = "Naive Bayes",
                    Accuracy = 82.0,
                    Precision = 79.0,
                    Recall = 91.0,
                    F1Score = 85.0
                },
                new ModelMetrics
                {
                    ModelName = "Decision Tree",
                    Accuracy = 70.0,
                    Precision = 70.0,
                    Recall = 79.0,
                    F1Score = 74.0
                }
            };
        }
    }
}
