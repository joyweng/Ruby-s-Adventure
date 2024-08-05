using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10;//可以增加的子彈補給
    public ParticleSystem collectEffect;//拾取特效
    public AudioClip collectClip;//拾取音效

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null) 
        { 
            if (pc.MyCurrentBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);//添加子彈補給
                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效
                AudioManager.instance.AudioPlay(collectClip);//播放拾取音效
                Destroy(this.gameObject);//添加完後銷毀自己
            }
        }
    }
}
