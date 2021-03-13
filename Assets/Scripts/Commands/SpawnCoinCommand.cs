using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinCommand : Command
{
    public GameObject coinItem;
    public int minCoinAmount, maxCoinAmount;

    public static int coinsSpawned;

    private void Awake()
    {
        coinsSpawned = 0;
    }

    public override void execute()
    {
        int coinAmount = Random.Range(minCoinAmount, maxCoinAmount + 1);

        for(int i = 0; i < coinAmount; i++)
        {
            var rx = Random.Range(-1.0f, -0.01f);
            var ry = Random.Range(-0.66f, 0.66f);

            GameObject go = Instantiate(coinItem, transform.position + new Vector3(rx, ry, 0), Quaternion.identity);
            coinsSpawned += 1;
        }
    }
}
