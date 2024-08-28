using DataAccess.Contexts;
using DataAccess.Repositories.Contracts.Interfaces;
using DataAccess.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace WriterPlatformWeb;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddHttpContextAccessor();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<WriterPlatformContext>(
                            options => options.UseNpgsql(connectionString));

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkRepository, WorkRepository>();


        services.AddEndpointsApiExplorer();
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseStatusCodePages();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllers();
        });
    }
}