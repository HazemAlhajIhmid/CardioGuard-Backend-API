using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using HeartDiseaseAPI.Models;

namespace HeartDiseaseAPI.Services
{
    public class KNNModelService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private readonly string _modelPath = "Models/knn_model.zip";

        public KNNModelService()
        {
            _mlContext = new MLContext(seed: 0);
            LoadOrTrainModel();
        }

        private void LoadOrTrainModel()
        {
            if (File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            }
            else
            {
                TrainModel();
            }
        }

        public void TrainModel()
        {
            // Load data from CSV
            var dataPath = "Data/heart_balanced.csv";
            var data = _mlContext.Data.LoadFromTextFile<HeartDiseaseData>(
                path: dataPath,
                hasHeader: true,
                separatorChar: ',');

            // Split data into training and testing sets (80/20)
            var trainTestSplit = _mlContext.Data.TrainTestSplit(data, testFraction: 0.2, seed: 0);

            // Define data preprocessing pipeline
            var dataProcessPipeline = _mlContext.Transforms.Concatenate("Features",
                nameof(HeartDiseaseData.Age),
                nameof(HeartDiseaseData.Sex),
                nameof(HeartDiseaseData.CP),
                nameof(HeartDiseaseData.TrestBPS),
                nameof(HeartDiseaseData.Chol),
                nameof(HeartDiseaseData.FBS),
                nameof(HeartDiseaseData.RestECG),
                nameof(HeartDiseaseData.Thalach),
                nameof(HeartDiseaseData.Exang),
                nameof(HeartDiseaseData.Oldpeak),
                nameof(HeartDiseaseData.Slope),
                nameof(HeartDiseaseData.CA),
                nameof(HeartDiseaseData.Thal))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"));

            // KNN algorithm with K=5 (can be tuned for better performance)
            var trainer = _mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(
                labelColumnName: "Label",
                featureColumnName: "Features");

            // Create training pipeline
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            // Train the model
            Console.WriteLine("Training KNN model...");
            _model = trainingPipeline.Fit(trainTestSplit.TrainSet);

            // Evaluate the model
            var predictions = _model.Transform(trainTestSplit.TestSet);
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions, "Label");

            Console.WriteLine($"KNN Model Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"KNN Model AUC: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"KNN Model F1Score: {metrics.F1Score:P2}");

            // Save the model
            Directory.CreateDirectory(Path.GetDirectoryName(_modelPath)!);
            _mlContext.Model.Save(_model, trainTestSplit.TrainSet.Schema, _modelPath);
        }

        public HeartDiseasePrediction Predict(HeartDiseaseData input)
        {
            if (_model == null)
            {
                throw new InvalidOperationException("Model is not loaded");
            }
            Console.WriteLine($"[KNN] Input: Age={input.Age}, Sex={input.Sex}, CP={input.CP}, TrestBPS={input.TrestBPS}, Chol={input.Chol}, FBS={input.FBS}, RestECG={input.RestECG}, Thalach={input.Thalach}, Exang={input.Exang}, Oldpeak={input.Oldpeak}, Slope={input.Slope}, CA={input.CA}, Thal={input.Thal}");
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HeartDiseaseData, HeartDiseasePrediction>(_model);
            var result = predictionEngine.Predict(input);
            Console.WriteLine($"[KNN] Output: Prediction={result.Prediction}, Probability={result.Probability}, Score={result.Score}");
            return result;
        }
    }
}
