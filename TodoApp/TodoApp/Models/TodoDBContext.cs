using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
	public partial class TodoDBContext: DbContext
	{
		public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options){}

		public virtual DbSet<Todo> Todos { get;set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Todo>()
				.Property(prop => prop.DueDate)
				.IsRequired(false);

			modelBuilder.Entity<Todo>()
				.Property(prop => prop.Title)
				.HasMaxLength(256);

			base.OnModelCreating(modelBuilder);
		}

		partial void OnModelCreatingpartial(ModelBuilder modelBuilder);
	}
}
