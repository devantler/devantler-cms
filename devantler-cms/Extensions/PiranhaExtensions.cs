using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLite;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

namespace devantler_cms.Extensions;
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

            var connectionString = config.GetConnectionString("Database");
            switch (environment.EnvironmentName)
            {
                case "Development":
                    options.UseEF<SQLiteDb>(db => db.UseSqlite(connectionString));
                    options.UseIdentityWithSeed<IdentitySQLiteDb>(db => db.UseSqlite(connectionString));
                    break;
                case "Staging":
                case "Production":
                    options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
                    options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(connectionString));
                    break;
            }
        });
    }

    internal static void UsePiranhaSimplified(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;
        var api = serviceProvider.GetRequiredService<IApi>();
        App.Init(api);

        new ContentTypeBuilder(api)
            .AddAssembly(typeof(Program).Assembly)
            .Build()
            .DeleteOrphans();

        ConfigureTinyMCE();

        app.UsePiranha(options =>
        {
            options.UseManager();
            options.UseTinyMCE();
            options.UseIdentity();
        });
    }

    private static void ConfigureTinyMCE()
    {
        EditorConfig.FromFile("editorconfig.json");
    }
}