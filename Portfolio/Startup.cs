
using Microsoft.EntityFrameworkCore;
using Portfolio.Services;

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
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(
                    configuration["ConnectionStrings:MySqlConnection"],
                    ServerVersion.AutoDetect(configuration["ConnectionStrings:MySqlConnection"])
                )
);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

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
