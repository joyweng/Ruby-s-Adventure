using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCmanager : MonoBehaviour
{
    public GameObject tipImage;//���ܹ�ܮ�
    public GameObject dialogImage;//NPC��ܮ�
    public float showTime = 4;//��ܮ���ܮɪ�
    private float showTimer = 0;//��ܮ���ܭp�ɾ�

    // Start is called before the first frame update
    void Start()
    {
        tipImage.SetActive(true);//��l�q�{��ܴ��ܹ�ܮ�
        dialogImage.SetActive(false);//��l�q�{���ù�ܮ�
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0) 
        { 
            tipImage.SetActive (true);
            dialogImage.SetActive(false); 
        }
    }

    //��ܹ�ܮ�
    public void ShowDialog()
    {
        showTimer = showTime;//�p�ɾ����m
        tipImage.SetActive (false);
        dialogImage.SetActive(true);
    }
}
