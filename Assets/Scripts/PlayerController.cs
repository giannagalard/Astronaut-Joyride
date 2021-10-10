using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameManager GM;
    UIManager UI;

    [Header("Stats")]
    public float moveSpeed = 0;
    public float manualMoveSpeed = 0; // Set as 1 after intro
    public float curFuel;
    public float maxFuel = 10f;
    public float fuelConsumptionRate = 1.5f;
    public float fuelRechargeRate = 1.2f;
    public int health = 3;
    [Space(5)]

    [Header("Bools")]
    public bool isGrounded;
    public bool freeRoam = false;

    private Vector2 screenBounds;

    private float objWidth, objHeight, tabTimer;

    Rigidbody2D rb;
     
    void Start()
    {
        GM = GameObject.Find("Managers").GetComponent<GameManager>();
        UI = GameObject.Find("Managers").GetComponent<UIManager>();

        curFuel = maxFuel;
        rb = transform.GetComponent<Rigidbody2D>();

        StartCoroutine("Intro"); // Wait 3 seconds before starting everything

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // Store the screen dimensions
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // Player sprite width
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // Player sprite height
    }

    void Update()
    {
        if(GM.isStarted)
        {
            if(!freeRoam)
            {
                moveSpeed = Mathf.Sqrt(Time.timeSinceLevelLoad) / 4.5f + 0.5f;

                transform.Translate(Vector2.right * Time.deltaTime * moveSpeed); // Moving this transform(player) 1 unit to the right over time * moveSpeed
            }

            if (isGrounded)
            {
                transform.GetComponent<Animator>().SetTrigger("Idle"); // Accessing the animator component of this object and setting its Idle trigger
            }

            // JETPACK, if fuel > 0
            if (Input.GetKey(KeyCode.UpArrow) && curFuel > 0)
            {
                UI.jetpackFuel.transform.parent.gameObject.SetActive(true);
                curFuel -= Time.deltaTime * fuelConsumptionRate; // Subtracting the current fuel by rate * change in time
                UI.jetpackFuel.fillAmount = curFuel / maxFuel;
                rb.AddForce(Vector2.up * 0.05f + Vector2.right * 0.0005f, ForceMode2D.Impulse); // Adding force to the rigidbody (physics simulator) in the up direction
                transform.GetComponent<Animator>().SetTrigger("Flying");
                //rb.AddForce((Vector2.up + Vector2.right).normalized * 0.03f, ForceMode2D.Impulse);
            }

            // REGULAR JUMP
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse); // Adding force to the rigidbody (physics simulator) in the up direction
                transform.GetComponent<Animator>().SetTrigger("Jump"); // Accessing the animator component of this object and setting its Jump trigger
            }

            if (isGrounded && curFuel < maxFuel)
            {
                curFuel += Time.deltaTime * fuelRechargeRate; // Adding to the current fuel by rate * change in time
                UI.jetpackFuel.fillAmount = curFuel / maxFuel;
            }

            if (curFuel >= maxFuel)
            {
                curFuel = maxFuel;
            }

            // MANUAL MOVEMENT
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(-Vector2.up * Time.deltaTime * manualMoveSpeed); // Moving this transform(player) 1 unit to the down over time * manualMoveSpeed
            }
            if (Input.GetKey(KeyCode.LeftArrow) && freeRoam)
            {
                transform.Translate(Vector2.left * Time.deltaTime * manualMoveSpeed); // Moving this transform(player) 1 unit to the left over time * manualMoveSpeed
            }
            if (Input.GetKey(KeyCode.RightArrow) && freeRoam)
            {
                transform.Translate(Vector2.right * Time.deltaTime * manualMoveSpeed); // Moving this transform(player) 1 unit to the right over time * manualMoveSpeed
            }

            // Tab to end run
            if (Input.GetKey(KeyCode.Tab))
            {
                //End the run with all rewards intact
                if (tabTimer < 3.0)
                {
                    tabTimer += Time.deltaTime;
                    UI.EscapeButton.gameObject.SetActive(true);
                    UI.EscapeButton.GetComponent<Animator>().SetTrigger("Pressed");
                    UI.EscapeButton.GetComponentInChildren<Text>().text = Mathf.RoundToInt(3 - tabTimer).ToString();
                }
                else
                {
                    moveSpeed = 0;
                    freeRoam = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                tabTimer = 0;
                UI.EscapeButton.gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        UI.hearts[health - 1].GetComponent<Animator>().SetTrigger("DMG");
        health -= dmg;

        if (health == 0)
        {
            print("Return to base with less rewards");
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(3);
        manualMoveSpeed = 1.0f;
        GM.isStarted = true;
    }


    // While this object is colliding with any other
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            UI.jetpackFuel.transform.parent.gameObject.SetActive(false);
        }
    }

    // When this object stops colliding with any(?) other
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}