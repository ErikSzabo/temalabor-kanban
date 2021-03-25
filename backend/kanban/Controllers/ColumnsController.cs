using kanban.Exceptions;
using kanban.Models;
using kanban.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Controllers
{
    [Route("api/columns")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService columnService;

        public ColumnsController(IColumnService columnService)
        {
            this.columnService = columnService;
        }

        [HttpGet]
        public async Task<ActionResult> GetColumns()
        {
            try
            {
                return Ok(await columnService.GetColumnsInOrder());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet("{columnID:int}")]
        public async Task<ActionResult<Column>> GetColumn(int columnID)
        {
            try
            {
                return Ok(await columnService.GetColumn(columnID));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet("{columnID:int}/cards")]
        public async Task<ActionResult> GetColumnCards(int columnID)
        {
            try
            {
                return Ok(await columnService.GetColumnCards(columnID));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPost("{columnID:int}/cards")]
        public async Task<ActionResult<Card>> AddCardToColumn(int columnID, Card card)
        {
            try
            {
                var savedCard = await columnService.AddCardToColumn(columnID, card);
                return Created(new Uri($"/api/columns/cards/{ savedCard.ID }", UriKind.Relative), savedCard);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }
    }
}
