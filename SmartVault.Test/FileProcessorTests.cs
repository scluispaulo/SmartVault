using System;
using Moq;
using SmartVault.Program;
using Xunit;

namespace SmartVault.Test
{
    public class FileProcessorTests
    {
        [Fact]
        public void FileProcessor_NotAuthenticated_DoesNotProcessFiles()
        {
            var mockOAuthIntegration = new Mock<IOAuthIntegration>();
            mockOAuthIntegration.Setup(o => o.IsAuthenticated).Returns(false);

            var fileProcessor = new FileProcessor(mockOAuthIntegration.Object);

            var exception = Assert.Throws<UnauthorizedAccessException>(() => fileProcessor.GetAllFileSizes());
            Assert.Equal("Not authenticated", exception.Message);
        }
    }
}
