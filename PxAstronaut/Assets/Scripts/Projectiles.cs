// meteors and shiz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject[] projectiles;

    void Start()
    {
        InvokeRepeating("Spawn", 5, 5);
    }

    void Spawn()
    {
        var projectile = Instantiate(projectiles[0], transform.position, Quaternion.identity);
        projectile.transform.position = new Vector3(transform.position.x, Random.Range(-1.1f, 1.1f), 4);
    }

}
