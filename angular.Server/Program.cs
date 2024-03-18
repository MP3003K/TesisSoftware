using API.Filters;
using Configurations;
using Containers;

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

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
        )
);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

