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

    // Start is called before the first frame update
    void Start()
    {
        unit = unitSO.character;
    }

    public UnitState GetUnitState ()
    {
        return unitState;
    }

    public void SetUnitState (UnitState state)
    {
        this.unitState = state;
    }

    private void OnMouseDown()
    {
        BattleSystem.Instance.StartUnitTurn(this);
    }

    private void OnMouseOver()
    {

        SetHovered(true);
        SetOutlineWidth(2f);
    }

    private void OnMouseExit()
    {
        SetHovered(false);
        SetOutlineWidth(0f);
    }

    public void SetHovered (bool hovered)
    {
        this.hovered = hovered;
    }
    public bool IsHovered ()
    {
        return hovered;
    }

    public void SetOutlineWidth(float width)
    {
        outlineModel.OutlineWidth = width;
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
        Debug.Log("Attack please");
    }

    // Update is called once per frame
    void Update()
    {
        BattleTile battleTile = BattleGrid.Instance.GetTileAtPosition(battleFieldPosition);
        this.transform.position = battleTile.transform.position;
    }

    public Vector2 GetBattleFieldPosition()
    {
        return battleFieldPosition;
    }

    public bool CanMoveTo(Vector2 position)
    {
        List<BattleTile> walkableTiles = GetWalkableTiles(this);
        IEnumerable<BattleTile> match = walkableTiles.Where(battleTile => battleTile.GetPosition() == position);
        return match.Count() > 0;
    }

    public List<BattleTile> GetAttackTiles (BattleUnit unit)
    {
        return GetCircularWalkableTiles(
            unit.GetBattleFieldPosition(),
            unit.GetUnit().GetAttackRange()
        );
    }

    public List<BattleTile> GetWalkableTiles (BattleUnit unit)
    {
        return GetCircularWalkableTiles(
            unit.GetBattleFieldPosition(),
            unit.GetUnit().movement
        );
    }

    public List<BattleTile> GetCircularWalkableTiles (Vector2 battleFieldPosition, int value)
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

    public void SetBattleFieldPosition (Vector2 position)
    {
        battleFieldPosition = position;
    }

    public Character GetUnit()
    {
        return unit;
    }
}
