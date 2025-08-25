using System.Text;
using System.Text.Json.Serialization;
using MarineIT.Infrastructure.Auth;
using MarineIT.Infrastructure.Data;
using MarineIT.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1) MVC + JSON (ignore EF circular refs; keep default camelCase)
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        // Do NOT set PropertyNamingPolicy; default camelCase matches the Next.js frontend.
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2) Infrastructure (DbContext, repositories/services, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// 3) Identity (users/roles in AppDbContext)
builder.Services
    .AddIdentity<AppUser, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 6;
        opts.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 4) JWT Auth (expects a "Jwt" section in appsettings)
var jwt = builder.Configuration.GetSection("Jwt");
var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);
var signingKey = new SymmetricSecurityKey(keyBytes);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// 5) CORS for the frontend (Next.js on :3000; keep Vite 5173 if you use it)
builder.Services.AddCors(options =>
{
    options.AddPolicy("ui", policy => policy
        .WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            "http://localhost:5173",
            "https://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
    // .AllowCredentials() // only if you switch to cookie auth
    );
});

var app = builder.Build();

// (Optional) seed roles/admin/sample data
await DataSeeder.SeedAsync(app.Services);

// 6) Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// With launchSettings exposing both http/https, this is fine
app.UseHttpsRedirection();

// CORS must be before auth if preflight needs to pass
app.UseCors("ui");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Quick health-check for the frontend
app.MapGet("/api/health", () => Results.Ok(new { status = "ok", timeUtc = DateTime.UtcNow }));

app.Run();
