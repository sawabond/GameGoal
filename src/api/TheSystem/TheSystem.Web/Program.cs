﻿using Application.Services.Abstractions;

namespace TheSystem.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var build = CreateHostBuilder(args).Build();

        await build.Services
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<ISeeder>()
            .SeedIfNeeded();

        build.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}