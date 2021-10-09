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

          StartCoroutine("Intro"); // Wait 3 seconds before starting everything

          screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // Store the screen dimensions
          objWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // Player sprite width
          objHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // Player sprite height
     }

     void Update()
     {
          transform.Translate(Vector2.right * Time.deltaTime * moveSpeed); // Moving this transform(player) 1 unit to the right over time * moveSpeed

          if (isGrounded)
          {
               transform.GetComponent<Animator>().SetTrigger("Idle"); // Accessing the animator component of this object and setting its Idle trigger
          }

          // JETPACK, if fuel > 0
          if (Input.GetKey(KeyCode.UpArrow) && curFuel > 0)
          {
               curFuel -= Time.deltaTime * fuelConsumptionRate; // Subtracting the current fuel by rate * change in time
               rb.AddForce(Vector2.up * 0.03f, ForceMode2D.Impulse); // Adding force to the rigidbody (physics simulator) in the up direction
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
          }
          if (Input.GetKey(KeyCode.DownArrow))
          {
               transform.Translate(-Vector2.up * Time.deltaTime * manualMoveSpeed); // Moving this transform(player) 1 unit to the down over time * manualMoveSpeed
          }
     }

     public void TakeDamage(int dmg)
     {
          hearts[health - 1].GetComponent<Animator>().SetTrigger("DMG");
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
          moveSpeed = 0.5f;
          Camera.main.GetComponent<BackgroundLoop>().isStarted = true;
     }

     // While this object is colliding with any other
     private void OnCollisionStay2D(Collision2D collision)
     {
          if (collision.collider.CompareTag("Ground"))
          {
               isGrounded = true;
          }
     }

     // When this object stops colliding with any(?) other
     private void OnCollisionExit2D(Collision2D collision)
     {
          isGrounded = false;
     }
}