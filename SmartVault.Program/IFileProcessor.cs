namespace SmartVault.Program
{
    public interface IFileProcessor
    {
        void WriteEveryThirdFileToFile(string filePath);
        void GetAllFileSizes();
    }
}
