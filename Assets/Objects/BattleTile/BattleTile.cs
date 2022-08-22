using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //[SerializeField] string[] = System.Enum.Get

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

    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    public void OnMouseDown()
    {
        BattleUnit battleUnit = BattleSystem.Instance.GetSelectedUnit();
        battleUnit.SetBattleFieldPosition(this.position);
    }

    public void OnMouseEnter()
    {
        HighlightGameObject.SetActive(true);
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