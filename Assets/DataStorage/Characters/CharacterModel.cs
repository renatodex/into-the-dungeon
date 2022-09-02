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
    public int physicalAttack;
    public int physicalDefense;
    public int magicalAttack;
    public int magicalDefense;

    public Sprite portrait;
    public Item mainWeapon;

    public Character(
        int id,
        string unitName,
        int movement = 1,
        int maxHp = 0,
        int currentHp = 0,
        int maxMp = 0,
        int currentMp = 0,
        int level = 1,
        int physicalAttack = 0,
        int physicalDefense = 0,
        int magicalAttack = 0,
        int magicalDefense = 0
    )
    {
        this.id = id;
        this.unitName = unitName;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.maxMp = maxMp;
        this.currentMp = currentMp;
        this.level = level;
        this.movement = movement;
        this.physicalAttack = physicalAttack;
        this.physicalDefense = physicalDefense;
        this.magicalAttack = magicalAttack;
        this.magicalDefense = magicalDefense;
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
        return this.mainWeapon.attackRange;
    }

    public int GetAttackValue ()
    {
        return this.mainWeapon.GetAttackValue();
    }

    public void TakeDamage(int value)
    {
        if (this.currentHp - value < 0)
        {
            this.currentHp = 0;
        } else
        {
            this.currentHp -= value;
        }
    }

    public Item GetWeapon ()
    {
        return this.mainWeapon;
    }

    public int GetDefenseForWeapon (Item item)
    {
        if (item.attackType == AttackType.Physical)
        {
            return physicalDefense;
        }

        if (item.attackType == AttackType.Magical)
        {
            return magicalDefense;
        }

        return physicalDefense;
    }
}