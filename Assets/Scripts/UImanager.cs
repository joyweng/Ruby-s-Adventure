using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject completeImage;//��ܥ��ȧ�����UI
    public GameObject quitPanel;//���}�C����UI
    public float showTime = 3;//UI����ܮɪ�
    private float showTimer = 0;//UI����ܭp�ɾ�
    //��ҼҦ�
    public static UImanager instance {  get; private set; }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        completeImage.SetActive(false);//��l�q�{����UI��
        quitPanel.SetActive(false);//��l�������}UI
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            completeImage.SetActive(false);
        }
        // �ˬd�O�_���U ESC ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }

    public void MissionCompleted()
    {
        showTimer = showTime;//�p�ɾ����m
        completeImage.SetActive(true);//���UI��
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

    public void ConfirmQuit()//�T�{���}�C���A�Y�bunity�s�边���h����B��C���A�Y�b���]�᪺�C�����h�����C���{��
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    public void CancelQuit()//�������}�C��
    {
        quitPanel.SetActive(false);
    }

}
