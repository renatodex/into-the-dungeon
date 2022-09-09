using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            StartMovementPhase(selectedUnit);
        }

        if (selectedUnit.GetUnitState() == UnitState.Attack)
        {
            StartAttackPhase(selectedUnit);
        }
    }

    #endregion

    #region Movement Methods
    public void StartMovementPhase(BattleUnit battleUnit)
    {
        battleUnit.SetOutlineWidth(2f);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            BattleGrid.Instance.ResetGridState();
            BattleGrid.Instance.SetMovementTilesForUnit(battleUnit);
        }
    }

    public void ExecuteMovement(BattleTile battleTile)
    {
        BattleUnit selectedBattleUnit = GetSelectedUnit();
        Vector2 battleTilePosition = battleTile.GetPosition();

        if (selectedBattleUnit.CanMoveTo(battleTilePosition) && battleTile.GetState() == TileState.Movement)
        {
            selectedBattleUnit.SetBattleFieldPosition(battleTilePosition);
            BattleGrid.Instance.ResetGridState();
            FinishMovementPhase(selectedBattleUnit);
        }
    }
    public void FinishMovementPhase(BattleUnit battleUnit)
    {
        battleUnit.SetOutlineWidth(0f);
        battleUnit.SetUnitState(UnitState.Idle);
    }
    #endregion

    #region Attack Methods

    public void StartAttackPhase(BattleUnit battleUnit)
    {
        battleUnit.SetUnitState(UnitState.Attack);
        BattleGrid.Instance.ResetGridState();
        BattleGrid.Instance.SetAttackTilesForUnit(battleUnit);
    }

    public void ExecuteAttack(BattleUnit defender)
    {
        BattleUnit unit = BattleSystem.Instance.GetSelectedUnit();

        if (unit.IsOnMyAttackRange(defender))
        {
            StartCoroutine(defender.TakeDamageOnWeaponAttack((unit)));
            FinishAttackPhase(defender);
        }
        else
        {
            Debug.Log("Not on attack range!");
        }
    }

    public void FinishAttackPhase(BattleUnit battleUnit)
    {
        Debug.Log("Attack has finished!");
        StartUnitTurn(battleUnit);
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

    public bool IsSelectedUnitAttacking(BattleUnit defender)
    {
        return (selectedUnit && selectedUnit.GetUnitState() == UnitState.Attack && !IsSelectedUnit(defender));
    }

    public bool IsSelectedUnit(BattleUnit unit)
    {
        return BattleSystem.Instance.GetSelectedUnit() == unit;
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
