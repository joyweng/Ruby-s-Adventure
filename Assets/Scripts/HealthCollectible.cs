using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem collectEffect;//拾取特效
    public AudioClip collectClip;//拾取音效


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();


        if (controller != null)
        {
            if (controller.MyCurrentHealth < controller.MyMaxHealth)
            {
                controller.ChangeHealth(1);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效
                AudioManager.instance.AudioPlay(collectClip);//播放拾取音效
                Destroy(this.gameObject);
            }
        }

    }
}
