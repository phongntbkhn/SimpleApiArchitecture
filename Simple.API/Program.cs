
using Microsoft.Extensions.Configuration;
using Simple.API.Infrastructure;
using Simple.API.Infrastructure.Repository.Base;
using Simple.Common;
using Microsoft.EntityFrameworkCore;
using Simple.API.Service.IService;
using Simple.API.Service;
using Simple.API.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Simple.API.Infrastructure.Entities;

namespace Simple.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Add services to the container.
            var connectStrings = Environment.GetEnvironmentVariable("CONNECTION");
            if (connectStrings == null || connectStrings == string.Empty)
                connectStrings = builder.Configuration.GetConnectionString("DefaultConnection");
            BaseDependency.InjectDependencies(builder.Services);
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            
            builder.Services.AddDbContext<SimpleApiContext>(opt => opt.UseNpgsql(connectStrings));

            builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<SimpleApiContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Dependency service
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductionService, ProductionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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
