using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    public MonsterSCR monsterData;
    public Transform posMonster;
    Monster monster;

    private void Awake()
    {
        BuildMonsterCard();
    }

    void BuildMonsterCard()
    {
        monster = new Monster(monsterData);
        Instantiate(monsterData._model, posMonster);
        GetComponentInChildren<MonsterCanvasController>().SetInitialValues(monsterData.name, monsterData._typeIcon);
    }
}
