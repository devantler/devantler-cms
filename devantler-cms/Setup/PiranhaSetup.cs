using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLite;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

namespace devantler_cms.Setup;
public static class PiranhaSetup
{
    public static void AddPiranhaSimplified(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
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

    internal static void UsePiranhaSimplified(this IApplicationBuilder app)
    {
        app.UsePiranha(options =>
        {
            options.UseManager();
            options.UseTinyMCE();
            options.UseIdentity();
        });
    }

    internal static void ConfigureTinyMCE()
    {
        EditorConfig.FromFile("editorconfig.json");
    }

    public static void Init(IApi api)
    {
        App.Init(api);

        new ContentTypeBuilder(api)
            .AddAssembly(typeof(PiranhaSetup).Assembly)
            .Build()
            .DeleteOrphans();
    }
}