using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideways : EnemyDamage
{

    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Animator anim;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        anim = GetComponent<Animator>();

    }


    private void Update() //kamo se krece pila
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
                if (CompareTag("Enemy1"))
                {
                    transform.Rotate(0f, 180f, 0f);
                }
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
                if (CompareTag("Enemy1"))
                {
                    transform.Rotate(0f, 180f, 0f);
                }
            }

        }
    }

  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
