using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject completeImage;//顯示任務完成的UI
    public GameObject quitPanel;//離開遊戲的UI
    public float showTime = 3;//UI框顯示時長
    private float showTimer = 0;//UI框顯示計時器
    //單例模式
    public static UImanager instance {  get; private set; }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        completeImage.SetActive(false);//初始默認隱藏UI框
        quitPanel.SetActive(false);//初始隱藏離開UI
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
        // 檢查是否按下 ESC 鍵
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }

    public void MissionCompleted()
    {
        showTimer = showTime;//計時器重置
        completeImage.SetActive(true);//顯示UI框
    }

    public Image healthBar; //角色血條
    public Text bulletCountText;//子彈數量顯示

    public void UpdateHealthBar(int curAmount, int maxAmount) //目前血條，最大血條
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateBulletCount(int curAmount, int maxAmount) //目前子彈數，最大子彈數
    {
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
    }

    public void ConfirmQuit()//確認離開遊戲，若在unity編輯器中則停止運行遊戲，若在打包後的遊戲中則關閉遊戲程式
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    public void CancelQuit()//取消離開遊戲
    {
        quitPanel.SetActive(false);
    }

}
