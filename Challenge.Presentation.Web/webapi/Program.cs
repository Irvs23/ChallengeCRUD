using Challenge.Infrastructure.Persistance.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region JWT Authentication

    builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
    {
        opt.Password.RequiredLength = 7;
        opt.Password.RequireDigit = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<RepositoryDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Home/AccessDenied");
        options.LoginPath = new PathString("/Identity/Account/Login");
        options.LogoutPath = "/Identity/Account/Login";
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
    });

    builder.Services.AddAuthentication()
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        //options.AddPolicy("ResourceAccess", policy => policy.Requirements.Add(new ResourceAccessRequirement()));
    });

// builder.Services.AddScoped<IAuthorizationHandler, ResourceAccessHandler>();

#endregion

builder.Services.AddDbContext<RepositoryDbContext>(opts =>
          opts.UseSqlServer(builder.Configuration.GetConnectionString("BiaTransformaDB"), b => { b.UseRowNumberForPaging(); b.MigrationsAssembly("BiaTransforma.Infrastructure.Persistance"); }));

builder.Services.AddControllers();
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



