using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;
using Architect.PersonFeature.Services;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Controllers.PersonFeature
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PeopleController : ApiController
    {
        private readonly IPersonTransactionalService service;

        public PeopleController(IPersonTransactionalService service)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required]int id, CancellationToken token)
        {
            var result = await service.GetAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([Required]CreatePersonRequest model, CancellationToken token)
        {
            var result = await service.CreateAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([Required]UpdatePersonRequest model, CancellationToken token)
        {
            var result = await service.UpdateAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpPatch("address")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAddress([Required]ChangeAddressRequest model, CancellationToken token)
        {
            var result = await service.ChangeAddressAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpPatch("name")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchName([Required]ChangeNameRequest model, CancellationToken token)
        {
            var result = await service.ChangeNameAsync(model, token);

            return GenerateResponse(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([Required]DeletePersonRequest model, CancellationToken token)
        {
            var result = await service.DeleteAsync(model, token);

            return GenerateResponse(result);
        }
    }
}