using Azure.Core;
using Demo.Api.Abstractions;
using Demo.Application.DTOs;
using Demo.Application.Exceptions;
using Demo.Application.Features.Commands;
using Demo.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;
namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<UserController>
    {
        [HttpGet]
        [Route("PostAllTest")]
        public async Task<List<UserModel>> PostAllTestAsync()
        {
            var list = await GetAllAsync(true);
            foreach (var item in list)
            {
                await PostAsync(item);
            }
            return await GetAllAsync();
        }

        [HttpGet]
        [Route("GetAll/{test?}")]
        public async Task<List<UserModel>> GetAllAsync(bool test = false)
        {
            var response = await _mediator.Send(new GetAllUserQuery() { faker = test});

            if (!response.Succeeded)
            {
                throw new ApiException(response.Message);
            }

            return response.Data;
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetAsync(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery() { Id = id });

            if (!response.Succeeded)
            {
                throw new ApiException(response.Message);
            }
            else if (!string.IsNullOrEmpty(response.Message))
            {
                throw new ApiException(response.Message);
            }

            return response.Data;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> PostAsync([FromBody] UserModel model)
        {
            var response = await _mediator.Send(_mapper.Map<CreateUserCommand>(model));

            if (!response.Succeeded)
            {
                throw new ApiException(response.Message);
            }
            else if (!string.IsNullOrEmpty(response.Message))
            {
                throw new ApiException(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> PutAsync([FromBody] UserModel model)
        {
            var response = await _mediator.Send(_mapper.Map<UpdateUserCommand>(model));

            if (!response.Succeeded)
            {
                throw new ApiException(response.Message);
            }
            else if (!string.IsNullOrEmpty(response.Message))
            {
                throw new ApiException(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand() { Id = id });

            if (!response.Succeeded)
            {
                throw new ApiException(response.Message);
            }
            else if (!string.IsNullOrEmpty(response.Message))
            {
                throw new ApiException(response.Message);
            }

            return Ok(response.Data);
        }
    }

}
