using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem collectEffect;//�B���S��
    public AudioClip collectClip;//�B������


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();


        if (controller != null)
        {
            if (controller.MyCurrentHealth < controller.MyMaxHealth)
            {
                controller.ChangeHealth(1);
                Instantiate(collectEffect, transform.position, Quaternion.identity);//�ͦ��S��
                AudioManager.instance.AudioPlay(collectClip);//����B������
                Destroy(this.gameObject);
            }
        }

    }
}
