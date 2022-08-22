using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleUnit selectedUnit;
    private CharacterDatabase characterDatabase;
    public static BattleSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        characterDatabase = GameObject.FindObjectOfType<CharacterDatabase>();
    }

    public BattleUnit GetSelectedUnit ()
    {
        return selectedUnit;
    }

    public void SetSelectedUnit (BattleUnit unit)
    {
        selectedUnit = unit;
    }
}
