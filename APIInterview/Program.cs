using APIInterview.Models;
using Microsoft.EntityFrameworkCore;

namespace APIInterview
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           

            // ���UDI�`�J 
            builder.Services.AddDbContext<ReviewContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Review"));
            });

            //���U�}��Cors
            string PolicyName = "ALL";
            builder.Services.AddCors(options => {
                options.AddPolicy(name: PolicyName, policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            // �K�[ HttpClient �A��
            builder.Services.AddHttpClient();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("ALL");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
