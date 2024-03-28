using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using System.Text.Json.Serialization;
using RMall_BE.Interfaces;
using RMall_BE.Repositories;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Repositories.ShopRepositories;
using RMall_BE.Repositories.OrderRepositories;
using RMall_BE.Repositories.MovieRepositories;
using RMall_BE.Repositories.MovieRepositories.SeatRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using RMall_BE.Swagger;
using RMall_BE.Interfaces.MallInterfaces;
using RMall_BE.Repositories.MallRepositories;
using RMall_BE.Repositories.UserRepositories;
using RMall_BE.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "v1",
        Title = "RMall API",
        Description = "RMall API",
        //Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        //{
        //    Name = "RMall",
        //    Url = new Uri("https://localhost:7168")
        //}
    });

});

// Config Swagger to Authentication
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IUserRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IUserRepository<Admin>, AdminRepository>();
builder.Services.AddScoped<IUserRepository<Tenant>, TenantRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGalleryMovieRepository, GalleryMovieRepository>();
builder.Services.AddScoped<IGalleryMallRepository, GalleryMallRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();
builder.Services.AddScoped<ISeatReservationRepository, SeatReservationRepository>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<RMallContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("RMallContext") ??
    throw new InvalidOperationException("Connection string 'RMallContext' not found."))
);

// Lấy cấu hình từ appsettings.json
var configuration = builder.Configuration;

// Lấy các cài đặt mail từ appsettings.json
var mailSettings = configuration.GetSection("MailSettings");
var jwtSettings = configuration.GetSection("JwtSettings");

// Thêm cấu hình mail vào dịch vụ của ứng dụng
//builder.Services.Configure<MailSettings>(mailSettings);

// Thiết lập JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

// Authorization
//builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication
app.UseAuthentication();

app.UseRouting();

// Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();