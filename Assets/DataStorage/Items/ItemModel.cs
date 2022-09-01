﻿using System;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public string itemName;
    public int attackRange;
    public string itemType;
    public string description;
    public int physicalAttack;
    public int magicalAttack;
    public int physicalDefense;
    public int magicalDefense;
    public int maxDurability;
    public int currentDurability;
    public int sellPrice;
    public Sprite portrait;
    public Item(
        int id,
        string itemName,
        int attackRange = 1,
        string description = "",
        string itemType = "Misc",
        int physicalAttack = 0,
        int physicalDefense = 0,
        int magicalAttack = 0,
        int magicalDefense = 0,
        int maxDurability = 0,
        int sellPrice = 9999
    )
    {
        this.id = id;
        this.attackRange = attackRange;
        this.description = description;
        this.itemName = itemName;
        this.itemType = itemType;
        this.physicalAttack = physicalAttack;
        this.physicalDefense = physicalDefense;
        this.magicalAttack = magicalAttack;
        this.magicalDefense = magicalDefense;
        this.maxDurability = maxDurability;
        this.currentDurability = maxDurability;
        this.sellPrice = sellPrice;
    }
    public void SetPortrait(Sprite portrait)
    {
        this.portrait = portrait;
    }
}