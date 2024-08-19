
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

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
