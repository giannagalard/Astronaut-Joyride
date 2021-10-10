using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    GameInfo GI;

    private void Start()
    {
        GI = GameObject.Find("Managers").GetComponent<GameInfo>();
        StartCoroutine("Die");
    }

    void Update()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * transform.GetComponent<Projectile>().speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Astronaut")
        {
            collision.GetComponent<PlayerController>().TakeDamage(transform.GetComponent<Projectile>().dmg);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }

}
