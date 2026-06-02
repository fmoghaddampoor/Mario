namespace MarioEngine.Core.Player;

using System;

/// <summary>Manages coin collection, spending, and star coin tracking.</summary>
internal sealed class PlayerCoinManager
{
    /// <summary>Number of coins required to earn an extra life.</summary>
    public static readonly int CoinsPerLife = 100;

    /// <summary>Gets the current coin count.</summary>
    public int Coins { get; private set; }

    /// <summary>Gets the number of star coins collected.</summary>
    public int StarCoins { get; private set; }

    /// <summary>Raised when the coin count changes, providing the new count.</summary>
    public event Action<int>? OnCoinChanged;

    /// <summary>Adds a single regular coin.</summary>
    public void AddCoin()
    {
        Coins++;
        OnCoinChanged?.Invoke(Coins);
    }

    /// <summary>Adds a single star coin.</summary>
    public void AddStarCoin()
    {
        StarCoins++;
    }

    /// <summary>Tries to spend a given amount of coins.</summary>
    /// <param name="amount">The number of coins to spend.</param>
    /// <returns><c>true</c> if the coins were spent; <c>false</c> if insufficient.</returns>
    public bool TrySpendCoins(int amount)
    {
        if (Coins < amount)
            return false;

        Coins -= amount;
        OnCoinChanged?.Invoke(Coins);
        return true;
    }
}
