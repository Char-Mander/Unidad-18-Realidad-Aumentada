using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { FIRE, WATER, ELECTRIC}

public class Monster : MonoBehaviour
{
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
        maxHp = mData._maxHp;
        currentHp = maxHp;
        mType = mData._mType;
    }
}
