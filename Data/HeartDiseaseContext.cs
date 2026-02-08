using Microsoft.EntityFrameworkCore;
using HeartDiseaseAPI.Models;

namespace HeartDiseaseAPI.Data
{
    public class HeartDiseaseContext : DbContext
    {
        public HeartDiseaseContext(DbContextOptions<HeartDiseaseContext> options)
            : base(options)
        {
        }

        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<PredictionHistory> PredictionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<PredictionHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.PredictedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.PredictionHistories)
                    .HasForeignKey(e => e.PatientId);
            });
        }
    }

    public class PatientRecord
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public int Age { get; set; }
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
        public DateTime CreatedAt { get; set; }

        public ICollection<PredictionHistory> PredictionHistories { get; set; } = new List<PredictionHistory>();
    }

    public class PredictionHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool KNNPrediction { get; set; }
        public double KNNConfidence { get; set; }
        public bool NaiveBayesPrediction { get; set; }
        public double NaiveBayesConfidence { get; set; }
        public bool DecisionTreePrediction { get; set; }
        public double DecisionTreeConfidence { get; set; }
        public bool EnsemblePrediction { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public double RiskScore { get; set; }
        public DateTime PredictedAt { get; set; }

        public PatientRecord Patient { get; set; } = null!;
    }
}
