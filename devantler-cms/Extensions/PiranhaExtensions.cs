using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLite;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

namespace devantler.cms.Extensions;
public static class PiranhaExtensions
{
    public static void AddPiranhaSimplified(this IServiceCollection services, ConfigurationManager config, IWebHostEnvironment environment)
    {
        services.AddPiranha(options =>
        {
            options.UseCms();
            options.UseManager();
            options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
            options.UseImageSharp();
            options.UseTinyMCE();
            options.UseMemoryCache();
            UseDb(config, environment, options);
        });
    }

    private static void UseDb(ConfigurationManager config, IWebHostEnvironment environment, PiranhaServiceBuilder options)
    {
        var connectionString = config.GetConnectionString("Database");
        switch (environment.EnvironmentName)
        {
            case "Development":
                UseSQLite(options, connectionString);
                break;
            case "Staging":
            case "Production":
                UseSQLServer(options, connectionString);
                break;
        }
    }

    private static void UseSQLite(PiranhaServiceBuilder options, string connectionString)
    {
        options.UseEF<SQLiteDb>(db => db.UseSqlite(connectionString));
        options.UseIdentityWithSeed<IdentitySQLiteDb>(db => db.UseSqlite(connectionString));
    }
    private static void UseSQLServer(PiranhaServiceBuilder options, string connectionString)
    {
        options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
        options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(connectionString));
    }

    internal static void UsePiranhaSimplified(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;
        var api = serviceProvider.GetRequiredService<IApi>();
        App.Init(api);
        ImportContentTypes(api);
        ConfigureTinyMCE();
        EnableServices(app);
    }

    private static void ImportContentTypes(IApi api)
    {
        new ContentTypeBuilder(api)
                    .AddAssembly(typeof(Program).Assembly)
                    .Build()
                    .DeleteOrphans();
    }

    private static void ConfigureTinyMCE()
    {
        EditorConfig.FromFile("editorconfig.json");
    }

    private static void EnableServices(WebApplication app)
    {
        app.UsePiranha(options =>
        {
            options.UseManager();
            options.UseTinyMCE();
            options.UseIdentity();
        });
    }
}