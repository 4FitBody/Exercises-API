namespace Exercises_API.Infrastructure.Data;

using Exercises_API.Core.Models;
using Microsoft.EntityFrameworkCore;

public class ExercisesDbContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }

    public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : base(options) { }
}