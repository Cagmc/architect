using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Architect.Common.Infrastructure.DataTransfer.Response;
using Architect.Database;
using Architect.Database.Entities;
using Architect.Database.QueryTypes;
using Architect.PersonFeature.DataTransfer.Request;
using Architect.PersonFeature.Queries;
using Architect.WebApp.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Architect.WebApp.Controllers.PersonFeature
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonQueriesController : ApiController
    {
        private readonly IPersonQueries queries;
        private readonly DatabaseContext context;

        public PersonQueriesController(IPersonQueries queries, DatabaseContext context)
        {
            this.queries = queries.ArgumentNullCheck(nameof(queries));
            this.context = context;
        }

        [HttpGet("view")]
        public async Task<IActionResult> GetView(CancellationToken token)
        {
            try
            {
                var people = await context.Query<PersonViewQuery>().ToListAsync(token);

                return Ok(people);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return StatusCode(500);
        }

        [HttpGet("sql")]
        public async Task<IActionResult> GetSql(CancellationToken token)
        {
            try
            {
                var people = await context.Query<PersonSqlQuery>().PersonSqlQuery().ToListAsync(token);

                return Ok(people);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return StatusCode(500);
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