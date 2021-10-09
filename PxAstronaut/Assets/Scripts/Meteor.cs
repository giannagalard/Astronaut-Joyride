using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 10f);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Astronaut")
        {
            collision.GetComponent<PlayerController>().TakeDamage();
            Destroy(this.gameObject);
        }
    }

}
