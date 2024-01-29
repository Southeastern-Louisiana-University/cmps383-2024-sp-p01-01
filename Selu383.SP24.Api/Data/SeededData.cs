using Microsoft.EntintyFrameworkCore;
using Selu383.SP24.Api.Controllers
using Selu383.SP24.Api.Data
namespace Selu383.SP24.Api.Data

public class SeededData {

        //This sets up the Data Context for the entity framework core
        using (var context = new DataContext(
            ServiceProvider.GetRequiredService<
                DbContextOption<DataContext>>()))

            public IConfiguration Configuration { get; }

// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddCors();
    services.AddControllers();


    services.AddDbContext<DataContext>(options =>
    {
        // options.UseInMemoryDatabase("FooBar");
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    });

    //TODO
    services.AddMvc();

    services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
        });

    services.AddAuthorization();
    services.AddHttpContextAccessor();

    // configure DI for application services
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
}

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
{
    dataContext.Database.EnsureDeleted();
    dataContext.Database.EnsureCreated();

    app.UseHsts();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSpaStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    }); ;

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning Starter Server API V1");
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(x => x.MapControllers());

    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "learning-starter-web";
        if (env.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:3001");
        }
    });

    SeedHotel(dataContext);
}

private void SeedHotel(DataContext dataContext)
{

    if (!dataContext.Hotel.Any())
    {

        var Hotels = dataContext.Hotels.ToList();

        var seededHotels = new List<Hotels>
                {
                    new Hotel
                    {
                   Name = "Holiday Inn",
                   Address = "1234 N Tenth St, Covington La 70433"
                }
                    dataContext.SaveChanges();
    }
    }
}

                
            