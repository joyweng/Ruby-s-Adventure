using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    private Animator animator;
    //���a���¦V �q�{�k��
    private Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        //
        Vector2 moveDirection = new Vector2(horizontal, vertical);
        if (horizontal != 0 || vertical != 0)
        {
            lookDirection = moveDirection;
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);

        Vector2 position = rigidbody2d.position;
        position += moveDirection * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
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
            animator.SetTrigger("Hit");
        }

        //�⪱�a���ͩR�Ȭ����b0�P�̤j�Ȥ���
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }
}
