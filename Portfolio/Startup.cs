
using Microsoft.EntityFrameworkCore;
using Portfolio.Filters;
using Portfolio.Services;
using Portfolio.Services.Email;
using Portfolio.Services.Storage;
using System.Configuration;

namespace Portfolio
{
    public class Startup
    {
        public IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IFileStorage, LocalFileStorage>();

            services.AddHttpContextAccessor();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddEndpointsApiExplorer();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("<your-domain>")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            services.AddScoped<IEmailService, EmailService>();

            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = configuration["ApplicationInsights:ConnectionString"];
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                }
            });

            app.UseCors("AllowLocalhost");

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
