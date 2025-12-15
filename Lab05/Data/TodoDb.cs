using Lab05.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Data
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options)
        {
        }

        // Bảng Todos trong InMemory DB
        public DbSet<Todo> Todos => Set<Todo>();
    }
}
