using devantler_cms.Setup;
using Piranha;

namespace devantler_cms;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration configuration)
    {
        _config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.SetupPiranha(_config);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        PiranhaSetup.SetupPiranhaApi(api);
        PiranhaSetup.ConfigureTinyMCE();
        app.SetupPiranhaMiddleware();
    }
}
