using AquaManager.Domain.Constants;
using AquaManager.Domain.Models;
using AquaManager.Presentation.Constants;

namespace AquaManager.Presentation.Models;

public class SwimmingFish
{
    private Image _imageLeft;
    private Image _imageRight;
    private Image _currentImage;

    public Fish Model { get; private set; }
    public PointF Position { get; set; }
    public PointF Velocity { get; set; }
    public Image Image => _currentImage; // Уменьшенное изображение

    public SwimmingFish(Fish fish, Image originalImage, float startX, float startY, bool defaultOrientationRight,
        int targetWidth = PresentationConstants.StandartFishImageWidth, int targetHeight = PresentationConstants.StandartFishImageHeight)
    {
        Model = fish;
        var scaled = ScaleImage(originalImage, targetWidth, targetHeight);

        if (defaultOrientationRight)
        {
            _imageRight = scaled;
            _imageLeft = MirrorImage(scaled);
        }
        else
        {
            _imageLeft = scaled;
            _imageRight = MirrorImage(scaled);
        }
        _currentImage = _imageRight;

        Position = new PointF(startX, startY);
        Random rnd = new Random();

        var speedMin = PresentationConstants.SwimmingFishVelocityMin;
        var speedMax = PresentationConstants.SwimmingFishVelocityMax;

        Velocity = new PointF(
            (float)(rnd.NextDouble() * (speedMax - speedMin) + speedMin),
            (float)(rnd.NextDouble() * (speedMax - speedMin) + speedMin)
        );
    }

    private Image MirrorImage(Image original)
    {
        var mirrored = new Bitmap(original.Width, original.Height);
        using (var g = Graphics.FromImage(mirrored))
        {
            g.DrawImage(original, mirrored.Width, 0, -mirrored.Width, mirrored.Height);
        }
        return mirrored;
    }

    private static Image ScaleImage(Image originalImage, int targetWidth, int targetHeight)
    {
        var newImage = new Bitmap(targetWidth, targetHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(originalImage, new Rectangle(0, 0, targetWidth, targetHeight));
        }
        return newImage;
    }

    public void Update(float maxWidth, float maxHeight)
    {
        // 1. Вычисляем новую позицию, прибавляя скорость
        float newX = Position.X + Velocity.X;
        float newY = Position.Y + Velocity.Y;

        // 2. Проверяем столкновение с левой/правой границей
        if (newX <= 0 || newX + Image.Width >= maxWidth)
        {
            Velocity = new PointF(-Velocity.X, Velocity.Y); // разворот по X
            newX = Math.Clamp(newX, 0, maxWidth - Image.Width);
        }

        // 3. Проверяем столкновение с верхней/нижней границей
        if (newY <= 0 || newY + Image.Height >= maxHeight)
        {
            Velocity = new PointF(Velocity.X, -Velocity.Y); // разворот по Y
            newY = Math.Clamp(newY, 0, maxHeight - Image.Height);
        }

        // 4. Устанавливаем новую позицию
        Position = new PointF(newX, newY);

        // 5. Отзеркаливаем, если нужно
        if (Velocity.X > 0)
            _currentImage = _imageRight;
        else if (Velocity.X < 0)
            _currentImage = _imageLeft;
    }
}
