using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Character selectedUnit;

    private CharacterDatabase characterDatabase;
    void Start()
    {
        characterDatabase = GameObject.FindObjectOfType<CharacterDatabase>();
        selectedUnit = characterDatabase.characters[0];
    }

    public Character GetSelectedUnit ()
    {
        return selectedUnit;
    }

    public void SetSelectedUnit (Character unit)
    {
        selectedUnit = unit;
    }
}
