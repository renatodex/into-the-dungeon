using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetails : MonoBehaviour
{
    [SerializeField] private BattleUnit battleUnitPrefab;
    [SerializeField] private Character unitCharacter;
    [SerializeField] private TMPro.TextMeshProUGUI unitNameLabel;
    [SerializeField] private TMPro.TextMeshProUGUI unitHp;
    [SerializeField] private TMPro.TextMeshProUGUI unitMp;
    [SerializeField] private Slider unitCurrentHpBar;
    [SerializeField] private Slider unitCurrentMpBar;
    [SerializeField] private TMPro.TextMeshProUGUI unitLevel;
    [SerializeField] private Image unitPortrait;
    [SerializeField] private bool hideBars = false;
    [SerializeField] private bool guiMode = false;

    private BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
        unitCharacter = BattleSystem.Instance.GetSelectedUnit().GetUnit();
    }

    private void Update()
    {
        if (hideBars)
        {
            if (battleUnitPrefab == BattleSystem.Instance.GetSelectedUnit())
            {
                unitCurrentHpBar.gameObject.SetActive(true);
            } else
            {
                unitCurrentHpBar.gameObject.SetActive(
                    battleUnitPrefab.IsHovered()
                );
            }
        }

        if (guiMode)
        {
            unitCharacter = BattleSystem.Instance.GetSelectedUnit().GetUnit();
            
        } else
        {
            unitCharacter = battleUnitPrefab.GetUnit();
        }
        string maxHpFormatted = unitCharacter.maxHp.ToString("D3");
        string currentHpFormatted = unitCharacter.currentHp.ToString("D3");
        string maxMpFormatted = unitCharacter.maxMp.ToString("D3");
        string currentMpFormatted = unitCharacter.currentMp.ToString("D3");

        if (unitNameLabel != null)
        {
            unitNameLabel.text = unitCharacter.unitName;
        }

        if (unitHp != null)
        {
            unitHp.text = $"{currentHpFormatted}/{maxHpFormatted}";
        }

        if (unitMp != null)
        {
            unitMp.text = $"{currentMpFormatted}/{maxMpFormatted}";
        }

        if (unitLevel != null)
        {
            unitLevel.text = unitCharacter.level.ToString("D2");
        }

        if (unitCurrentHpBar != null)
        {
            if (unitCharacter.maxHp == 0)
            {
                unitCurrentHpBar.value = 0;
                unitCurrentHpBar.enabled = false;
            }
            else
            {
                unitCurrentHpBar.value = (100f * unitCharacter.currentHp / unitCharacter.maxHp) / 100f;
                unitCurrentHpBar.enabled = true;
            }
        }

        if (unitCurrentMpBar != null)
        {
            if (unitCharacter.maxMp == 0)
            {
                unitCurrentMpBar.value = 0;
            }
            else
            {
                unitCurrentMpBar.value = (100f * unitCharacter.currentMp / unitCharacter.maxMp) / 100f;
            }
        }

        if (unitPortrait != null)
        {
            unitPortrait.sprite = unitCharacter.portrait;
        }

    }
}
