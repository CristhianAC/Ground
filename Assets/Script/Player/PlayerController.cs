using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D r2d;
    float moveH;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    bool canJump = true;    
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(moveH*velocity, r2d.velocity.y);
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            Debug.Log("entro");
            r2d.AddForce(Vector2.up * jumpForce);
            canJump = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {
            canJump = true;
        }
    }
    
}
