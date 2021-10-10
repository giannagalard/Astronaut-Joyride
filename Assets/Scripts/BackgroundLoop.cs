// watches location of everything in scene - all background elements (planets, stars) moves and repositions them

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //References
    private GameManager GM;

    //GameObjects
    public GameObject[] levels;
    private GameObject player;

    public Camera cam;
    public float camSpeed = 0;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Managers").GetComponent<GameManager>();
        player = GM.player;
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z)); // Calculate bounds of camera view
    }

    private void Update()
    {
    // Once intro sequence complete - isStarted = true
    if (GM.isStarted)
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, cam.transform.position.y, cam.transform.position.z); // Store player x pos in new vector3
        cam.transform.position = playerPos; // Cam follows playerPos

        // For every object in the list calculate the distance to loop bg elements seamlessly and endlessly
        foreach (GameObject obj in levels)
        {
            float dist = cam.transform.position.x - obj.GetComponent<SpriteRenderer>().bounds.center.x; // Distance between center of element and cam

            float leftBound = cam.transform.position.x - screenBounds.x; // Left boundary coord

            float leftXCoord = leftBound - obj.GetComponent<SpriteRenderer>().bounds.size.x / 2; // expected left x coord of element just off screen

            float maxDist = cam.transform.position.x - leftXCoord; // Distance between cam.x and the leftXCoord

            float leftLimit = cam.transform.position.x - maxDist; // Point at which element going left should loop
            float rightLimit = cam.transform.position.x + maxDist; // Point at which element going right should loop

            if (obj.GetComponent<SpriteRenderer>().bounds.center.x < leftLimit)
            {
                obj.transform.position = new Vector3(rightLimit, obj.transform.position.y, obj.transform.position.z);
            }
            else if (obj.GetComponent<SpriteRenderer>().bounds.center.x > rightLimit)
            {
                obj.transform.position = new Vector3(leftLimit, obj.transform.position.y, obj.transform.position.z);
            }
            //print($"Distance {dist}");
            //print($"Size {obj.GetComponent<SpriteRenderer>().bounds.size}");
            //print($"Center X {obj.GetComponent<SpriteRenderer>().bounds.center.x}");
        }
    }
    }

}