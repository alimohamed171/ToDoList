using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<INoteServices, NoteServices>();

builder.Services.AddDbContext<ApplicationDbContext>(
    builder => builder.UseSqlServer("Server=localhost\\sqlexpress; Database=ToDoList; Integrated Security=True; trust server certificate=true"));


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
