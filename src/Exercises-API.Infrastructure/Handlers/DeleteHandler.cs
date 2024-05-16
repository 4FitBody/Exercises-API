namespace Exercises_API.Infrastructure.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Commands;

public class DeleteHandler : IRequestHandler<DeleteCommand>
{
    private readonly IExerciseRepository exerciseRepository;

    public DeleteHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        await this.exerciseRepository.DeleteAsync(request.Id);
    }
}