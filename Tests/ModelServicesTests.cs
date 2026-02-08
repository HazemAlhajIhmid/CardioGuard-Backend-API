using System;
using Xunit;
using HeartDiseaseAPI.Services;
using HeartDiseaseAPI.Models;

namespace HeartDiseaseAPI.Tests
{
    public class ModelServicesTests
    {
        [Fact]
        public void DecisionTree_Predict_ReturnsResult()
        {
            var service = new DecisionTreeModelService();
            var input = new HeartDiseaseData { /* ضع بيانات اختبار مناسبة هنا */ };
            var result = service.Predict(input);
            Assert.NotNull(result);
        }

        [Fact]
        public void KNN_Predict_ReturnsResult()
        {
            var service = new KNNModelService();
            var input = new HeartDiseaseData { /* ضع بيانات اختبار مناسبة هنا */ };
            var result = service.Predict(input);
            Assert.NotNull(result);
        }

        [Fact]
        public void NaiveBayes_Predict_ReturnsResult()
        {
            var service = new NaiveBayesModelService();
            var input = new HeartDiseaseData { /* ضع بيانات اختبار مناسبة هنا */ };
            var result = service.Predict(input);
            Assert.NotNull(result);
        }
    }
}
