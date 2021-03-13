using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffer : MonoBehaviour, IInteractableStation
{
    private int storedCoins;

    private void Awake()
    {
        storedCoins = 0;
    }

    public void Interact(PlayerController player)
    {
        if (player.isHolding) return;

        var coinsToStore = player.GetCoins();
        storedCoins += coinsToStore;
        player.ResetCoins();

        print("storedCoins: " + storedCoins);
    }
}
