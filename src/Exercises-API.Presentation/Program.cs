using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Data;
using Exercises_API.Presentation.Options;
using Exercises_API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var blobOptionsSection = builder.Configuration.GetSection("BlobOptions");

var blobOptions = blobOptionsSection.Get<BlobOptions>() ?? throw new Exception("Couldn't create jwt options object");

builder.Services.Configure<BlobOptions>(blobOptionsSection);

var infrastructureAssembly = typeof(ExercisesDbContext).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

builder.Services.AddScoped<IExerciseRepository, ExerciseSqlRepository>();

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("ExercisesDb");

builder.Services.AddDbContext<ExercisesDbContext>(dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseNpgsql(connectionString, o =>
    {
        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    const string scheme = "Bearer";

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "4FitBody (api for working staff)",
        Version = "v1"
    });

    options.AddSecurityDefinition(
        name: scheme,

        new OpenApiSecurityScheme() {
            Description = "Enter here jwt token with Bearer",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = scheme
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement() {
            {
                new OpenApiSecurityScheme() {
                    Reference = new OpenApiReference() {
                        Id = scheme,
                        Type = ReferenceType.SecurityScheme
                    }
                } ,
                new string[] {}
            }
        }
    );
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options => {
    options.AddPolicy("BlazorWasmPolicy", corsBuilder => {
        corsBuilder
            .WithOrigins("http://localhost:5160")
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