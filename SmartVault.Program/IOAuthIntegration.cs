namespace SmartVault.Program
{
    public interface IOAuthIntegration
    {
        void Authenticate();
        bool IsAuthenticated { get; }
    }
}
