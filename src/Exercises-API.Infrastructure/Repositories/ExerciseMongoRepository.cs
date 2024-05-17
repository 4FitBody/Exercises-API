namespace Exercises_API.Infrastructure.Repositories;

using Exercises_API.Core.Models;
using Exercises_API.Core.Repositories;
using MongoDB.Driver;

public class ExerciseMongoRepository : IExerciseRepository
{
    private readonly IMongoDatabase exercisesDb;
    private readonly IMongoCollection<Exercise> collection;
    private readonly IMongoCollection<ExericseIndex> idCollection;

    public ExerciseMongoRepository(string connestionstring, string db, string collection)
    {
        var client = new MongoClient(connestionstring);

        this.exercisesDb = client.GetDatabase(db);

        this.collection = this.exercisesDb.GetCollection<Exercise>(collection);

        this.idCollection = this.exercisesDb.GetCollection<ExericseIndex>("ExercisesIds");
    }

    public async Task<IEnumerable<Exercise>?> GetAllAsync()
    {
        var exercises = await this.collection.FindAsync(f => f.IsApproved == true);

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
        var lastIndex = this.collection.Find(e => e.Id >= 0).ToList().Last().Id;

        exercise.Id = lastIndex + 1;

        await this.collection.InsertOneAsync(exercise);
    }

    public async Task DeleteAsync(int id)
    {
        await this.collection.FindOneAndDeleteAsync(exercise => exercise.Id == id);
    }

    public async Task UpdateAsync(int id, Exercise exercise)
    {
        exercise.Id = id;

        var oldExercise = await this.GetByIdAsync(id);

        exercise.GifUrl = oldExercise.GifUrl;

        await this.collection.ReplaceOneAsync<Exercise>(filter: ex => ex.Id == id, replacement: exercise);
    }
}