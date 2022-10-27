namespace GameGoal.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();
            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}