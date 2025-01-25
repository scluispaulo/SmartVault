using System;
using System.Data.SQLite;
using System.IO;

namespace SmartVault.Program
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IOAuthIntegration _oauthIntegration;
        public FileProcessor(IOAuthIntegration oauthIntegration)
        {
            _oauthIntegration = oauthIntegration;
        }

        private void CheckifAuthenticated()
        {
            if (!_oauthIntegration.IsAuthenticated)
                throw new UnauthorizedAccessException("Not authenticated");
        }

        public void GetAllFileSizes()
        {
            CheckifAuthenticated();

            var totalSize = 0L;
            using (var conn = new SQLiteConnection("Data Source=C:..\\SmartVault.DataGeneration\\bin\\Debug\\net5.0\\testdb.sqlite"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = @"SELECT FilePath FROM Document";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var filePath = reader.GetString(0);
                        totalSize += new FileInfo(filePath).Length;
                    }
                }
            }
            Console.WriteLine($"Total size of all files: {totalSize}");
        }

        // go to the third file from an accountId and check if the file contains the text "Smith Property", if so, write it to a single file
        // TODO: Move connectionString and Query
        public void WriteEveryThirdFileToFile(string accountId)
        {
            CheckifAuthenticated();

            using (var conn = new SQLiteConnection("Data Source=C:..\\SmartVault.DataGeneration\\bin\\Debug\\net5.0\\testdb.sqlite"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText =
                    @"SELECT
	                    FilePath
                    FROM Document
                    WHERE AccountId = @AccountId
                    ORDER By Id
                    LIMIT 1 OFFSET 2";

                command.Parameters.AddWithValue("@AccountId", accountId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var filePath = reader.GetString(0);
                        if (File.ReadAllText(filePath).Contains("Smith Property"))
                        {
                            File.AppendAllText("SmithPropertyFiles.txt", File.ReadAllText(filePath) + Environment.NewLine);
                        }
                    }
                }
            }
        }
    }
}
