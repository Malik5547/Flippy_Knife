using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    public bool[] coins;

    public LevelData(bool[] coinsData)
    {
        coins = coinsData;
        //coins[0] = coinsData[0];
        //coins[1] = coinsData[1];
        //coins[2] = coinsData[2];
    }
}
