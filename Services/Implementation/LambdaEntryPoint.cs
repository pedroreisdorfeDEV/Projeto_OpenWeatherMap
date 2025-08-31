using Amazon.Lambda.AspNetCoreServer;

namespace projetoGloboClima.Services.Implementation
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {

        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseLambdaServer(); // Inicializa o WebApplication do Program.cs
        }
    }
}
