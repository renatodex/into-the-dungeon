using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetails : MonoBehaviour
{
    [SerializeField] private Character unit;
    [SerializeField] private TMPro.TextMeshProUGUI unitNameLabel;
    [SerializeField] private TMPro.TextMeshProUGUI unitHp;
    [SerializeField] private TMPro.TextMeshProUGUI unitMp;
    [SerializeField] private Slider unitCurrentHpBar;
    [SerializeField] private Slider unitCurrentMpBar;
    [SerializeField] private TMPro.TextMeshProUGUI unitLevel;
    [SerializeField] private Image unitPortrait;

    private BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
        unit = battleSystem.GetSelectedUnit();
    }

    private void Update()
    {
        unit = battleSystem.GetSelectedUnit();
        unitNameLabel.text = unit.unitName;

        string maxHpFormatted = unit.maxHp.ToString("D3");
        string currentHpFormatted = unit.currentHp.ToString("D3");
        string maxMpFormatted = unit.maxMp.ToString("D3");
        string currentMpFormatted = unit.currentMp.ToString("D3");

        unitHp.text = $"{currentHpFormatted}/{maxHpFormatted}";
        unitMp.text = $"{currentMpFormatted}/{maxMpFormatted}";
        unitLevel.text = unit.level.ToString("D2");

        if (unit.maxHp == 0)
        {
            unitCurrentHpBar.value = 0;
        }
        else
        {
            unitCurrentHpBar.value = (100f * unit.currentHp / unit.maxHp) / 100f;
        }

        if (unit.maxMp == 0)
        {
            unitCurrentMpBar.value = 0;
        }
        else
        {
            unitCurrentMpBar.value = (100f * unit.currentMp / unit.maxMp) / 100f;
        }

        unitPortrait.sprite = unit.portrait;
    }
}
