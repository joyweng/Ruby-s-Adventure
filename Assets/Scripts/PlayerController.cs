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
    private int maxBulletCount = 99;//�̤j�l�u��
    private int currentBulletCount;//��e�l�u��
    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }
    public int MyMaxBulletCount { get { return maxBulletCount; } }
    public int MyCurrentBulletCount { get { return currentBulletCount; } }
    private float invincibleTime = 2f;//�L�Įɶ�
    private float invincibleTimer;//�L�Įɶ��p�ɾ�
    private bool isInvincible;//�O�_�B��L�Ī��A
    private Animator animator;
    //���a���¦V �q�{�k��
    private Vector2 lookDirection = new Vector2(1, 0);
    public GameObject bulletPrefab; //�l�u
    public AudioClip hurtClip;//���˭���
    public AudioClip launchClip;//�o�g�l�u����
    public AudioClip footstepClip; // �}�B�n����
    private AudioSource audioSource; // ���ķ�

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 2;//��l��2���q
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);//��s���
        currentBulletCount = 0;//��l�S���l�u
        UImanager.instance.UpdateBulletCount(currentBulletCount, maxBulletCount);//��s�l�uUI
        invincibleTimer = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // ������ķ�
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //
        Vector2 moveVector = new Vector2(horizontal, vertical);
        if (horizontal != 0 || vertical != 0)
        {
            lookDirection = moveVector;
        }
        if (lookDirection.x < 0) { lookDirection.x = -1; }
        if (lookDirection.x > 0) { lookDirection.x = 1; }
        if (lookDirection.y < 0) { lookDirection.y = -1; }
        if (lookDirection.y > 0) { lookDirection.y = 1; }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveVector.magnitude);

        Vector2 position = rigidbody2d.position;
        position += moveVector * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        // ����}�B�n
        if (moveVector.magnitude > 0)
        {
            if (!audioSource.isPlaying || audioSource.clip != footstepClip)
            {
                audioSource.clip = footstepClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == footstepClip){ audioSource.Stop();}
        }

        //�L�Įɶ��P�_
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0){ isInvincible = false; } //�˭p�ɵ����A�����L�Ī��A
        }
       
        //���UJ��o�g�l�u�A��e�l�u�ƭn�j��0
        if (Input.GetKeyDown(KeyCode.J) && currentBulletCount > 0)
        {
            ChangeBulletCount(-1);//�C��������֤@���l�u
            animator.SetTrigger("Launch");
            AudioManager.instance.AudioPlay(launchClip);//����o�g����
            GameObject bullet = Instantiate(bulletPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
        }

        //���UE��PNPC�椬
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider !=  null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
                if (npc != null) { npc.ShowDialog(); }//��ܹ�ܮ�
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
            animator.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(hurtClip);//������˭���
        }

        //�⪱�a���ͩR�Ȭ����b0�P�̤j�Ȥ���
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);//��s���
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }

    public void ChangeBulletCount(int amount)
    {
        currentBulletCount = Mathf.Clamp(currentBulletCount += amount, 0, maxBulletCount);//�N��e�l�u�Ƭ����b0~�̤j�Ȥ���
        UImanager.instance.UpdateBulletCount(currentBulletCount, maxBulletCount);//��s�l�u��UI
    }

}
