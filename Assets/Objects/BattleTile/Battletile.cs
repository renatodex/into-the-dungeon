using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState {
    Transparent,
    Movement,
    Attack,
};

public class Battletile : MonoBehaviour
{
    [SerializeField] Material AttackCell;
    [SerializeField] Material MovementCell;
    [SerializeField] Material TransparentCell;
    [SerializeField] TileState cellState = new TileState();

    //[SerializeField] string[] = System.Enum.Get

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetState(TileState state)
    {
        cellState = state;
    }

    private void SetMaterial (Material material)
    {
        this.GetComponent<Renderer>().material = material;
    }

}