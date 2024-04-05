using BookStore.API.Data;
using BookStore.API.Helpers;
using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            //Identity Configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();

            //JWT Configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
                AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata= false;
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding Custom Configuration
            builder.Services.AddTransient<IBookRepository,BookRepository>();
            builder.Services.AddTransient<IAccountRepository,AccountRepository>();

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

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
