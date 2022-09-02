using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRolls : MonoBehaviour
{
    [SerializeField] private DeterministicDiceRoller deterministicDiceRoller;
    [SerializeField] private DeterministicDice d20;

    public static GameRolls Instance;

    public void Awake()
    {
        //d20.gameObject.SetActive(false);
        Instance = this;
    }

    public int RollD20 ()
    {
        int result = GetRandomNumber(1, 20);
        List<int> wantedList = new List<int>(new int[] { result });
        deterministicDiceRoller.wantedRoll = wantedList;
        return result;
    }

    public IEnumerator AnimateRollD20 ()
    {
        Debug.Log("Rolling Now");
        //d20.gameObject.SetActive(true);

        new WaitForSeconds(1f);

        deterministicDiceRoller.rollDice = true;

        new WaitForSeconds(1f);

        while (!d20.diceSleeping)
        {
            yield return null;
        }

        new WaitForSeconds(8f);

        Debug.Log("Roll Finished");

        //d20.gameObject.SetActive(false);

        //deterministicDiceRoller.rollDice = false;

        yield return null;
    }

    public int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
