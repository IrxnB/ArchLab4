using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arch4Platform;
using ArchLab4.Repository;

namespace ArchLab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskEntitiesController : ControllerBase
    {
        private readonly ArchDbContext _context;

        public TaskEntitiesController(ArchDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTasks()
        {
          if (_context.Tasks == null)
          {
              return NotFound();
          }
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/TaskEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetTaskEntity(long? id)
        {
          if (_context.Tasks == null)
          {
              return NotFound();
          }
            var taskEntity = await _context.Tasks.FindAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return taskEntity;
        }

        // PUT: api/TaskEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskEntity(long? id, TaskEntity taskEntity)
        {
            if (id != taskEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskEntity>> PostTaskEntity(TaskEntity taskEntity)
        {
          if (_context.Tasks == null)
          {
              return Problem("Entity set 'ArchDbContext.Tasks'  is null.");
          }
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskEntity", new { id = taskEntity.Id }, taskEntity);
        }

        // DELETE: api/TaskEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskEntity(long? id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var taskEntity = await _context.Tasks.FindAsync(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskEntityExists(long? id)
        {
            return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
