
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Behaviors;
using Mudrik.Application.Interfaces;
using Mudrik.Infrastructure.Data;

namespace Mudrik.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
            );

            builder.Services.AddScoped<IAppDbContext, AppDbContext>();

            builder.Services.AddMediatR(options =>
                options.RegisterServicesFromAssemblies(typeof(IAssemblyMarker).Assembly)
            );

            builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaveior<,>));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
