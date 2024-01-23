namespace TodoApp.ViewModels
{
	public class CreateTodoRequest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime? DueDate { get; set; } = null;
	}
}
