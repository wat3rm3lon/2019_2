using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;
using EntityApi.Services;
using EntityApi.Dtos;
using EntityApi.Entities;
namespace EntityApi.Controllers
{
    [Route("")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private IEntityService service_;
        private IMapper mapper_;
        public EntityController(IEntityService service, IMapper mapper)
        {
            service_ = service;
            mapper_ = mapper;
        }

        /* working body */
        
        [AllowAnonymous]
        [HttpPost("insert")]
        public IActionResult Insert([FromBody]EntityDto entityDto)
        {
            try
            {
                service_.Insert(new Entity { Id = entityDto.Id, OperationDate = entityDto.OperationDate, Amount = entityDto.Amount });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        /* */
        /* query string */
        //[AllowAnonymous]
        //[HttpPost("insert")]
        //public IActionResult Insert(EntityDto entityDto)
        /*
        [
    HttpGet("api/orders") // api/orders?identifier=7
]
        public Task<Order> Get(
    [FromQuery(Name = "identifier")] int id,
    [FromServices] IOrderService orderService)
    => orderService.GetOrderAsync(id);
        */
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Insert([FromQuery(Name = "insert")]string json)
        {
            try
            {
                string tmp = json;
                if (json.IndexOf("+") == -1)
                {
                    tmp = json.Replace(" ", "+");
                }
                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<Entity>(tmp);
                
                service_.Insert(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "get")]string id)
        {
            try
            {
                var entity = service_.Get(Guid.Parse(id));
                if(entity == null)
                    return BadRequest(string.Format("Сущность с id '{0}' не найдена", id));
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        


        /*
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var entity = service_.Get(id);
                var entityDto = mapper_.Map<EntityDto>(entity);
                return Ok(entityDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        */
    }
}