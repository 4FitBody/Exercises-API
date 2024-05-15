namespace Exercises_API.Infrastructure.Repositories;

using Exercises_API.Core.Models;
using Exercises_API.Core.Repositories;
using MongoDB.Driver;

public class ExerciseMongoRepository : IExerciseRepository
{
    private readonly IMongoDatabase exercisesDb;
    private readonly IMongoCollection<Exercise> collection;

    public ExerciseMongoRepository(string connestionstring, string db, string collection)
    {
        var client = new MongoClient(connestionstring);

        this.exercisesDb = client.GetDatabase(db);

        this.collection = this.exercisesDb.GetCollection<Exercise>(collection);
    }

    public async Task<IEnumerable<Exercise>?> GetAllAsync()
    {
        var exercises = await this.collection.FindAsync(f => f.IsApproved == false);

        var allexercises = exercises.ToList();

        return allexercises;
    }

    public async Task<Exercise> GetByIdAsync(int id)
    {
        var exercise = await this.collection.FindAsync(e => e.Id == id);

        var searchedExercise = exercise.FirstOrDefault();

        return searchedExercise;
    }

    public async Task CreateAsync(Exercise exercise)
    {
        await this.collection.InsertOneAsync(exercise);
    }

    public async Task DeleteAsync(int id)
    {
        await this.collection.FindOneAndDeleteAsync(exercise => exercise.Id == id);
    }

    public async Task UpdateAsync(int id, Exercise exercise)
    {
        await this.collection.ReplaceOneAsync<Exercise>(filter: ex => ex.Id == id, replacement: exercise);
    }
}