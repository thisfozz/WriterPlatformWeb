using DataAccess.Contexts;
using DataAccess.Repositories.Contracts.Interfaces;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WriterPlatformWeb.Services.Contracts.Interfaces;
using WriterPlatformWeb.Services.Implementations;

namespace WriterPlatformWeb;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddHttpContextAccessor();

        services.AddDbContext<WriterPlatformContext>(options =>
        {
            var connectionString = _configuration.GetConnectionString("WriterPlatformConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkRepository, WorkRepository>();

        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkService, WorkService>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = new PathString(CookieAuthenticationDefaults.LoginPath);
                opt.LogoutPath = new PathString(CookieAuthenticationDefaults.LogoutPath);
                opt.AccessDeniedPath = new PathString(CookieAuthenticationDefaults.AccessDeniedPath);
            });

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