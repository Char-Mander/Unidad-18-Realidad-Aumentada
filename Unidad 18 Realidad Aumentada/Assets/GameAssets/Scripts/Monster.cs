using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterType { FIRE, WATER, ELECTRIC}

[System.Serializable]
public class Monster
{
    public string mName;
    public int maxHp = 50;
    private int _hp;
    public int currentHp
    {
        get { return _hp; }
        set { _hp = Mathf.Clamp(value, 0, maxHp);  }
    }

    public MonsterType mType;


    public int attack;
    public int defense;

    private void Start()
    {
        _hp = maxHp;
    }

    UnityEvent OnHpChange = new UnityEvent();

    public Monster(MonsterSCR mData)
    {
        mName = mData._mName;
        maxHp = mData._maxHp;
        currentHp = maxHp;
        mType = mData._mType;
        attack = mData._attack;
        defense = mData._defense;
    }

    public void Attack(Monster enemy)
    {
        int defaultDamage = Random.Range(attack/4, attack);
        int totalDamage = CalculateWeakness(defaultDamage, mType, enemy.mType);
        enemy.TakeDamage(totalDamage);
    }

    public void TakeDamage(int damage)
    {
        int damageToTake = damage - defense;
        if (damageToTake <= 0) damageToTake = 1;
        currentHp -= damageToTake;
        OnHpChange.Invoke();
    }

    int CheckNum(int nume)
    {
        if (nume > 2)
        {
            return 0;
        }
        else if (nume < 0)
        {
            return 2;
        }
        return nume;
    }

    public int CalculateWeakness(int damage, MonsterType damageType, MonsterType defenseType)
    {

        List<int> effectiveness = new List<int>() { 0, 0, 0 };
        int n = (int)damageType;
        effectiveness.Insert(1, n);
        effectiveness.Insert(0, CheckNum(n - 1));
        effectiveness.Insert(2, CheckNum(n + 1));

        if ((int)damageType < (int)defenseType)
        {
            return damage / 2;
        }
        else if((int)damageType > (int)defenseType)
        {
            return damage * 2;
        }
        else
        {
            return damage;
        }
    }
}
