using LayeringBookAPI.Models;
using LayeringBookAPI.Repository;
using LayeringBookAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Logging.AddLog4Net();
builder.Services.AddTransient<IRepoBook<Book>, RepoBook>();
builder.Services.AddTransient<ISerBook<Book>, SerBook>();
builder.Services.AddTransient<IRepoBooking<Booking>, RepoBooking>();
builder.Services.AddTransient<ISerBooking<Booking>, SerBooking>();
builder.Services.AddTransient<IRepoClient<Client>, RepoClient>();
builder.Services.AddTransient<ISerClient<Client>, SerClient>();
builder.Services.AddTransient<IRepoLibStaff<LibStaff>, RepoLibStaff>();
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseCors(x => x
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
