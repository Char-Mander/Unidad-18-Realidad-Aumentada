using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { FIRE, WATER, ELECTRIC}

[System.Serializable]
public class Monster
{
    public string mName;
    public int maxHp = 100;
    public int _hp;
    public int currentHp
    {
        get { return _hp; }
        set { _hp = Mathf.Clamp(value, 0, maxHp);  }
    }

    public MonsterType mType;

    private void Start()
    {
        _hp = maxHp;
    }

    public Monster(MonsterSCR mData)
    {
        mName = mData._mName;
        maxHp = mData._maxHp;
        currentHp = maxHp;
        mType = mData._mType;
    }
}
