using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRb;

    public int health = 3;

    public float movementSpeed = 7.5f;
    public float speedIncrease = 2.5f;
    public bool speedBoostEnabled = false;
    public float speedTimer = 0;
    public float speedCooldownTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            health = 3;
        }

        if (speedBoostEnabled)
        {
            if (speedTimer < speedCooldownTime)
                speedTimer += Time.deltaTime;

            else
            {
                speedBoostEnabled = false;
                speedTimer = 0;
            }
        }

        Vector2 tempVelocity = myRb.velocity;

        tempVelocity.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        tempVelocity.y = Input.GetAxisRaw("Vertical") * movementSpeed;

        myRb.velocity = tempVelocity;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.gameObject.tag == "health")
        {
            Destroy(collision.gameObject);
            health++;
        }else if (collision.gameObject.tag == "speed" && !speedBoostEnabled)
        {
            Destroy(collision.gameObject);
            movementSpeed += speedIncrease;
            speedBoostEnabled = true;
        }
        
    }
    
}
