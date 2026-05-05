using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;

namespace AquaManager.Presentation.Extensions;

public static class AquariumFactoryExtension
{
    public static Image GetAquariumImage(this AquariumFactory aquariumFactory, AquariumType type)
    {
        string name = type.ToString().ToLower();
        return (Image)(Properties.Resources.ResourceManager.GetObject(name + "Aquarium") ?? Properties.Resources.defaultAquarium);
    }

    public static string GetAquariumDescription(this AquariumFactory aquariumFactory, AquariumType type)
    {
        return $"вместимость: {aquariumFactory.GetAquariumCapacity(type)} рыбок";
    }
}
