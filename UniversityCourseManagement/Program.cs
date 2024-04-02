using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityCourseManagement.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	connectionString: builder.Configuration.GetConnectionString("DefaultConnection")
	));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for checking authentication
builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = false;
	x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer= false,
		IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("thisIsMySecurity..............")),
		ValidateAudience= false,
		ValidateIssuerSigningKey = true,
		RequireExpirationTime=true,
		ClockSkew = TimeSpan.Zero,
		
	};

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseCors(option => option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()); //cors problem access 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
