using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRolls : MonoBehaviour
{
    public static GameRolls Instance;

    public void Awake()
    {
        Instance = this;
    }

    public int RollD20 ()
    {
        return GetRandomNumber(1, 20);
    }

    public int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
