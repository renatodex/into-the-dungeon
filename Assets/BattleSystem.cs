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

    public void StartUnitTurn(BattleUnit unit)
    {
        ResetAllUnitStates();
        unit.SetUnitState(UnitState.Movement);
        SetSelectedUnit(unit);
        SetupOnUnitAttack();
        unit.SetOutlineWidth(2f);
    }

    public BattleUnit GetSelectedUnit ()
    {
        return selectedUnit;
    }

    public void ResetAllUnitStates ()
    {
        foreach (BattleUnit combatUnit in combatUnits)
        {
            combatUnit.SetUnitState(UnitState.Idle);
        }
    }

    public void SetSelectedUnit (BattleUnit unit)
    {
        selectedUnit = unit;
    }

    public void SetupOnUnitAttack ()
    {
        attackButton.onClick.AddListener(OnUnitAttack);
    }

    public void OnUnitAttack ()
    {
        selectedUnit.SetUnitState(UnitState.Attack);
        selectedUnit.StartAttackPhase();
    }
}
