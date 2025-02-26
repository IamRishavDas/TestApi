using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Helper;
using AutoMapper;
using TestAPI.Repository;
using TestAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<StudentRepository>(); // Register the repository
builder.Services.AddScoped<IStudentInterface, StudentRepository>(); // Register the interface and implementation

builder.Services.AddScoped<CourseRepository>(); // Register the repository
builder.Services.AddScoped<ICoursesInterface, CourseRepository>(); // Register the interface and implementation

builder.Services.AddScoped<InstructorRepository>(); // Register the repository
builder.Services.AddScoped<IInstructorInterface, InstructorRepository>(); // Register the interface and implementation

builder.Services.AddScoped<CourseStudentRepository>(); // Register the repository
builder.Services.AddScoped<ICourseStudentInterface, CourseStudentRepository>(); // Register the interface and implementation

var app = builder.Build();

//// Ensure database is created and seeded
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate(); // Apply any pending migrations
//    ApplicationDbContextSeed.SeedData(context); // Seed the database with data
//}

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
