﻿using BookStoreApplicationAPI.DAL;
using BookStoreApplicationAPI.DAL.Services;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data;
using BookStoreApplicationAPI.Data.Infrastructure;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Filters;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProductRepository<ProductWithResource>, ProductRepository<ProductWithResource>>();
builder.Services.AddTransient<IStoreRepository, StoreRepository>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "My API - V1",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "BookStoreApplicationAPI.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add<JsonExceptionFilter>();
    options.Filters.Add<LogRequestFilter>();
    options.Filters.Add<LinkRewritingFilter>();
});
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreApplicationContext") ?? throw new InvalidOperationException("Connection string 'BookStoreApplicationAPIContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
            {
                option.LoginPath = "/api/Access";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            });
builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("policyAdmin", pb => pb
    .RequireRole("Admin"));
});

//builder.Services.AddLogging(options =>
//    options.AddDebug(builder.Configuration.AddConfiguration("Default"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

var app = builder.Build();

InitializeDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Access}/{action=Login}/{id?}");

app.MapControllers().RequireAuthorization("policyAdmin");

app.Run();

static void InitializeDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            SeedData.InitializeAsync(services).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "error seeding db.");
        }

    }
}
