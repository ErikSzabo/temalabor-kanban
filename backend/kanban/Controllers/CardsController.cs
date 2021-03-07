using kanban.Exceptions;
using kanban.Models;
using kanban.Models.Requests;
using kanban.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Controllers
{
    [Route("api/columns/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService service;

        public CardsController(ICardService service)
        {
            this.service = service;
        }

        [HttpGet("{cardID:int}")]
        public async Task<ActionResult> GetCard(int cardID)
        {
            try
            {
                return Ok(await service.GetCard(cardID));
            }
            catch (NotFound e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
            }
        }

        [HttpDelete("{cardID:int}")]
        public async Task<ActionResult> DeleteCard(int cardID)
        {
            try
            {
                await service.DeleteCard(cardID);
                return StatusCode(StatusCodes.Status204NoContent, "Card deleted");
            }
            catch (NotFound e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
            }
        }

        [HttpPut("{cardID:int}")]
        public async Task<ActionResult> UpdateCard(int cardID, Card card)
        {
            try
            {
                await service.UpdateCard(cardID, card);
                return NoContent();
            }
            catch (NotFound e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
            }
        }

        [HttpPut("{cardID:int}/moves")]
        public async Task<ActionResult> MoveCard(int cardID, CardMove cardMove)
        {
            try
            {
                await service.MoveCard(cardID, cardMove);
                return Ok(cardMove);
            }
            catch(NotFound e)
            {
                return NotFound(e.Message);
            }
            catch(BadRequest e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
            }
        }
    }
}
