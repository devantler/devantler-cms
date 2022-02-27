using devantler.cms.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebOptimizerSimplified(builder.Environment);
builder.Services.AddPiranhaSimplified(builder.Configuration, builder.Environment);

var app = builder.Build();
app.UseDeveloperExceptionPageSimplified(app.Environment);
app.UsePiranhaSimplified();
app.UseWebOptimizer();
app.Run();