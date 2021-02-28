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
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService service;

        public ColumnController(IColumnService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetColumns()
        {
            return Ok(await service.GetColumnsInOrder());
        }
    }
}
