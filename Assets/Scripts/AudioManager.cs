using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }
    public AudioSource audioSource;
    private int fixedCount;//被修復的敵人數
    private int enemyCount;//敵人總數
    public AudioClip completedClip;

    void Awake()
    {
        enemyCount = 0;//敵人總數先設置0
        fixedCount = 0;//初始默認=0
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enemyCount != 0 && enemyCount == fixedCount)
        {
            Debug.Log("==============任務完成=================");
            AudioPlay(completedClip);
            fixedCount = 0;//避免重複撥放音樂將修復敵人數設為0
            UImanager.instance.MissionCompleted();//呼叫任務完成的UI
        }
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void UpdateFixedCount(int amount){ fixedCount += amount; }

    public void UpdateEnemyCount(int amount) { enemyCount += amount; }

}
