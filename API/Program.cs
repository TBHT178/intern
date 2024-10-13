using API.Data;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hỗ trợ Swagger/OpenAPI cho tài liệu hóa và kiểm thử API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt auth header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Sử dụng ReferenceHandler.Preserve
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//});

// Đăng ký dịch vụ Controller để hỗ trợ API
builder.Services.AddControllers();

// Đăng ký dịch vụ IUserService với triển khai UserService, sử dụng Dependency Injection (DI)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IFlightService, FlightService>();
// Cấu hình xác thực JWT (JSON Web Token) cho ứng dụng
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Thiết lập các tùy chọn xác thực JWT
            ValidateIssuer = true, // Kiểm tra Issuer (người phát hành token)
            ValidateAudience = true, // Kiểm tra Audience (đối tượng nhận token)
            ValidateLifetime = true, // Kiểm tra thời hạn token
            ValidateIssuerSigningKey = true, // Kiểm tra khóa ký token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer hợp lệ, lấy từ cấu hình
            ValidAudience = builder.Configuration["Jwt:Issuer"], // Audience hợp lệ, lấy từ cấu hình
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Khóa ký token, lấy từ cấu hình
        };
    });

builder.Services.AddDbContext<FlightSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // Chuyển hướng tất cả các yêu cầu HTTP sang HTTPS

// Bật tính năng xác thực và phân quyền
app.UseAuthentication(); // Kiểm tra xác thực bằng JWT
app.UseAuthorization(); // Kiểm tra quyền truy cập

app.MapControllers();

app.Run();
