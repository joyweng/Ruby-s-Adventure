using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }
    public AudioSource audioSource;
    private int fixedCount;//�Q�״_���ĤH��
    private int enemyCount;//�ĤH�`��
    public AudioClip completedClip;

    void Awake()
    {
        enemyCount = 0;//�ĤH�`�ƥ��]�m0
        fixedCount = 0;//��l�q�{=0
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enemyCount != 0 && enemyCount == fixedCount)
        {
            Debug.Log("==============���ȧ���=================");
            AudioPlay(completedClip);
            fixedCount = 0;//�קK���Ƽ��񭵼ֱN�״_�ĤH�Ƴ]��0
            UImanager.instance.MissionCompleted();//�I�s���ȧ�����UI
        }
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void UpdateFixedCount(int amount){ fixedCount += amount; }

    public void UpdateEnemyCount(int amount) { enemyCount += amount; }

}
