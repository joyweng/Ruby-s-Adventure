using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed;//移動速度
    public bool vertical;
    public float changeTime = 4.0f;

    // Private variables
    private Rigidbody2D rigidbody2d;
    private float changeTimer;
    private int direction = 1;//方向 1 or -1
    private Animator animator;
    private bool isFixed;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        speed = 18;
        changeTimer = changeTime;
        animator = GetComponent<Animator>();
        isFixed = false;
    }


    // Update is called every frame
    void Update()
    {
        if (isFixed) { return; }    //若被修復了則不執行以下程式

        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            direction = -direction;
            changeTimer = changeTime;
        }

        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("moveX", direction);
            animator.SetFloat("moveY", 0);
        }

        rigidbody2d.MovePosition(position);
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        isFixed = true;
        rigidbody2d.simulated = false;  //禁用物理
        animator.SetTrigger("fix");
    }
}
