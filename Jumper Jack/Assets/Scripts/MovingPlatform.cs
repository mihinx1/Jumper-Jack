using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //brzina platforme
    public int startingPoint; //pocetna tocka
    public Transform[] points; //zavrsna tocka kretanja

    private int i;
    private void Start()
    {
        transform.position = points[startingPoint].position; 
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position)<0.02f)
        {
            i++;
            if (i==points.Length)
            {
                i = 0;
            }
        }
        //kretanje platforme do pozicije sa indexom i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
