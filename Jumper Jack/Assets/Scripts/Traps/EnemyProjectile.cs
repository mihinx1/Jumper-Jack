using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //Ozljedit ce igraca svaki put kad se dotakne
{
    //U unityu imamo damage polje u koje unosimo damage jer EnemyDamage skripta ima to polje

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;
        if (lifetime>resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;


        base.OnTriggerEnter2D(collision);
        //zanemari collider od pollygon collidera, Vidljivo(game object)
        Physics2D.IgnoreLayerCollision(11, 2);
        boxCollider.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("explode");
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

   

}
