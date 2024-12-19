using API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddServiceDefaults();

builder.Services.AddDbContext<AppDbContextMongo>(opt =>
{
	opt.UseMongoDB(builder.Configuration.GetSection("MongoDB:ConnectionString").Value ?? throw new InvalidOperationException("MongoDB:ConnectionString not found"), builder.Configuration.GetSection("MongoDB:DatabaseName").Value ?? throw new InvalidOperationException("MongoDB:DatabaseName not found"));
});

builder.Services.AddCors(opt => {
	opt.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins(builder.Configuration.GetSection("NextJs:Url").Value ?? throw new InvalidOperationException("NextJs:Url not found"))
			.AllowAnyHeader()
			.AllowAnyOrigin()
			.AllowAnyMethod();
	});
});

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();