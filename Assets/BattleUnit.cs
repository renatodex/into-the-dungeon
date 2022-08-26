using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// This class describe a Unit inside the BattleField, which is different from the generic "Unit" model.
public class BattleUnit : MonoBehaviour
{
    [SerializeField] private Character unit;
    [SerializeField] private CharacterSO unitSO;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Vector2 battleFieldPosition;

    // Start is called before the first frame update
    void Start()
    {
        unit = unitSO.character;
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            BattleUnit hitUnit = hit.transform.GetComponent<BattleUnit>();

            if (hitUnit != null)
            {
                BattleGrid.Instance.ResetGridState();
                BattleSystem.Instance.SetSelectedUnit(hitUnit);
                List<BattleTile> walkableTiles = GetWalkableTiles(hitUnit);

                for (int i = 0; i < walkableTiles.Count(); i++)
                {
                    walkableTiles[i].SetState(TileState.Movement);
                }
            }
        }
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

    public List<BattleTile> GetWalkableTiles (BattleUnit unit)
    {
        Character character = unit.GetUnit();
        List<BattleTile> walkableTiles = new List<BattleTile>();
        for (int x = (int)(unit.GetBattleFieldPosition().x - character.movement); x <= (int)(unit.GetBattleFieldPosition().x + character.movement); x++)
        {
            for (int z = (int)(unit.GetBattleFieldPosition().y - character.movement); z <= (int)(unit.GetBattleFieldPosition().y + character.movement); z++)
            {
                // Manhattan Distance formula
                int AbsX = (int)Mathf.Abs(unit.GetBattleFieldPosition().x - x);
                int AbsZ = (int)Mathf.Abs(unit.GetBattleFieldPosition().y - z);

                if (AbsX + AbsZ <= character.movement && BattleGrid.Instance.WithinGridBounding(new Vector2(x, z)))
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
