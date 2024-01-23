using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
	public class TodoController : Controller
	{
		private readonly TodoDBContext _dbContext;
		private readonly ITodoService _todoService;

		public TodoController(TodoDBContext todoDBContext, ITodoService todoService)
		{
			_dbContext = todoDBContext;
			_todoService = todoService;
		}

		public async Task<IActionResult> Index()
		{
			var todos = await _todoService.GetTodos();
			return View(todos);
		}
		/*public IActionResult Index()
		{
			return View();
		}*/

		public IActionResult CreateWindow()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateTodoRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Todo todo = new Todo
			{
				Title = request.Title,
                DueDate = request.DueDate,
                IsCompleted = false,				
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};
			_dbContext.Add(todo);
			await _dbContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Todo/Details/4
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _dbContext == null) { 
				return NotFound();
            }

			var todo = await _dbContext.Todos.FirstOrDefaultAsync( item => item.Id == id);
			if (todo == null)
			{
				return NotFound();
			}

			return View(todo);
		}

		public async Task<IActionResult> DeleteWindow(int? id)
		{
			if (id == null || _dbContext.Todos == null)
			{
				return NotFound();
			}
			var todo = await _dbContext.Todos.FirstOrDefaultAsync(item => item.Id == id);
			if (todo == null )
			{
				return NotFound();
			}

			return View(todo);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_dbContext.Todos == null)
			{
				return Problem("Entity set 'SchoolsDBContext.Student' is null");
			}
			var student = await _dbContext.Todos.FindAsync(id);
			if (student != null)
			{
				_dbContext.Todos.Remove(student);
			}

			await _dbContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
        // GET: Courses/Edit/5
        public async Task<IActionResult> EditWindow(int? id)
		{
			if (id == null || _dbContext.Todos == null)
			{
				return NotFound();
			}

			var todo = await _dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
			if (todo == null)
			{
				return NotFound();
			}
			return View(todo);
		}
        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,IsCompleted,DueDate,CreatedAt,UpdatedAt")] Todo todo)
		{
			if (id != todo.Id)
			{
				return NotFound();
			}			
            if (ModelState.IsValid)
            {
                try
                {
					todo.UpdatedAt = DateTime.Now;
                    _dbContext.Update(todo);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);

        }
		/*
		[Route("Todo/EditWindow")]
        [Route("Todo/EditWindow/{id?}")]
        public async Task<IActionResult> Restore(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var todo = await _dbContext.Todos.FirstOrDefaultAsync( todo => todo.Id == id);
			if (todo == null)
			{
				return NotFound();
			}
			return View(todo);
		}*/
        private bool TodoExists(int id)
		{
			return (_dbContext.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
