// watches location of everything in scene - all background elements (planets, stars) moves and repositions them

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
     public GameObject[] levels;
     public Camera cam;
     public float camSpeed = 0;
     public GameObject player;
     private Vector2 screenBounds;

     public bool isStarted = false;

     // Start is called before the first frame update
     void Start()
     {
          cam = Camera.main;
          screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
     }

     private void Update()
     {
          if (isStarted)
          {

               Vector3 playerPos = new Vector3(player.transform.position.x, cam.transform.position.y, cam.transform.position.z);
               cam.transform.position = playerPos;
               //cam.transform.Translate(Vector3.right * Time.deltaTime * camSpeed);
               foreach (GameObject obj in levels)
               {
                    float dist = cam.transform.position.x - obj.GetComponent<SpriteRenderer>().bounds.center.x;

                    float leftBound = cam.transform.position.x - screenBounds.x;

                    float leftXCoord = leftBound - obj.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                    float maxDist = cam.transform.position.x - leftXCoord;

                    float leftLimit = cam.transform.position.x - maxDist;
                    float rightLimit = cam.transform.position.x + maxDist;

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