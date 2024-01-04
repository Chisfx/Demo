using AspNetCoreHero.Results;
using AutoMapper;
using Bogus;
using Demo.Application.DTOs;
using Demo.Application.Interfaces.Repositories;
using Demo.Domain.Entities;
using MediatR;
namespace Demo.Application.Features.Queries
{
    public class GetAllUserQuery : IRequest<Result<List<UserModel>>>
    {
        public bool faker { get; set; }
        public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserModel>>>
        {
            private readonly IRepositoryAsync<User> _repository;
            private readonly IMapper _mapper;
            public GetAllUserQueryHandler(
                IRepositoryAsync<User> repository,
                IMapper mapper
                )
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<List<UserModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<UserModel> entities;
                    if (request.faker)
                    {
                        var user = new Faker<UserModel>()
                        .RuleFor(c => c.Name, (k, a) => $"{k.Name.FirstName()} {k.Name.LastName()}")
                        .RuleFor(c => c.Email, (k, a) => k.Internet.Email(a.Name))
                        .RuleFor(c => c.Age, k => k.Random.Int(18, 60));

                        entities = user.Generate(100);
                    }
                    else
                    {
                        var response = await _repository.GetAllAsync();
                        entities = _mapper.Map<List<UserModel>>(response);
                    }
                    
                    return await Result<List<UserModel>>.SuccessAsync(entities);
                }
                catch (Exception ex)
                {
                    return await Result<List<UserModel>>.FailAsync(ex.Message);
                }
            }
        }

    }
}
