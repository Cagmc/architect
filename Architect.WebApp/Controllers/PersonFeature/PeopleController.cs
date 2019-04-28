using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;
using Architect.PersonFeature.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Controllers.PersonFeature
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonTransactionalService service;

        public PeopleController(IPersonTransactionalService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IDataResponse<PersonViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id, CancellationToken token)
        {
            var result = await service.GetAsync(id, token);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.ErrorMessage);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(CreatePersonRequest model, CancellationToken token)
        {
            var result = await service.CreateAsync(model, token);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = result.EntityId });
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.ErrorMessage);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(UpdatePersonRequest model, CancellationToken token)
        {
            var result = await service.UpdateAsync(model, token);

            if (result.IsSuccess)
            {
                return Ok(result.EntityId);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.ErrorMessage);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(DeletePersonRequest model, CancellationToken token)
        {
            var result = await service.DeleteAsync(model, token);

            if (result.IsSuccess)
            {
                return Ok(result.EntityId);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.ErrorMessage);
            }
        }
    }
}