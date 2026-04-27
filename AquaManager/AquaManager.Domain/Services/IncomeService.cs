using AquaManager.Domain.Constants;
using AquaManager.Domain.Interfaces.Services;
using AquaManager.Domain.Models;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AquaManager.Domain.Services;

/// <summary>
/// Сервис для начисления пассивного дохода игроку
/// </summary>
public class IncomeService : IIncomeService
{
    private readonly Timer _incomeTimer;
    private Player _player;

    public event EventHandler<IncomeEventArgs> IncomeGenerated;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    /// <param name="player">Текущий игрок</param>
    public IncomeService(Player player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _incomeTimer = new Timer(GameConstants.IncomeIntervalSeconds * 1000);
        _incomeTimer.Elapsed += OnIncomeTick;
        _incomeTimer.AutoReset = true;
    }

    public void Start()
    {
        if (_player == null) return;
        _incomeTimer.Start();
    }

    public void Stop()
    {
        _incomeTimer.Stop();
    }
    
    public void UpdatePlayer(Player player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    /// <summary>
    /// Рассчитать общее количество живых рыбок во всех аквариумах
    /// </summary>
    private int CalculateTotalLiveFish()
    {
        if (_player?.Aquariums == null) return 0;

        int total = 0;
        foreach (var aquarium in _player.Aquariums)
        {
            total += aquarium.GetLiveFishCount();
        }
        return total;
    }

    /// <summary>
    /// Обработчик тика таймера
    /// </summary>
    private void OnIncomeTick(object sender, ElapsedEventArgs e)
    {
        if (_player == null) return;

        int liveFishCount = CalculateTotalLiveFish();
        if (liveFishCount == 0) return;

        decimal income = liveFishCount * GameConstants.IncomePerLiveFish;
        _player.AddMoney(income);

        OnIncomeGenerated(income);
    }

    /// <summary>
    /// Вызов события IncomeGenerated
    /// </summary>
    private void OnIncomeGenerated(decimal amount)
    {
        IncomeGenerated?.Invoke(this, new IncomeEventArgs(amount));
    }

    public void Dispose()
    {
        _incomeTimer?.Stop();
        _incomeTimer?.Dispose();
    }
}

/// <summary>
/// Аргументы события начисления дохода
/// </summary>
public class IncomeEventArgs : EventArgs
{
    public decimal Amount { get; }

    public IncomeEventArgs(decimal amount)
    {
        Amount = amount;
    }
}