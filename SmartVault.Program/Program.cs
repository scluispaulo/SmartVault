namespace SmartVault.Program
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            var oauthIntegration = new OAuthIntegration(
                clientId: "clientId",
                clientSecret: "clientSecret",
                authorizationEndpoint: "https://oauth-provider.com/authorize",
                tokenEndpoint: "https://oauth-provider.com/token",
                redirectUri: "http://localhost/callback"
            );

            oauthIntegration.Authenticate();

            var fileProcessor = new FileProcessor(oauthIntegration);
            fileProcessor.WriteEveryThirdFileToFile(args[0]);
            fileProcessor.GetAllFileSizes();
        }
    }
}