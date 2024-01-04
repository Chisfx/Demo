using AspNetCoreHero.Results;
using AutoMapper;
using Demo.Application.DTOs;
using Demo.Application.Interfaces.Repositories;
using Demo.Application.Interfaces.Shared;
using Demo.Domain.Entities;
using MediatR;
namespace Demo.Application.Features.Commands
{
    public class UpdateUserCommand : IRequest<Result<UserModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Age { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserModel>>
        {
            private readonly IRepositoryAsync<User> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ICompareObject _compare;
            public UpdateUserCommandHandler(
                IRepositoryAsync<User> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ICompareObject compare)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _compare = compare;
            }

            public async Task<Result<UserModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    UserModel result = null;

                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);
               
                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }
                    else
                    {
                        var exist = await _repository.AnyAsync(p => p.Id != request.Id && p.Email == request.Email);
                        if (exist)
                        {
                            msg = $"Email {request.Email} already exist.";
                        }
                        else
                        {
                            if (_compare.Compare(_mapper.Map<UserModel>(entity), _mapper.Map<UserModel>(request)))
                            {
                                msg = "Must update records.";
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        _mapper.Map(request, entity);

                        await _repository.UpdateAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);

                        if (entity.Id == 0) throw new Exception("Database Error");

                        result = _mapper.Map<UserModel>(entity);
                    }

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<UserModel>.SuccessAsync(result,msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                    return await Result<UserModel>.FailAsync(ex.Message);
                }
            }
        }

    }
}
