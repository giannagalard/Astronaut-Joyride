using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    GameInfo GI;

    private void Start()
    {
        GI = GameObject.Find("Managers").GetComponent<GameInfo>();
    }

    void Update()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * transform.GetComponent<Projectile>().speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Astronaut")
        {
            GI.score += 100;
            GI.potions += 1;
            Destroy(this.gameObject);
        }
    }
}
