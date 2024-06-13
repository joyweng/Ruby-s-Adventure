using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //無敵時間判斷
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;//倒計時結束，取消無敵狀態
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
        }

        //把玩家的生命值約束在0與最大值之間
        currentHealth = Mathf.Clamp(currentHealth += amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }
}
