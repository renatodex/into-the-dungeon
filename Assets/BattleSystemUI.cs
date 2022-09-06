using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    public static BattleSystemUI Instance;

    [SerializeField] private Button attackButton;
    [SerializeField] private Button movementButton;
    private void Awake()
    {
        Instance = this;
    }
    public void SetupOnUnitAttack()
    {
        attackButton.onClick.AddListener(OnUnitAttack);
    }

    public void OnUnitAttack()
    {
        BattleUnit selectedUnit = BattleSystem.Instance.GetSelectedUnit();
        selectedUnit.SetUnitState(UnitState.Attack);
        selectedUnit.StartAttackPhase();
    }
}
