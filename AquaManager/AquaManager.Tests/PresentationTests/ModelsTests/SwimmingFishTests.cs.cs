using AquaManager.Domain.Models;
using AquaManager.Domain.Enums;
using AquaManager.Presentation.Models;
using System.Drawing;
using AquaManager.Domain.Factories;

namespace AquaManager.Tests.PresentationTests.ModelsTests
{
    [TestFixture]
    public class SwimmingFishTests
    {
        private Fish _fish;
        private Image _img;
        private FishFactory _fishFactory;

        [SetUp]
        public void SetUp()
        {
            _fishFactory = new FishFactory();
            _fish = _fishFactory.CreateFish(FishType.Guppy);
            _img = new Bitmap(60, 60);
        }

        [TearDown]
        public void TearDown()
        {
            _img.Dispose();
        }

        [Test]
        public void Constructor_ShouldSetProperties()
        {
            var sf = new SwimmingFish(_fish, _img, 10, 20, _fishFactory.IsDefaultRight(FishType.Guppy), 45, 45);
            Assert.AreEqual(_fish, sf.Model);
            Assert.AreEqual(10, sf.Position.X);
            Assert.AreEqual(20, sf.Position.Y);
            Assert.AreEqual(45, sf.Image.Width);
            Assert.AreEqual(45, sf.Image.Height);
            Assert.IsNotNull(sf.Velocity);
        }

        [Test]
        public void Update_ShouldChangePosition()
        {
            var sf = new SwimmingFish(_fish, _img, 50, 50, _fishFactory.IsDefaultRight(FishType.Guppy), 45, 45);
            var oldPos = sf.Position;
            // зафиксируем скорость для предсказуемости (через рефлексию или добавить сеттер)
            sf.Velocity = new PointF(2, 1.5f);
            sf.Update(200, 200);
            Assert.AreEqual(52, sf.Position.X);
            Assert.AreEqual(51.5, sf.Position.Y);
        }

        [Test]
        public void Update_ShouldBounceOffRightEdge()
        {
            var sf = new SwimmingFish(_fish, _img, 160, 50, _fishFactory.IsDefaultRight(FishType.Guppy), 45, 45);
            sf.Velocity = new PointF(5, 0);
            sf.Update(200, 200);
            Assert.That(sf.Position.X, Is.EqualTo(200 - 45).Within(0.001));
            Assert.AreEqual(-5, sf.Velocity.X);
        }

        [Test]
        public void Update_ShouldBounceOffLeftEdge()
        {
            var sf = new SwimmingFish(_fish, _img, 5, 50, _fishFactory.IsDefaultRight(FishType.Guppy), 45, 45);
            sf.Velocity = new PointF(-5, 0);
            sf.Update(200, 200);
            Assert.That(sf.Position.X, Is.EqualTo(0).Within(0.001));
            Assert.AreEqual(5, sf.Velocity.X);
        }
    }
}