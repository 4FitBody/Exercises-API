namespace Exercises_API.Core.Repositories;

using Exercises_API.Core.Models;

public interface IExerciseRepository
{
    IEnumerable<Exercise>? GetAll();
    Task CreateAsync(Exercise exercise);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, Exercise exercise);
    Task<Exercise> GetByIdAsync(int id);
    Task ApproveAsync(int id);
}