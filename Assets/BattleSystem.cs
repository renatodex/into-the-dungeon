using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleUnit selectedUnit;
    [SerializeField] private List<BattleUnit> combatUnits;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button movementButton;
    [SerializeField] private CharacterDatabase characterDatabase;
    public static BattleSystem Instance;

    #region Unity Events

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (selectedUnit.GetUnitState() == UnitState.Movement)
        {
            selectedUnit.StartMovementPhase();
        }

        if (selectedUnit.GetUnitState() == UnitState.Attack)
        {
            selectedUnit.StartAttackPhase();
        }
    }

    #endregion

    #region Public Interface

    public void StartUnitTurn(BattleUnit unit)
    {
        ResetAllUnitStates();
        SetSelectedUnit(unit);
        unit.SetUnitState(UnitState.Movement);
        unit.SetOutlineWidth(2f);
        BattleSystemUI.Instance.SetupOnUnitAttack();
    }

    public BattleUnit GetSelectedUnit ()
    {
        return selectedUnit;
    }

    #endregion

    #region Private Interface

    private void ResetAllUnitStates ()
    {
        foreach (BattleUnit combatUnit in combatUnits)
        {
            if (combatUnit.GetUnitState() != UnitState.Dead)
            {
                combatUnit.SetUnitState(UnitState.Idle);
            }
        }
    }

    public void SetSelectedUnit (BattleUnit unit)
    {
        selectedUnit = unit;
    }

    #endregion
}
