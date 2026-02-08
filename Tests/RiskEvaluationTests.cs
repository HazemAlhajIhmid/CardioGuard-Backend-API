using System;
using Xunit;
using HeartDiseaseAPI.Services;
using HeartDiseaseAPI.Models;

namespace HeartDiseaseAPI.Tests
{
    public class RiskEvaluationTests
    {
        [Theory]
        [InlineData(44, 1, 1, 120, 263, 0, 1, 173, 0, 0.0, 2, 0, 3, "low")]
        [InlineData(59, 1, 0, 135, 234, 0, 1, 161, 0, 0.5, 1, 0, 3, "moderate")]
        [InlineData(55, 1, 0, 140, 217, 0, 1, 111, 1, 5.6, 0, 0, 3, "high")]
        public void PredictionService_RiskLevel_AndroidBackend_Compatible(float age, float sex, float cp, float trestbps, float chol, float fbs, float restecg, float thalach, float exang, float oldpeak, float slope, float ca, float thal, string expectedRiskLevel)
        {
            var knnService = new KNNModelService();
            var naiveBayesService = new NaiveBayesModelService();
            var decisionTreeService = new DecisionTreeModelService();
            var predictionService = new PredictionService(knnService, naiveBayesService, decisionTreeService);
            var input = new PredictionRequest
            {
                Age = age,
                Sex = (int)sex,
                CP = (int)cp,
                TrestBPS = trestbps,
                Chol = chol,
                FBS = (int)fbs,
                RestECG = (int)restecg,
                Thalach = thalach,
                Exang = (int)exang,
                Oldpeak = oldpeak,
                Slope = (int)slope,
                CA = (int)ca,
                Thal = (int)thal
            };
            var result = predictionService.PredictHeartDisease(input);
            Assert.NotNull(result);
            Assert.NotNull(result.Ensemble.RiskLevel);
            Assert.Equal(expectedRiskLevel, result.Ensemble.RiskLevel);
        }
    }
}
