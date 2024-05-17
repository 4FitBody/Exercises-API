namespace Exercises_API.Presentation.Controllers;

using MediatR;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Exercises_API.Core.Models;
using Microsoft.Extensions.Options;
using Exercises_API.Presentation.Dtos;
using Exercises_API.Presentation.Models;
using Exercises_API.Presentation.Options;
using Exercises_API.Infrastructure.Queries;
using Exercises_API.Infrastructure.Commands;
using Exercises_API.Infrastructure.Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class ExerciseController : ControllerBase
{
    private readonly ISender sender;
    private readonly BlobContainerService blobContainerService;

    public ExerciseController(ISender sender, IOptions<BlobOptions> blobOptions)
    {
        this.sender = sender;

        this.blobContainerService = new BlobContainerService(blobOptions.Value.Url, blobOptions.Value.ContainerName);
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var exercises = await this.sender.Send(getAllQuery);

        return base.Ok(exercises);
    }

    [HttpGet]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var exercise = await this.sender.Send(getByIdQuery);

        return base.Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> Create(object exerciseContentJson)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var exerciseContent = JsonConvert.DeserializeObject<ExerciseContent>(exerciseContentJson.ToString()!, settings);  

        var imageFileName = exerciseContent!.ImageFileName;

        var imageFileData = exerciseContent.ImageFileContent;

        var rawPath = Guid.NewGuid().ToString() + imageFileName;

        var path = rawPath.Replace(" ", "%20");

        var exercise = new Exercise
        {
            Name = exerciseContent.Exercise!.Name,
            BodyPart = exerciseContent.Exercise.BodyPart,
            Equipment = exerciseContent.Exercise.Equipment,
            Target = exerciseContent.Exercise.Target,
            IsApproved = false,
            GifUrl = "https://4fitbodystorage.blob.core.windows.net/exercise-images/" + path,
            SecondaryMuscles = exerciseContent.Exercise.SecondaryMuscles!,
            Instructions = exerciseContent.Exercise.Instructions!,
        };

        await this.blobContainerService.UploadAsync(new MemoryStream(imageFileData!), rawPath);

        var createCommand = new CreateCommand(exercise);

        await this.sender.Send(createCommand);

        return base.Ok();
    }

    [HttpDelete]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return base.Ok();
    }

    [HttpPut]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExerciseDto exerciseDto)
    {
        var exercise = new Exercise
        {
            Name = exerciseDto.Name,
            BodyPart = exerciseDto.BodyPart,
            Equipment = exerciseDto.Equipment,
            Target = exerciseDto.Target,
            IsApproved = false,
            SecondaryMuscles = exerciseDto.SecondaryMuscles!,
            Instructions = exerciseDto.Instructions!,
        };

        var createCommand = new UpdateCommand(id, exercise);

        await this.sender.Send(createCommand);

        return base.Ok();
    }
}