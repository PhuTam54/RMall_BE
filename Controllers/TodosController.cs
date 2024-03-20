using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Models;

namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly RMallContext _context;

        public TodosController(RMallContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Todo>),200)]
        public IActionResult GetTodos()
        {
            var todos = _context.Todos.ToList();
            return Ok(todos);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateTodo([FromBody] Todo todocreate)
        {
            
            _context.Add(todocreate);
            _context.SaveChanges();

            return Ok("create success");

        }


        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodo(int id, [FromBody] Todo todoupdate)
        {
            var todo = _context.Todos.AsNoTracking().FirstOrDefault(t => t.ID == id);
            if(todo == null)
            {
                return NotFound();
            }
            if(id !=  todoupdate.ID)
            {
                return BadRequest();
            }
            _context.Update(todoupdate);
            _context.SaveChanges();

            return Ok("update success");

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _context.Todos.AsNoTracking().FirstOrDefault(t => t.ID == id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.Remove(todo);
            _context.SaveChanges();

            return Ok("delete success");

        }

    }
}
