using Microsoft.Extensions.Configuration;
using System;
namespace CSharpEmilyApp;

public static class Config
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public static string ApiKey => _configuration["ApiKey"] ?? throw new Exception("ApiKey not found");
    public static string ApiUrl => _configuration["ApiUrl"] ?? throw new Exception("ApiUrl not found");
}