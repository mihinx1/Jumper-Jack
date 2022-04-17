using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{

    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool triggered; //zamka se ukljuci
    private bool active; //zamka aktivna i ostecuje igraca

    private Health playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (playerHealth!=null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
            {
                //zamka se ukljuci
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //zamka postaje crvena da znamo da smo ju upalili
        triggered = true;
        sprite.color = Color.red;

        //ceka se delay, aktivira se zamka, upali se animacija, boja se vraca u normalno stanje
        yield return new WaitForSeconds(activationDelay);
        sprite.color = Color.white;
        active = true;
        anim.SetBool("activated",true);

        //pricekamo sekunde, deaktiviramo zamku i resetiramo animacije i sve varijable
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }

}
