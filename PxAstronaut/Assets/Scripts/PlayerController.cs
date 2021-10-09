using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float manualMoveSpeed = 1;
    public bool isGrounded = true;
    private Vector2 screenBounds;
    private float objWidth, objHeight;
    Rigidbody2D rb;

    public float curFuel;
    public float maxFuel = 10f;
    public float fuelConsumptionRate = 1.5f;
    public float fuelRechargeRate = 1.2f;

    public int health = 3;
    public GameObject[] hearts;

    void Start()
    {
        moveSpeed = 0f;
        manualMoveSpeed = 0f;
        curFuel = maxFuel;
        rb = transform.GetComponent<Rigidbody2D>();

        StartCoroutine("Intro");

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);

        if (isGrounded)
        {
            transform.GetComponent<Animator>().SetTrigger("Idle");
        }

        // JETPACK
        if (Input.GetKey(KeyCode.UpArrow) && curFuel > 0)
        {
            curFuel -= Time.deltaTime * fuelConsumptionRate;
            rb.AddForce(Vector2.up * 0.05f, ForceMode2D.Impulse);
        }

        // REGULAR JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            transform.GetComponent<Animator>().SetTrigger("Jump");
        }

        if(isGrounded && curFuel < maxFuel)
        {
            curFuel += Time.deltaTime * fuelRechargeRate;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-Vector2.up * Time.deltaTime * manualMoveSpeed);
        }
    }

    public void TakeDamage()
    {
        hearts[health-1].GetComponent<Animator>().SetTrigger("DMG");
        health -= 1;

        if(health == 0)
        {
            print("Return to base with less rewards");
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(2);
        manualMoveSpeed = 1.0f;
        moveSpeed = 0.5f;
        Camera.main.GetComponent<BackgroundLoop>().isStarted = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
