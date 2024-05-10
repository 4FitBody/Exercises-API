namespace Exercises_API.Infrastructure.Handlers;

using MediatR;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Commands;

public class ApproveHandler : IRequestHandler<ApproveCommand>
{
    private readonly IExerciseRepository exerciseRepository;

    public ApproveHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task Handle(ApproveCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        await this.exerciseRepository.ApproveAsync((int)request.Id);
    }
}