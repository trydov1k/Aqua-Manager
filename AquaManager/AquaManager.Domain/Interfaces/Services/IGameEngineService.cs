using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Services;

public interface IGameEngineService
{
    /// <summary>
    /// Игрок
    /// </summary>
    Player Player { get; }

    /// <summary>
    /// Запуск игры
    /// </summary>
    void Start();

    /// <summary>
    /// Остановка игры
    /// </summary>
    void Stop();

    /// <summary>
    /// Начать новую игру
    /// </summary>
    void NewGame();

    /// <summary>
    /// Загрузить другого игрока (чтобы продолжить старую игру)
    /// </summary>
    /// <param name="loadedPlayer">Игрок, который теперь станет основным</param>
    void LoadPlayer(Player loadedPlayer);

    /// <summary>
    /// Покормить всех рыбок
    /// </summary>
    /// <returns>true если успешно, иначе false</returns>
    bool FeedAllFish();

    /// <summary>
    /// Покормить рыбку по id
    /// </summary>
    /// <param name="fishId">id рыбки</param>
    /// <returns>true если успешно, иначе false</returns>
    bool FeedSingleFish(string fishId);

    /// <summary>
    /// Поменять воду в аквариуме
    /// </summary>
    /// <returns>true если успешно, иначе false</returns>
    bool ChangeWater();

    /// <summary>
    /// Купить рыбку
    /// </summary>
    /// <param name="type">Тип рыбки, для покупки</param>
    /// <returns>true если успешно, иначе false</returns>
    bool BuyFish(FishType type, string fishName);

    /// <summary>
    /// Купить аквариум
    /// </summary>
    /// <returns>true если успешно, иначе false</returns>
    bool BuyAquarium(string name);

    /// <summary>
    /// Метод для проверки можно ли купить рыбку
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>true елси можно купить, иначе false</returns>
    bool CanBuyFish(FishType type);

    /// <summary>
    /// Метод для проверки можно ли купить аквариум
    /// </summary>
    /// <returns>true елси можно купить, иначе false</returns>
    bool CanBuyAquarium();

    /// <summary>
    /// Удалить рыбку
    /// </summary>
    bool RemoveFish(string fishId);

    /// <summary>
    /// Переключиться на аквариум по id
    /// </summary>
    /// <param name="index">id аквариума, на который надо переключиться</param>
    /// <returns>true если успешно, иначе false</returns>
    bool SwitchAquarium(int index);

    /// <summary>
    /// Получить текущий аквариум
    /// </summary>
    /// <returns>Текущий аквариум</returns>
    Aquarium? GetCurrentAquarium();

    /// <summary>
    /// Сохранить игру
    /// </summary>
    void SaveGame();

    /// <summary>
    /// Загрузить игру
    /// </summary>
    void LoadGame();

    /// <summary>
    /// Удаление игрового движка
    /// </summary>
    void Dispose();
}
