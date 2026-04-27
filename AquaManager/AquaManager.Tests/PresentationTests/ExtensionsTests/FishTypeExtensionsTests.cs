using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Presentation.Extensions;

namespace AquaManager.Tests.PresentationTests.ExtensionsTests
{
    [TestFixture]
    public class FishTypeExtensionsTests
    {
        [Test]
        public void GetImage_ShouldReturnNonNullImageForAllTypes()
        {
            var factory = new FishFactory();
            foreach (FishType type in factory.GetAllFishTypes())
            {
                var img = factory.GetFishImage(type);
                Assert.IsNotNull(img, $"Image for {type} is null");
            }
        }

        [Test]
        public void GetFishDescription_ShouldReturnNonNullStringForAllTypes()
        {
            var factory = new FishFactory();
            foreach (FishType type in factory.GetAllFishTypes())
            {
                var desc = factory.GetFishDescription(type);
                Assert.IsNotNull(desc, $"Description for {type} is null");
            }
        }
    }
}