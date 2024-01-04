using AspNetCoreHero.Results;
using Demo.Application.Interfaces.Repositories;
using Demo.Domain.Entities;
using MediatR;
namespace Demo.Application.Features.Commands
{
    public class DeleteUserCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
        {
            private readonly IRepositoryAsync<User> _repository;
            private IUnitOfWork _unitOfWork { get; set; }
            public DeleteUserCommandHandler(
                IRepositoryAsync<User> repository,
                IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        await _repository.DeleteAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);
                    }

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<bool>.SuccessAsync(true, msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                    return await Result<bool>.FailAsync(ex.Message);
                }
            }
        }
    }
}
