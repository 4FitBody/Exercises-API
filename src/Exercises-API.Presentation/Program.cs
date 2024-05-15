using Microsoft.OpenApi.Models;
using Exercises_API.Core.Repositories;
using Exercises_API.Presentation.Options;
using Exercises_API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var blobOptionsSection = builder.Configuration.GetSection("BlobOptions");

var blobOptions = blobOptionsSection.Get<BlobOptions>() ?? throw new Exception("Couldn't create blob options object");

var connectionString = builder.Configuration.GetSection("ExerciseDb").Value;

var databaseName = builder.Configuration.GetSection("Database").Value;

var collectionName = builder.Configuration.GetSection("Collection").Value;

builder.Services.Configure<BlobOptions>(blobOptionsSection);

var infrastructureAssembly = typeof(ExerciseMongoRepository).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

builder.Services.AddScoped<IExerciseRepository>(provider =>
{
    ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

    ArgumentNullException.ThrowIfNullOrWhiteSpace(databaseName, nameof(databaseName));

    ArgumentNullException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

    return new ExerciseMongoRepository(connectionString!, databaseName!, collectionName!);
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "4FitBody (api for working staff)",
        Version = "v1"
    });
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasmPolicy", corsBuilder =>
    {
        corsBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("BlazorWasmPolicy");

app.Run();