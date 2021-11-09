using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Net6LambdaTemplate;

/*
This class extends from APIGatewayProxyFunction which contains the method FunctionHandlerAsync which is the 
actual Lambda function entry point. The Lambda handler field in serverless.template should be set to:
 
    Net6LambdaTemplate::Net6LambdaTemplate.LambdaEntryPoint::FunctionHandlerAsync

The base class must be set to match the AWS service invoking the Lambda function:

    API Gateway REST API -> Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    API Gateway HTTP API -> Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction
    Application Load Balancer -> Amazon.Lambda.AspNetCoreServer.ApplicationLoadBalancerFunction
*/

public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction
{
    /// <summary>
    /// The builder has configuration, logging and Amazon API Gateway already configured. The startup class
    /// needs to be configured in this method using the UseStartup&lt;&gt;() method.
    /// </summary>
    /// <param name="builder"></param>
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }

    /// <summary>
    /// Use this override to customize the services registered with the IHostBuilder. 
    /// 
    /// It is recommended not to call ConfigureWebHostDefaults to configure the IWebHostBuilder inside this method.
    /// Instead customize the IWebHostBuilder in the Init(IWebHostBuilder) overload.
    /// </summary>
    /// <param name="builder"></param>
    protected override void Init(IHostBuilder builder)
    {
    }
}