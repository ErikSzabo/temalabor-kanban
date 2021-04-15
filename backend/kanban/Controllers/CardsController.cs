using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Kanban.Data;
using Kanban.Bll;
using Kanban.Bll.Exceptions;
using Kanban.Bll.Models;

namespace Kanban.Api.Controllers
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
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
                throw;
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
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
                throw;
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
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
                throw;
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
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
                throw;
            }
        }
    }
}
