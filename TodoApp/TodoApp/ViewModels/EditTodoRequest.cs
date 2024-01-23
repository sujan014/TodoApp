namespace TodoApp.ViewModels
{
	public class EditTodoRequest
	{
		public int? Id { get; set; }
		public string Title;
		public bool IsCompleted;
		public DateTime? DueDate;
	}
}
