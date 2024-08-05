using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10;//�i�H�W�[���l�u�ɵ�
    public ParticleSystem collectEffect;//�B���S��
    public AudioClip collectClip;//�B������

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null) 
        { 
            if (pc.MyCurrentBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);//�K�[�l�u�ɵ�
                Instantiate(collectEffect, transform.position, Quaternion.identity);//�ͦ��S��
                AudioManager.instance.AudioPlay(collectClip);//����B������
                Destroy(this.gameObject);//�K�[����P���ۤv
            }
        }
    }
}
