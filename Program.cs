using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Application.Interfaces.Repositories;
using NetCoreWebApi.Application.Interfaces.Services;
using NetCoreWebApi.Application.Mapping;
using NetCoreWebApi.Application.Services;
using NetCoreWebApi.Application.Validators;
using NetCoreWebApi.Domain.Entities;
using NetCoreWebApi.Infrastructure.Data;
using NetCoreWebApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Register Services and Repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<IRepository<Book>, BookRepository>();

// builder.Services.AddScoped(typeof(IGenericRepository<Book>), typeof(BookRepository<>));


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAutoMapper(typeof(AuthorMappingProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);


// Register all validators in the assembly
builder.Services.AddValidatorsFromAssemblyContaining<AuthorDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BookDtoValidator>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
