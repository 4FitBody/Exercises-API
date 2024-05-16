namespace Exercises_API.Infrastructure.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Commands;

public class UpdateHandler : IRequestHandler<UpdateCommand>
{
    private readonly IExerciseRepository exerciseRepository;

    public UpdateHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        ArgumentNullException.ThrowIfNull(request.Exercise);

        await this.exerciseRepository.UpdateAsync(request.Id, request.Exercise);
    }
}