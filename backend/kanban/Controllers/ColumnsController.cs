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
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService service;

        public ColumnsController(IColumnService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetColumns()
        {
            return Ok(await service.GetColumnsInOrder());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            try
            {
                var result = await service.GetColumn(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data.");
            }
        }
    }
}
