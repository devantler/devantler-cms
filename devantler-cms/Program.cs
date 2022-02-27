using devantler_cms.Extensions;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
builder.Services.AddWebOptimizerSimplified(environment);
builder.Services.AddPiranhaSimplified(builder.Configuration, environment);

var app = builder.Build();
app.UseDeveloperExceptionPageSimplified(environment);
app.UsePiranhaSimplified();
app.UseWebOptimizer();
app.Run();