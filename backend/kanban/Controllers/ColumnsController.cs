using Kanban.Bll;
using Kanban.Bll.Exceptions;
using Kanban.Bll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanban.Api.Controllers
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
        public async Task<ActionResult<List<ColumnDto>>> GetColumns()
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
        public async Task<ActionResult<ColumnDto>> GetColumn(int columnID)
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
        public async Task<ActionResult<List<CardDto>>> GetColumnCards(int columnID)
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
        public async Task<ActionResult<CardDto>> AddCardToColumn(int columnID, [FromBody] CardDto card)
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
