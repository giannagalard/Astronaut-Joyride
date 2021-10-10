using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private GameManager GM;

    private void Start()
    {
        GM = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.position = new Vector2(GM.player.transform.position.x-4, transform.position.y);
    }
}
