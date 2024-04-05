using BookStore.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreContext: IdentityDbContext<ApplicationUser>
        {
        public BookStoreContext(DbContextOptions<BookStoreContext> options): base(options) 
        {
            
        }

        public DbSet<Books> Books { get; set; }

        //Configuring connection string to connect Database
        //Other way of connecting Database can be done on Program.cs class
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Databse=BookStoreAPI;Integrated Security= True");
            base.OnConfiguring(optionsBuilder);
        }
        */
    }
}
