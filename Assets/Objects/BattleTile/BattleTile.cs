using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum TileState {
    Transparent,
    Movement,
    Attack,
};

public class BattleTile : MonoBehaviour
{
    [SerializeField] Material AttackCell;
    [SerializeField] Material MovementCell;
    [SerializeField] Material TransparentCell;
    [SerializeField] TileState cellState = new TileState();

    [SerializeField] Renderer ColorMaterial;
    [SerializeField] GameObject HighlightGameObject;

    [SerializeField] bool isSelected = false;

    [SerializeField] Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        HighlightGameObject.SetActive(isSelected);
    }

    // Update is called once per frame
    void Update()
    {
        if (cellState == TileState.Attack)
        {
            SetMaterial(AttackCell);
        }

        if (cellState == TileState.Movement)
        {
            SetMaterial(MovementCell);
        }

        if (cellState == TileState.Transparent)
        {
            SetMaterial(TransparentCell);
        }
    }

    public Vector2 GetPosition ()
    {
        return position;
    }

    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            BattleUnit battleUnit = BattleSystem.Instance.GetSelectedUnit();
            if (battleUnit.CanMoveTo(this.position) && this.cellState == TileState.Movement)
            {
                battleUnit.SetBattleFieldPosition(this.position);
                BattleGrid.Instance.ResetGridState();
                battleUnit.FinishMovementPhase();
            }
        }
    }
    public void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HighlightGameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        HighlightGameObject.SetActive(false);
    }

    public void SetState(TileState state)
    {
        cellState = state;
    }

    private void SetMaterial (Material material)
    {
        ColorMaterial.material = material;
    }

}