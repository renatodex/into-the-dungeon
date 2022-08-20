using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class describe a Unit inside the BattleField, which is different from the generic "Unit" model.
public class BattleUnit : MonoBehaviour
{
    [SerializeField] private Character unit;
    [SerializeField] private CharacterSO unitSO;
    [SerializeField] private BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
        unit = unitSO.character;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                BattleUnit hitUnit = hit.transform.GetComponent<BattleUnit>();
                if (hitUnit != null)
                {
                    Debug.Log(hitUnit.GetUnit());
                    battleSystem.SetSelectedUnit(hitUnit.GetUnit());
                }
            }
        }
    }

    private Character GetUnit()
    {
        return unit;
    }
}
