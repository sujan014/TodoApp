using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services
{
	public class TodoService : ITodoService
	{
		private readonly TodoDBContext _context;

		public TodoService(TodoDBContext context)
		{
			_context = context;
		}

		public async Task<Todo> GetTodo(int id)
		{
			var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
			if (todo == null)
			{
				throw new Exception("Todo not found");
			}
			return todo;
		}

		public async Task<IEnumerable<Todo>> GetTodos()
		{
			return await _context.Todos.ToListAsync();
		}

		public async Task<Todo> CreateTodo(CreateTodoRequest request)
		{
			var todo = new Todo
			{
				Title = request.Title,
				DueDate = request.DueDate,
				IsCompleted = false,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};

			_context.Todos.Add(todo);
			await _context.SaveChangesAsync();

			return todo;
		}
	}
}
