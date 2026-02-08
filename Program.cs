using HeartDiseaseAPI.Services;
using HeartDiseaseAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSvelteApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add Database Context
builder.Services.AddDbContext<HeartDiseaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HeartDiseaseDB")));

// Add ML Services
builder.Services.AddSingleton<KNNModelService>();
builder.Services.AddSingleton<NaiveBayesModelService>();
builder.Services.AddSingleton<DecisionTreeModelService>();
builder.Services.AddScoped<PredictionService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSvelteApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
