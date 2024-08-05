using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //虫ㄒ家Α
    public static UImanager instance {  get; private set; }

    void Awake()
    {
        instance = this;
    }

    public Image healthBar; //à︹﹀兵
    public Text bulletCountText;//紆计秖陪ボ

    public void UpdateHealthBar(int curAmount, int maxAmount) //ヘ玡﹀兵程﹀兵
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateBulletCount(int curAmount, int maxAmount) //ヘ玡紆计程紆计
    {
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
    }

}
