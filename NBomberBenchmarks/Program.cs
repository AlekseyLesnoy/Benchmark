using System;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace NBomberBenchmarks
{
    static class Program
    {
        static void Main(string[] args)
        {
            var httpFactory = HttpClientFactory.Create();
            
            var step = Step.Create("step", httpFactory, async context =>
            {
                var response = await context.Client.GetAsync("https://oauth.dev.####.com/healthapi/ready",
                    context.CancellationToken);

                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: (int)response.StatusCode)
                    : Response.Fail(statusCode: (int)response.StatusCode);
            });

            var scenario = ScenarioBuilder
                .CreateScenario("simple_http_test", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(Simulation.InjectPerSec(20, TimeSpan.FromSeconds(10)));
                //.WithLoadSimulations(Simulation.KeepConstant(20, TimeSpan.FromSeconds(10)));

            NBomberRunner.RegisterScenarios(scenario).Run();
        }
    }
}