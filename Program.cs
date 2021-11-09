using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;

namespace Net6LambdaTemplate;

public class Program
{
    public static void Main(string[] args)
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
        {
            CreateHostBuilder(args).Build().Run();
        }
        else
        {
            var lambdaEntry = new LambdaEntryPoint();
            var functionHandler =
                (Func<APIGatewayHttpApiV2ProxyRequest, ILambdaContext, Task<APIGatewayHttpApiV2ProxyResponse>>)
                (lambdaEntry.FunctionHandlerAsync);

            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(functionHandler, new DefaultLambdaJsonSerializer());
            using var bootstrap = new LambdaBootstrap(handlerWrapper);

            bootstrap.RunAsync().Wait();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}