using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var companiesConfigXml = builder.Configuration.GetSection("Companies").Get<List<DataOfCompany>>();
var companiesConfigJson = builder.Configuration.GetSection("Companies").Get<List<DataOfCompany>>();
var companiesConfigIni = builder.Configuration.GetSection("Companies").Get<List<DataOfCompany>>();
var userConfig = builder.Configuration.GetSection("DataOfUser").Get<DataOfUser>();

var analysisOfWorkers = companiesConfigXml
    .Concat(companiesConfigJson) 
    .Concat(companiesConfigIni)
    .OrderByDescending(c => c.Employees)
    .FirstOrDefault();

app.MapGet("/", () => $"Name of Company [Number of Workers] - {analysisOfWorkers?.Name} [{analysisOfWorkers?.Employees}]\n\n" + $"Name - {userConfig?.Name}, Hobby - {userConfig?.Hobby}, Age - {userConfig?.Age}");
app.Run();