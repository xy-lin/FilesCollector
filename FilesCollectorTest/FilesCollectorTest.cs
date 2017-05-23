using NUnit.Framework;

namespace FilesUtility
{
    [TestFixture]
    public class SpotlightCollectorTest : AssertionHelper
    {
        FilesCollector _spotlightCollector;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _spotlightCollector = new FilesCollector();
        }

        [TearDown]
        public void Teardown()
        {
        }

        [Test]
        [TestCase("Somefile.xml")]
        public void CopyPicture(string filename)
        {
            
        }
        
        [Test]
        public void ConfigsReading(string filename)
        {
        }
   

    }
}
