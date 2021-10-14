using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindZone : MonoBehaviour
{
    public Rigidbody2D golf;
    public Text wind;
    private int Wind = 0;

    private void Start()
    {
        Wind = Random.Range(-9,9);
        wind.text = "Wind : " +  Wind.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            golf.AddForce(new Vector2(Wind*5, 0),ForceMode2D.Force);
        }
    }
}
