namespace Exercises_API.Presentation.Models;

using Exercises_API.Core.Models;

public class ExerciseContent
{
    public Exercise? Exercise { get; set; }
    public string? ImageFileName { get; set; }
    public byte[]? ImageFileContent { get; set; }
}