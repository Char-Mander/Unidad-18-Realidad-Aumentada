using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster", order = 1)]
public class MonsterSCR : ScriptableObject
{
    public string _mName;
    public int _maxHp;
    public MonsterType _mType;
    public Sprite _typeIcon;
    public GameObject _model;
}
