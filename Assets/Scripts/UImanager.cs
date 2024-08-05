using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //��ҼҦ�
    public static UImanager instance {  get; private set; }

    void Awake()
    {
        instance = this;
    }

    public Image healthBar; //������
    public Text bulletCountText;//�l�u�ƶq���

    public void UpdateHealthBar(int curAmount, int maxAmount) //�ثe����A�̤j���
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateBulletCount(int curAmount, int maxAmount) //�ثe�l�u�ơA�̤j�l�u��
    {
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
    }

}
