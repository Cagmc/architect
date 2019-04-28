using System.ComponentModel.DataAnnotations;
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
        [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required]int id, CancellationToken token)
        {
            var result = await service.GetAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([Required]CreatePersonRequest model, CancellationToken token)
        {
            var result = await service.CreateAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([Required]UpdatePersonRequest model, CancellationToken token)
        {
            var result = await service.UpdateAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpPatch("address")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAddress([Required]ChangeAddressRequest model, CancellationToken token)
        {
            var result = await service.ChangeAddressAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([Required]DeletePersonRequest model, CancellationToken token)
        {
            var result = await service.DeleteAsync(model, token);

            return GenerateResponse(result);
        }

        protected virtual IActionResult GenerateResponse(IStatusResponse response, bool isCreated = false)
        {
            if (response.IsSuccess)
            {
                if (isCreated)
                {
                    return CreatedAtAction(nameof(Get), new { id = response.EntityId });
                }
                else
                {
                    return Ok(response.EntityId);
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }

        protected virtual IActionResult GenerateResponse<T>(IDataResponse<T> response)
        {
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }
    }
}