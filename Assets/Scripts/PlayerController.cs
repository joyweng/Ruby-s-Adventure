using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed = 5.0f;
    private float horizontal;   //for��������
    private float vertical;     //for��������
    private int maxHealth = 5;  //�̤j�ͩR��
    private int currentHealth;  //��e�ͩR��
    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }
    private float invincibleTime = 2f;//�L�Įɶ�
    private float invincibleTimer;//�L�Įɶ��p�ɾ�
    private bool isInvincible;//�O�_�B��L�Ī��A

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //�L�Įɶ��P�_
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;//�˭p�ɵ����A�����L�Ī��A
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    //�Ω���ܪ��a���ͩR��
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible == true)
            {
                return;
            }

            isInvincible = true;
            invincibleTimer = invincibleTime;
        }

        //�⪱�a���ͩR�Ȭ����b0�P�̤j�Ȥ���
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }
}
