using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.DataTransfer.Response;
using Architect.PersonFeature.Queries;
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
        private readonly IPersonQueries queries;

        public PeopleController(IPersonTransactionalService service, IPersonQueries queries)
        {
            this.service = service.ArgumentNullCheck(nameof(service));
            this.queries = queries.ArgumentNullCheck(nameof(queries));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required]int id, CancellationToken token)
        {
            var result = await service.GetAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IListResponse<PersonAggregate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(
            [Required][FromQuery] PeopleFilter filter, CancellationToken token)
        {
            var result = await queries.GetAsync(filter, token);

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