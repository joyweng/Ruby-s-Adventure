using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;
    public AudioClip hitClip;//�������魵��
    
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.instance.AudioPlay(hitClip);//������������
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if (ec != null) 
        {
            ec.Fix();//�״_�ĤH
        }
        Destroy(this.gameObject);
    }

}
