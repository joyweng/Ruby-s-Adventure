using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed = 5.0f;
    private float horizontal;   //for水平移動
    private float vertical;     //for垂直移動
    private int maxHealth = 5;  //最大生命值
    private int currentHealth;  //當前生命值
    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }
    private float invincibleTime = 2f;//無敵時間
    private float invincibleTimer;//無敵時間計時器
    private bool isInvincible;//是否處於無敵狀態
    private Animator animator;
    //玩家的朝向 默認右方
    private Vector2 lookDirection = new Vector2(1, 0);
    public GameObject bulletPrefab; //子彈
    public AudioClip hurtClip;//受傷音效
    public AudioClip launchClip;//發射子彈音效
    public AudioClip footstepClip; // 腳步聲音效
    private AudioSource audioSource; // 音效源

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // 獲取音效源
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);//更新血條
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

        // 播放腳步聲
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

        //無敵時間判斷
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0){ isInvincible = false; } //倒計時結束，取消無敵狀態
        }
       
        //按下J鍵發射子彈
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Launch");
            AudioManager.instance.AudioPlay(launchClip);//播放發射音效
            GameObject bullet = Instantiate(bulletPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
        }

        //按下E鍵與NPC交互
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider !=  null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
                if (npc != null) { npc.ShowDialog(); }//顯示對話框
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

    //用於改變玩家的生命值
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
            AudioManager.instance.AudioPlay(hurtClip);//播放受傷音效
        }

        //把玩家的生命值約束在0與最大值之間
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);//更新血條
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }
}
