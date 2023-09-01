using API.Filters;
using IoC.Configurations;
using IoC.Containers;
 using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvcCore(options =>
{
    options.Filters.Add<ExceptionFilter>();
    options.Filters.Add<TransactionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Start IoC project Configurations
builder.Services.AddConfigurations(builder.Configuration);

//Start IoC Dependencies Injections
builder.Services.AddInjections();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
