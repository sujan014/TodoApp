using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services
{
	public interface ITodoService
	{
		Task<IEnumerable<Todo>> GetTodos();

		Task<Todo> GetTodo(int id);
		Task<Todo> CreateTodo(CreateTodoRequest request);
	}
}
