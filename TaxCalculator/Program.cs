using CongestionTaxAPI.Business;
using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.Interfaces;
using CongestionTaxAPI.Intrefaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();
builder.Services.AddTransient<ITaxRuleService, TaxRuleService>();

var app = builder.Build();

using (var dbContext = new DatabaseContext())
{
    dbContext.Initialize();
    //dbContext.SeedData();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CongestionTaxAPI V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
