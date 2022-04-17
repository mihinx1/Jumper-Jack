using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    public static int deathValue = 0;
    Text death;

    void Start()
    {
        death = GetComponent<Text>();
    }

    void Update()
    {
        death.text = "Deaths: " + deathValue;
    }
}
