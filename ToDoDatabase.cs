using Microsoft.EntityFrameworkCore;

namespace MinimalApiTutorial
{
    public class ToDoDatabase : DbContext
    {
        public ToDoDatabase(DbContextOptions<ToDoDatabase> options) : base(options)
        {

        }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
