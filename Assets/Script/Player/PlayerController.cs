using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    
    Rigidbody2D r2d;
    float moveH;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    bool canJump = true;
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Debug.Log(OwnerClientId +"; randomNumber: " + randomNumber.Value);
        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0,100);
        }
        if (!IsOwner) return;
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
