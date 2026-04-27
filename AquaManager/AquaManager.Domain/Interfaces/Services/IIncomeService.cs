
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using System.Timers;

namespace AquaManager.Domain.Interfaces.Services;

public interface IIncomeService
{
    /// <summary>
    /// Событие возникает после начисления дохода
    /// </summary>
    event EventHandler<IncomeEventArgs> IncomeGenerated;

    /// <summary>
    /// Запуск начисления дохода
    /// </summary>
    void Start();

    /// <summary>
    /// Остановка начисления дохода
    /// </summary>
    void Stop();

    /// <summary>
    /// Обновить ссылку на игрока (например, после загрузки сохранения)
    /// </summary>
    void UpdatePlayer(Player player);

    /// <summary>
    /// Освобождение ресурсов
    /// </summary>
    void Dispose();
}
