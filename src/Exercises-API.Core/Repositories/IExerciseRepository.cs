namespace Exercises_API.Core.Repositories;

using Exercises_API.Core.Models;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>?> GetAllAsync();
    Task CreateAsync(Exercise exercise);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, Exercise exercise);
    Task<Exercise> GetByIdAsync(int id);
}