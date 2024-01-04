using AspNetCoreHero.Results;
using AutoMapper;
using Demo.Application.Interfaces.Repositories;
using Demo.Domain.Entities;
using MediatR;
namespace Demo.Application.Features.Commands
{
    public class CreateUserCommand : IRequest<Result<int>>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Age { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
        {
            private readonly IRepositoryAsync<User> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            public CreateUserCommandHandler(
                IRepositoryAsync<User> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    int result = 0;
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var exist = await _repository.AnyAsync(p => p.Email == request.Email);
                    if (exist)
                    {
                        msg = $"Email {request.Email} already exist.";
                    }
                    else
                    {
                        var entity = _mapper.Map<User>(request);

                        await _repository.AddAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);

                        if (entity.Id == 0) throw new Exception("Database Error");

                        result = entity.Id;
                    }

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<int>.SuccessAsync(result, msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                    return await Result<int>.FailAsync(ex.Message);
                }
            }
        }

    }
}
