using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queue_Server.Common.Enums;
using Queue_Server.Common.Interfaces;
using Queue_Server.Common.Models;

namespace Queue_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class QueueController : ControllerBase
    {
        public IQueueEntityRepository repo { get; set; }
        public QueueController(IQueueEntityRepository queueEntityRepository)
        {
            repo = queueEntityRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repo.GetAllEntities());
        }

        [HttpGet("InProgress")]
        public IActionResult GetInProgressEntity()
        {
            return Ok(repo.GetEntityByStatus(Status.InProgress));

        }

        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            return Ok(repo.AddEntity(name));
        }

        [HttpPost("Enqueue")]
        public IActionResult PullNewEntity([FromForm] QueueEntity entity)
        {
            return Ok(repo.PullEntityFromQueue(entity));
        }
    }
}
