using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [SerializeField] string[] tags;

    public float currentHealth { get; private set; } //get=mozemo pristupiti trenutnom zdravlju iz bilo koje druge skripte
    //private, set=trenutno zdravlje mozemo postaviti samo u ovoj skripti
    private Animator anim;
    private bool dead;


    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
       
        if (invulnerable)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //igrac ozljeda
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            //ako igrac umre
            if (gameObject.CompareTag("Player"))
            {

                
                if (!dead)
                {
                    //restarta level
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                    //Deathcounter +1
                    DeathCounter.deathValue += 1;

                    dead = true;
                }
            }

            //ako neprijatelj umre
            for (int i = 0; i < tags.Length; i++)
            {
               if(gameObject.CompareTag(tags[i])) { 
                
                    if (!dead)
                    {
                        anim.SetTrigger("die");

                        //deaktiviramo sve skripte za kretanje koje su u komponentama
                        foreach (Behaviour component in components)
                            component.enabled = false;

                        dead = true;
                    }

                }
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0,0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

        }
        //invurnebaility duration
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
