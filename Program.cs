using BookStore.API.Data;
using BookStore.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuration for using JSON File
            builder.Configuration.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Configuration;

            // ConfigureService() (Startup.cs Deprecated above .Net 5.0)

            //Adding the Custom Repository and adding connection string
            builder.Services.AddDbContext<BookStoreContext>(options=>options.UseSqlServer(configuration.GetConnectionString("BookStoreDB"))); 

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding Custom Configuration
            builder.Services.AddTransient<IBookRepository,BookRepository>();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            //CORS Policy
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure() (Startup.cs Deprecated above .Net 5.0)
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
