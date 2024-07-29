using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //��ҼҦ�
    public static UImanager instance {  get; private set; }

    void Start()
    {
        instance = this;
    }

    public Image healthBar; //������

    public void UpdateHealthBar(int curAmount, int maxAmount) //�ثe����A�̤j���
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

}
