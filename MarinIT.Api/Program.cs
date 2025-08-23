using System.Text;
using MarineIT.Infrastructure.Auth;
using MarineIT.Infrastructure.Data;
using MarineIT.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1) Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext + app services (from your Infrastructure project)
builder.Services.AddInfrastructure(builder.Configuration);

// Identity (users/roles stored in AppDbContext)
builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 2) JWT Authentication (make sure appsettings.json has a Jwt section)
var jwt = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = key
        };
    });

builder.Services.AddAuthorization();

// 🔹 3) CORS: allow your frontend origins to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("ui", policy => policy
        .WithOrigins(
            "http://localhost:5173",   // Vite default
            "http://localhost:3000",   // (optional) another dev port
            "https://localhost:5173",  // if you run https
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
    // .AllowCredentials() // only if you use cookies; not needed for Bearer tokens
    );
});

var app = builder.Build();

// OPTIONAL: seed roles/admin/sample data on startup
await DataSeeder.SeedAsync(app.Services);

// 4) HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 enable CORS before auth
app.UseCors("ui");

app.UseAuthentication();   // required for JWT
app.UseAuthorization();

app.MapControllers();      // makes your controllers live

app.Run();
