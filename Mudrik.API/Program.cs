
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Behaviors;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Gamification.Commands.AwardXp;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using Mudrik.Infrastructure.DataSeeding;
using Mudrik.Infrastructure.Realtime;
using Mudrik.Infrastructure.Services;
using Mudrik.Infrastructure.Services.Repositories;
using Mudrik.Infrastructure.Settings;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mudrik.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IAppDbContext, AppDbContext>();

            builder.Services.AddMediatR(options =>
                options.RegisterServicesFromAssemblies(typeof(IAssemblyMarker).Assembly)
            );


            // SignalR
            builder.Services.AddSignalR();

            builder.Services.AddScoped<IGamificationNotifier, GamificationNotifier>();
            builder.Services.AddScoped<IXpTransactionRepository, XpTransactionRepository>();
            builder.Services.AddScoped<IGamificationStreakRepository, GamificationStreakRepository>();
            builder.Services.AddScoped<IStudentDirectoryLookup, StudentDirectoryLookup>();

            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AwardXpCommand>());
            //builder.Services.AddValidatorsFromAssemblyContaining<AwardXpCommandValidator>();

            builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaveior<,>));

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection("GoogleAuthSettings"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(
                   new JsonStringEnumConverter());

               options.JsonSerializerOptions.AllowTrailingCommas = true;
               options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;

           });

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
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await IdentitySeeder.SeedAsync(userManager, roleManager);
            }

            app.MapControllers();

            app.Run();
        }
    }
}
