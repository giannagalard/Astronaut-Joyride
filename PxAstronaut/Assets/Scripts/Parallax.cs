// not using

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    Camera cam;
    public float parallaxFactor;

    private void Start()
    {
        cam = Camera.main;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    private void Update()
    {
        //float temp = (cam.transform.position.x * (1 - parallaxFactor));
        float dist = (cam.transform.position.x * parallaxFactor);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }

    /*
    public Camera cam;
    public Transform subject;

    public Vector2 startPos;
    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPos;

    float distFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distFromSubject) / clippingPlane;

    public void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        Vector2 newPos = transform.position = startPos + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
    */
}
