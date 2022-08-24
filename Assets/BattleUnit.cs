using System.Collections;
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
                Character character = hitUnit.GetUnit();
                Debug.Log(character.unitName);
                battleSystem.SetSelectedUnit(hitUnit);

                BattleGrid.Instance.ResetGridState();

                for (int x = (int)(battleFieldPosition.x - character.movement); x <= (int)(battleFieldPosition.x + character.movement); x++)
                {
                    for (int z = (int)(battleFieldPosition.y - character.movement); z <= (int)(battleFieldPosition.y + character.movement); z++)
                    {
                        // Manhattan Distance formula
                        int AbsX = (int) Mathf.Abs(battleFieldPosition.x - x);
                        int AbsZ = (int) Mathf.Abs(battleFieldPosition.y - z);

                        if (AbsX + AbsZ <= character.movement && BattleGrid.Instance.WithinGridBounding(new Vector2(x, z)))
                        {
                            BattleTile tile = BattleGrid.Instance.GetTileAtPosition(new Vector2(x, z));
                            tile.SetState(TileState.Movement);
                        }
                    }
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

    public bool CanMoveTo(Vector2 position)
    {
        Character character = GetUnit();

        bool canMoveToTile = false;
        for (int x = (int)(battleFieldPosition.x - character.movement); x <= (int)(battleFieldPosition.x + character.movement); x++)
        {
            for (int z = (int)(battleFieldPosition.y - character.movement); z <= (int)(battleFieldPosition.y + character.movement); z++)
            {
                // Manhattan Distance formula
                int AbsX = (int)Mathf.Abs(battleFieldPosition.x - x);
                int AbsZ = (int)Mathf.Abs(battleFieldPosition.y - z);

                if (AbsX + AbsZ <= character.movement && BattleGrid.Instance.WithinGridBounding(new Vector2(x, z)))
                {
                    BattleTile tile = BattleGrid.Instance.GetTileAtPosition(new Vector2(x, z));
                    
                    if (tile && tile.GetPosition() == position)
                    {
                        canMoveToTile = true;
                    }
                }
            }
        }

        return canMoveToTile;
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
