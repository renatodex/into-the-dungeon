using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class describe a Unit inside the BattleField, which is different from the generic "Unit" model.
public enum UnitState
{
    Idle,
    Movement,
    Attack,
    Dead,
}
public class BattleUnit : MonoBehaviour
{
    [SerializeField] private Character unit;
    [SerializeField] private CharacterSO unitSO;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Vector2 battleFieldPosition;
    [SerializeField] private Outline outlineModel;
    [SerializeField] private UnitState unitState = UnitState.Idle;
    [SerializeField] private bool hovered = false;
    [SerializeField] private Animator animator;

    #region Unity Events

    void Start()
    {
        unit = Instantiate(unitSO).character;

        if (unitSO.initialWeapon != null)
        {
            unit.mainWeapon = unitSO.initialWeapon.item;
        }
    }

    void Update()
    {
        BattleTile battleTile = BattleGrid.Instance.GetTileAtPosition(battleFieldPosition);
        this.transform.position = battleTile.transform.position;

        if (Input.GetKey(KeyCode.Escape) && IsSelectedUnit())
        {
            SetUnitState(UnitState.Movement);
        }
    }

    private void OnMouseDown()
    {
        if (GetUnitState() != UnitState.Dead)
        {
            BattleUnit unit = BattleSystem.Instance.GetSelectedUnit();

            if (unit && unit.GetUnitState() == UnitState.Attack && !IsSelectedUnit())
            {
                if (unit.IsOnMyAttackRange(this))
                {
                    StartCoroutine(TakeDamageOnWeaponAttack((unit)));
                }
                else
                {
                    Debug.Log("Not on attack range!");
                }
            }
            else
            {
                BattleSystem.Instance.StartUnitTurn(this);
            }
        }
    }

    private void OnMouseOver()
    {
        if (GetUnitState() != UnitState.Dead)
        {
            SetHovered(true);
            SetOutlineWidth(2f);
        }
    }

    private void OnMouseExit()
    {
        if (GetUnitState() != UnitState.Dead)
        {
            SetHovered(false);
            SetOutlineWidth(0f);
        }
    }

    #endregion

    #region Public Interface
    public UnitState GetUnitState ()
    {
        return unitState;
    }
    public bool IsSelectedUnit()
    {
        return BattleSystem.Instance.GetSelectedUnit() == this;
    }

    public bool IsHovered()
    {
        return hovered;
    }

    public void StartMovementPhase ()
    {
        SetOutlineWidth(2f);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            BattleGrid.Instance.ResetGridState();
            List<BattleTile> walkableTiles = GetWalkableTiles(this);

            for (int i = 0; i < walkableTiles.Count(); i++)
            {
                walkableTiles[i].SetState(TileState.Movement);
            }
        }
    }

    public void SetUnitState(UnitState state)
    {
        this.unitState = state;
    }
    public void SetOutlineWidth(float width)
    {
        outlineModel.OutlineWidth = width;
    }

    public void FinishMovementPhase ()
    {
        SetOutlineWidth(0f);
        SetUnitState(UnitState.Idle);
    }

    public void StartAttackPhase ()
    {
        BattleGrid.Instance.ResetGridState();
        List<BattleTile> walkableTiles = GetAttackTiles(this);

        for (int i = 0; i < walkableTiles.Count(); i++)
        {
            walkableTiles[i].SetState(TileState.Attack);
        }
    }

    public bool CanMoveTo(Vector2 position)
    {
        List<BattleTile> walkableTiles = GetWalkableTiles(this);
        IEnumerable<BattleTile> match = walkableTiles.Where(battleTile => battleTile.GetPosition() == position);
        return match.Count() > 0;
    }

    public void SetBattleFieldPosition (Vector2 position)
    {
        battleFieldPosition = position;
    }

    public Character GetUnit()
    {
        return unit;
    }

    #endregion

    #region Private Interface

    private Vector2 GetBattleFieldPosition()
    {
        return battleFieldPosition;
    }

    private bool IsOnMyAttackRange(BattleUnit unit)
    {
        List<BattleTile> attackTiles = GetAttackTiles(this);

        foreach (BattleTile attackTile in attackTiles)
        {
            if (attackTile.GetPosition() == unit.GetBattleFieldPosition())
            {
                return true;
            }
        }

        return false;
    }

    private List<BattleTile> GetAttackTiles(BattleUnit unit)
    {
        return GetCircularWalkableTiles(
            unit.GetBattleFieldPosition(),
            unit.GetUnit().GetAttackRange()
        );
    }

    private List<BattleTile> GetWalkableTiles(BattleUnit unit)
    {
        return GetCircularWalkableTiles(
            unit.GetBattleFieldPosition(),
            unit.GetUnit().movement
        );
    }

    private List<BattleTile> GetCircularWalkableTiles(Vector2 battleFieldPosition, int value)
    {
        List<BattleTile> walkableTiles = new List<BattleTile>();
        for (int x = (int)(battleFieldPosition.x - value); x <= (int)(battleFieldPosition.x + value); x++)
        {
            for (int z = (int)(battleFieldPosition.y - value); z <= (int)(battleFieldPosition.y + value); z++)
            {
                // Manhattan Distance formula
                int AbsX = (int)Mathf.Abs(battleFieldPosition.x - x);
                int AbsZ = (int)Mathf.Abs(battleFieldPosition.y - z);

                if (AbsX + AbsZ <= value && BattleGrid.Instance.WithinGridBounding(new Vector2(x, z)))
                {
                    BattleTile tile = BattleGrid.Instance.GetTileAtPosition(new Vector2(x, z));
                    if (tile)
                    {
                        walkableTiles.Add(tile);
                    }
                }
            }
        }

        return walkableTiles;
    }

    private bool HasAnimator()
    {
        return animator != null;
    }

    private void TriggerDieAnimation()
    {
        if (this.HasAnimator()) animator.SetTrigger("Die");
    }

    private void TriggerAttackAnimation()
    {
        if (this.HasAnimator()) animator.SetTrigger("BrawlAttack");
    }

    private void TriggerHitAnimation()
    {
        if (this.HasAnimator()) animator.SetTrigger("Hit");
    }
    IEnumerator TakeDamageOnWeaponAttack(BattleUnit attackerUnit)
    {
        attackerUnit.TriggerAttackAnimation();

        Character attacker = attackerUnit.GetUnit();
        BattleUnit defenderUnit = this;
        Character defender = this.GetUnit();

        int rollValue1 = GameRolls.Instance.RollD20();
        int rollValue2 = GameRolls.Instance.RollD20();
        int attackValue = attackerUnit.GetUnit().GetAttackValue();
        int finalValue = Mathf.Max(rollValue1, rollValue2) + attackValue;
        int defenderDefense = defender.GetDefenseForWeapon(attacker.GetWeapon());

        yield return new WaitForSeconds(0.6f);

        if (finalValue > defender.GetDefenseForWeapon(attacker.GetWeapon()))
        {
            Debug.Log(
                "Attack Succeeeded. (" + finalValue + ") <-> (" + defenderDefense + ")"
            );

            int damageValue = attacker.GetWeapon().RollDamageValue();

            bool damageWillKill = defenderUnit.GetUnit().DamageWillKill(
                damageValue
            );

            if (damageWillKill)
            {
                defenderUnit.TriggerDieAnimation();
                defenderUnit.SetUnitState(UnitState.Dead);
            }
            else
            {
                defenderUnit.TriggerHitAnimation();
            }

            this.GetUnit().TakeDamage(damageValue);
        }
        else
        {
            Debug.Log(
                "Attack Missed. (" + finalValue + ") <-> (" + defenderDefense + ")"
            );
        }

        yield return null;
    }

    private void SetHovered(bool hovered)
    {
        this.hovered = hovered;
    }

    #endregion
}