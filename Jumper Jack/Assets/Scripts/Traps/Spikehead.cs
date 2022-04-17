using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikhead postavke")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private float checkTimer;
    private Vector3 destination;

    private bool attacking;

    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        //samo ako spikhead napada samo onda ide na destination
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);

        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer>checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        //provjera da li spikhead vidi igraca u sva 4 smjera
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider!=null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }


    private void CalculateDirections()
    {
        
        directions[0] = transform.right * range; //u desno
        directions[1] = -transform.right * range; //u lijevo
       directions[2] = transform.up * range; //prema gore
       directions[3] = -transform.up * range; //prema dolje

    }

    private void Stop()
    {
        {
            destination = transform.position;
            attacking = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();//zaustavi spikheada kada se dotakne neceg
    }
    
}
