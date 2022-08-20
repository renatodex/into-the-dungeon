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
    public Sprite portrait;

    public Character(int id, string unitName, int maxHp = 0, int currentHp = 0, int maxMp = 0, int currentMp = 0, int level = 1)
    {
        this.id = id;
        this.unitName = unitName;
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.maxMp = maxMp;
        this.currentMp = currentMp;
        this.level = level;
    }

    public void SetPortrait(Sprite portrait)
    {
        this.portrait = portrait;
    }
}