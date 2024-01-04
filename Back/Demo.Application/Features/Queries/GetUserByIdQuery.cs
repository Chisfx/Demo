using AspNetCoreHero.Results;
using AutoMapper;
using Demo.Application.DTOs;
using Demo.Application.Interfaces.Repositories;
using Demo.Domain.Entities;
using MediatR;
namespace Demo.Application.Features.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserModel>>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserModel>>
        {
            private readonly IRepositoryAsync<User> _repository;
            private readonly IMapper _mapper;
            public GetUserByIdQueryHandler(
                IRepositoryAsync<User> repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<UserModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    UserModel result = null;
                    string msg = string.Empty;

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }
                    else
                    {
                        result = _mapper.Map<UserModel>(entity);
                    }

                    return await Result<UserModel>.SuccessAsync(result, msg);
                }
                catch (Exception ex)
                {
                    return await Result<UserModel>.FailAsync(ex.Message);
                }
            }
        }

    }
}
