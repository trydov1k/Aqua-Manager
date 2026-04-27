using AquaManager.Domain.Constants;
using AquaManager.Domain.Models;

namespace AquaManager.Presentation.Models;

public class SwimmingFish
{
    public Fish Model { get; private set; }
    public PointF Position { get; set; }
    public PointF Velocity { get; set; }
    public Image Image { get; private set; } // Уменьшенное изображение

    private static Image ScaleImage(Image originalImage, int targetWidth, int targetHeight)
    {
        var newImage = new Bitmap(targetWidth, targetHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(originalImage, new Rectangle(0, 0, targetWidth, targetHeight));
        }
        return newImage;
    }

    public SwimmingFish(Fish fish, Image originalImage, float startX, float startY, 
        int targetWidth = GameConstants.StandartFishImageWidth, int targetHeight = GameConstants.StandartFishImageHeight)
    {
        Model = fish;
        Image = ScaleImage(originalImage, targetWidth, targetHeight);
        Position = new PointF(startX, startY);
        Random rnd = new Random();

        var speedMin = GameConstants.SwimmingFishVelocityMin;
        var speedMax = GameConstants.SwimmingFishVelocityMax;

        Velocity = new PointF(
            (float)(rnd.NextDouble() * (speedMax - speedMin) + speedMin),
            (float)(rnd.NextDouble() * (speedMax - speedMin) + speedMin)
        );
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
    }
}
