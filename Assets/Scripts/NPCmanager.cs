using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCmanager : MonoBehaviour
{
    public GameObject tipImage;//提示對話框
    public GameObject dialogImage;//NPC對話框
    public float showTime = 4;//對話框顯示時長
    private float showTimer = 0;//對話框顯示計時器

    // Start is called before the first frame update
    void Start()
    {
        tipImage.SetActive(true);//初始默認顯示提示對話框
        dialogImage.SetActive(false);//初始默認隱藏對話框
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

    //顯示對話框
    public void ShowDialog()
    {
        showTimer = showTime;//計時器重置
        tipImage.SetActive (false);
        dialogImage.SetActive(true);
    }
}
