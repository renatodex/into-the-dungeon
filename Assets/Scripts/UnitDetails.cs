using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitDetailsUIMode
{
    Canvas,
    GameObject,
}

public class UnitDetails : MonoBehaviour
{
    [SerializeField] private BattleUnit battleUnitPrefab;
    [SerializeField] private TMPro.TextMeshProUGUI unitNameLabel;
    [SerializeField] private TMPro.TextMeshProUGUI unitHpText;
    [SerializeField] private TMPro.TextMeshProUGUI unitMpText;
    [SerializeField] private Slider unitCurrentHpBar;
    [SerializeField] private Slider unitCurrentMpBar;
    [SerializeField] private TMPro.TextMeshProUGUI unitLevel;
    [SerializeField] private Image unitPortrait;
    [SerializeField] private UnitDetailsUIMode uiMode = UnitDetailsUIMode.GameObject;

    #region Unity Methods

    void Start()
    {
        BattleUnit selectedBattleUnit = BattleSystem.Instance.GetSelectedUnit();
    }

    private void Update()
    {
        BattleUnit battleUnit = getBattleUnitByMode(uiMode);

        renderUnitHpBar(battleUnit, unitCurrentHpBar);

        renderUnitMpBar(battleUnit, unitCurrentMpBar);

        if (uiMode == UnitDetailsUIMode.Canvas)
        {
            renderUnitName(battleUnit, unitNameLabel);

            renderUnitLevel(battleUnit, unitLevel);

            renderUnitHpMpText(battleUnit, unitHpText, unitMpText);

            renderUnitPortrait(battleUnit, unitPortrait);
        }

        if (uiMode == UnitDetailsUIMode.GameObject)
        {
            hideCanvasIfUnitIsDead(battleUnit, this.GetComponent<Canvas>());

            showHpBarOnSelectedOrHover(battleUnit, unitCurrentHpBar);
        }
    }

    #endregion

    #region Private Methods

    private BattleUnit getBattleUnitByMode(UnitDetailsUIMode uiMode)
    {
        if (uiMode == UnitDetailsUIMode.Canvas)
        {
            return BattleSystem.Instance.GetSelectedUnit();
        }
        else
        {
            return battleUnitPrefab;
        }
    }

    private void hideCanvasIfUnitIsDead(BattleUnit battleUnit, Canvas canvas)
    {
        if (battleUnit.GetUnitState() == UnitState.Dead)
        {
            canvas.enabled = false;
        }
    }

    private void renderUnitLevel (BattleUnit battleUnit, TMPro.TextMeshProUGUI label = null)
    {
        if (label != null)
        {
            label.text = battleUnit.GetUnit().level.ToString("D2");
        }
    }

    private void renderUnitName (BattleUnit battleUnit, TMPro.TextMeshProUGUI label = null)
    {
        if (label != null)
        {
            label.text = battleUnit.GetUnit().unitName;
        }
    }

    private void renderUnitHpMpText(BattleUnit battleUnit, TMPro.TextMeshProUGUI hpLabel, TMPro.TextMeshProUGUI mpLabel)
    {
        Character character = battleUnit.GetUnit();

        string maxHpFormatted = character.maxHp.ToString("D3");
        string currentHpFormatted = character.currentHp.ToString("D3");
        string maxMpFormatted = character.maxMp.ToString("D3");
        string currentMpFormatted = character.currentMp.ToString("D3");

        if (unitHpText != null)
        {
            unitHpText.text = $"{currentHpFormatted}/{maxHpFormatted}";
        }

        if (unitMpText != null)
        {
            unitMpText.text = $"{currentMpFormatted}/{maxMpFormatted}";
        }
    }

    private void renderUnitHpBar (BattleUnit battleUnit, Slider sliderBar = null)
    {
        Character character = battleUnit.GetUnit();

        if (sliderBar != null)
        {
            if (character.maxHp == 0)
            {
                sliderBar.value = 0;
                sliderBar.enabled = false;
            }
            else
            {
                sliderBar.value = (100f * character.currentHp / character.maxHp) / 100f;
                sliderBar.enabled = true;
            }
        }
    }

    private void showHpBarOnSelectedOrHover (BattleUnit battleUnit, Slider slider)
    {
        if (battleUnit.IsSelectedUnit())
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(
                battleUnit.IsHovered()
            );
        }
    }

    private void renderUnitMpBar (BattleUnit battleUnit, Slider sliderBar = null)
    {
        Character character = battleUnit.GetUnit();

        if (sliderBar != null)
        {
            if (character.maxMp == 0)
            {
                sliderBar.value = 0;
            }
            else
            {
                sliderBar.value = (100f * character.currentMp / character.maxMp) / 100f;
            }
        }
    }

    private void renderUnitPortrait (BattleUnit battleUnit, Image portrait = null)
    {
        if (portrait != null)
        {
            portrait.sprite = battleUnit.GetUnit().portrait;
        }
    }

    #endregion
}
