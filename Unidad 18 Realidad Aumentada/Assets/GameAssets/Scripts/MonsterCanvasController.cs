using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCanvasController : MonoBehaviour
{
    public Text monsterNameTxt;
    public Image iconTypeImg;
    public Image healthBarImg;

    public void SetInitialValues(string mName, Sprite iconType, int _current, int _max)
    {
        monsterNameTxt.text = mName;
        iconTypeImg.sprite = iconType;
        UpdateMonsterHp(_current, _max);
    }

    public void UpdateMonsterHp(int hp, int maxHp)
    {
        healthBarImg.fillAmount = (float)hp / (float)maxHp;
    }
}
