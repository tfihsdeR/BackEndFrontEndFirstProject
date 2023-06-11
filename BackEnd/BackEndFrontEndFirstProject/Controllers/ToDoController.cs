using BackEndFrontEndFirstProject.Data;
using BackEndFrontEndFirstProject.Entity.DTOs;
using BackEndFrontEndFirstProject.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFrontEndFirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        AppDbContext context = new AppDbContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ToDoGetAllResponseDto> toDoList = context.ToDoList.Select(t => new ToDoGetAllResponseDto()
            {
                CreateDate = t.CreateDate,
                Message = t.Message,
                UpdateDate = t.UpdateDate
            }).ToList();

            return Ok(toDoList);
        }

        [HttpGet("{message}")]
        public IActionResult GetByMessage(string message)
        {
            ToDoGetByIdResponseDto toDo = context.ToDoList.Select(t => new ToDoGetByIdResponseDto()
            {
                CreateDate = t.CreateDate,
                Message = t.Message
            }).FirstOrDefault(t => t.Message.ToLower() == message.ToLower());

            return Ok(toDo);
        }

        [HttpPost]
        public IActionResult Create(ToDoCreateRequestDto todoDto)
        {
            ToDo toDo = new ToDo()
            {
                CreateDate = DateTime.Now,
                Message = todoDto.Message
            };

            if (!context.ToDoList.Any(t => t.Message.ToLower() == todoDto.Message.ToLower()))
            {
                context.ToDoList.Add(toDo);
                context.SaveChanges();
                return Ok(toDo);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{message}")]
        public IActionResult Update(string message, ToDoUpdateDateRequestDto toDoDto)
        {
            ToDo toDo = context.ToDoList.FirstOrDefault(t => t.Message.ToLower() == message.ToLower());

            if (toDo == null)
            {
                return BadRequest();
            }
            else
            {
                toDo.Message = toDoDto.Message.Trim();
                toDo.UpdateDate = DateTime.Now;
                context.SaveChanges();
                return Ok(toDoDto);
            }
        }

        [HttpDelete("{message}")]
        public IActionResult Remove(string message)
        {
            ToDo toDo = context.ToDoList.FirstOrDefault(t => t.Message.ToLower() == message.ToLower());

            if (toDo == null)
            {
                return BadRequest();
            }
            else
            {
                context.Remove(toDo);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
