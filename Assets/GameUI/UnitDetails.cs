using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetails : MonoBehaviour
{
    [SerializeField] private Character unitCharacter;
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
        unitCharacter = BattleSystem.Instance.GetSelectedUnit().GetUnit();
    }

    private void Update()
    {
        unitCharacter = BattleSystem.Instance.GetSelectedUnit().GetUnit();
        unitNameLabel.text = unitCharacter.unitName;

        string maxHpFormatted = unitCharacter.maxHp.ToString("D3");
        string currentHpFormatted = unitCharacter.currentHp.ToString("D3");
        string maxMpFormatted = unitCharacter.maxMp.ToString("D3");
        string currentMpFormatted = unitCharacter.currentMp.ToString("D3");

        unitHp.text = $"{currentHpFormatted}/{maxHpFormatted}";
        unitMp.text = $"{currentMpFormatted}/{maxMpFormatted}";
        unitLevel.text = unitCharacter.level.ToString("D2");

        if (unitCharacter.maxHp == 0)
        {
            unitCurrentHpBar.value = 0;
        }
        else
        {
            unitCurrentHpBar.value = (100f * unitCharacter.currentHp / unitCharacter.maxHp) / 100f;
        }

        if (unitCharacter.maxMp == 0)
        {
            unitCurrentMpBar.value = 0;
        }
        else
        {
            unitCurrentMpBar.value = (100f * unitCharacter.currentMp / unitCharacter.maxMp) / 100f;
        }

        unitPortrait.sprite = unitCharacter.portrait;
    }
}
