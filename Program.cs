using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task2.Helpers;
using Task2.Models;
using Task2.Services;

var builder = WebApplication.CreateBuilder(args);

string CORSOpenPolicy = "OpenCORSPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: CORSOpenPolicy,
      builder => {
          builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
      });
});

// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseInMemoryDatabase("Database")
);
builder.Services.AddScoped<BookService, BookService>();

var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AutoMapperProfile>();
});
builder.Services.AddSingleton<IMapper>(new Mapper(config));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(CORSOpenPolicy);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(name: "recommendedAlias", pattern: "api/recommended", defaults: new { controller = "Books", action = "GetBooks"});

app.Run();
