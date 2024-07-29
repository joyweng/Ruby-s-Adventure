using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //單例模式
    public static UImanager instance {  get; private set; }

    void Start()
    {
        instance = this;
    }

    public Image healthBar; //角色血條

    public void UpdateHealthBar(int curAmount, int maxAmount) //目前血條，最大血條
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

}
