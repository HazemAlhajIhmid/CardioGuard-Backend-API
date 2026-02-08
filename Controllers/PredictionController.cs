using Microsoft.AspNetCore.Mvc;
using HeartDiseaseAPI.Models;
using HeartDiseaseAPI.Services;

namespace HeartDiseaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _predictionService;
        private readonly ILogger<PredictionController> _logger;

        public PredictionController(
            PredictionService predictionService,
            ILogger<PredictionController> logger)
        {
            _predictionService = predictionService;
            _logger = logger;
        }

        /// <summary>
        /// Predict heart disease risk based on patient data
        /// </summary>
        /// <param name="request">Patient medical data</param>
        /// <returns>Prediction results from all three models</returns>
        [HttpPost("predict")]
        [ProducesResponseType(typeof(PredictionResponse), 200)]
        [ProducesResponseType(400)]
        public ActionResult<PredictionResponse> Predict([FromBody] PredictionRequest request)
        {
            try
            {
                _logger.LogInformation("Received prediction request for age: {Age}", request.Age);

                // Validate input
                if (request.Age < 20 || request.Age > 100)
                {
                    return BadRequest(new { error = "Age must be between 20 and 100" });
                }

                if (request.TrestBPS < 80 || request.TrestBPS > 200)
                {
                    return BadRequest(new { error = "Blood pressure must be between 80 and 200" });
                }

                if (request.Chol < 100 || request.Chol > 600)
                {
                    return BadRequest(new { error = "Cholesterol must be between 100 and 600" });
                }

                // Get prediction
                var result = _predictionService.PredictHeartDisease(request);

                _logger.LogInformation("Prediction completed. Ensemble prediction: {Prediction}, Risk: {Risk}",
                    result.Ensemble.Prediction, result.Ensemble.RiskLevel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making prediction");
                return StatusCode(500, new { error = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Get model performance metrics
        /// </summary>
        /// <returns>Performance metrics for all models</returns>
        [HttpGet("metrics")]
        [ProducesResponseType(typeof(List<ModelMetrics>), 200)]
        public ActionResult<List<ModelMetrics>> GetMetrics()
        {
            try
            {
                var metrics = _predictionService.GetModelMetrics();
                return Ok(metrics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving metrics");
                return StatusCode(500, new { error = "An error occurred while retrieving metrics" });
            }
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        /// <returns>API health status</returns>
        [HttpGet("health")]
        public ActionResult<object> HealthCheck()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                models = new
                {
                    knn = "loaded",
                    naiveBayes = "loaded",
                    decisionTree = "loaded"
                }
            });
        }
    }
}
