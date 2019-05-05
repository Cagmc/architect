using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database.Entities;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.Queries;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architect.WebApp.Controllers.PersonFeature
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonQueriesController : ApiController
    {
        private readonly IPersonQueries queries;

        public PersonQueriesController(IPersonQueries queries)
        {
            this.queries = queries.ArgumentNullCheck(nameof(queries));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonAggregate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required]int id, CancellationToken token)
        {
            var result = await queries.GetAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IListResponse<PersonAggregate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(
            [Required][FromQuery]PeopleFilter filter, CancellationToken token)
        {
            var result = await queries.GetAsync(filter, token);

            return GenerateResponse(result);
        }

        [HttpGet("address/{id}")]
        [ProducesResponseType(typeof(AddressAggregate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddress([Required]int id, CancellationToken token)
        {
            var result = await queries.GetAddressAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpGet("address")]
        [ProducesResponseType(typeof(IListResponse<AddressAggregate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddressList([Required][FromQuery]AddressFilter filter, CancellationToken token)
        {
            var result = await queries.GetAddressAsync(filter, token);

            return GenerateResponse(result);
        }

        [HttpGet("name/{id}")]
        [ProducesResponseType(typeof(NameAggregate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetName([Required]int id, CancellationToken token)
        {
            var result = await queries.GetNameAsync(id, token);

            return GenerateResponse(result);
        }

        [HttpGet("name")]
        [ProducesResponseType(typeof(IListResponse<NameAggregate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNameList([Required][FromQuery]NameFilter filter, CancellationToken token)
        {
            var result = await queries.GetNameAsync(filter, token);

            return GenerateResponse(result);
        }
    }
}