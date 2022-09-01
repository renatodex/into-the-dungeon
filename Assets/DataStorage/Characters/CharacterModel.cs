using System;
using UnityEngine;

[Serializable]
public class Character
{
    public int id;
    public string unitName;
    public int maxHp;
    public int currentHp;
    public int maxMp;
    public int currentMp;
    public int level;
    public int movement;
    public Sprite portrait;
    public Item mainWeapon;

    public Character(int id, string unitName, int movement = 1, int maxHp = 0, int currentHp = 0, int maxMp = 0, int currentMp = 0, int level = 1)
    {
        this.id = id;
        this.unitName = unitName;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.maxMp = maxMp;
        this.currentMp = currentMp;
        this.level = level;
        this.movement = movement;
    }

    public void equipWeaponMain(Item item)
    {
        mainWeapon = item;
    }

    public void SetPortrait(Sprite portrait)
    {
        this.portrait = portrait;
    }

    public int GetAttackRange ()
    {
        return mainWeapon.attackRange;
    }
}