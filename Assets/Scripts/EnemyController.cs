using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed = 3;//移動速度
    public bool vertical;
    public float changeTime = 3.0f;

    // Private variables
    Rigidbody2D rigidbody2d;
    float changeTimer;
    int direction = 1;//方向 1 or -1


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        speed = 3;
        changeTimer = changeTime;
    }


    // Update is called every frame
    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer < 0)
        {
            direction = -direction;
            changeTimer = changeTime;
        }
    }




    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
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
}
