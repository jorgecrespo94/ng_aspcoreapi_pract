using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Value> Values {get; set;}//My models as tables
        public DbSet<User> Users {get; set;}//My models as tables
    }
}
