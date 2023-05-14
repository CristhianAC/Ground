using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Mono.Cecil.Cil;

public class PlayerController : NetworkBehaviour
{
    
    Rigidbody2D r2d;
    float moveH;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);
    private Animator anim; 
    bool _facingRight = true;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask layerMask;
    private bool _isGrounded;
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            
            r2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
        }
        
        if (moveH > 0 && !_facingRight )
        {
            flip();
        }else if (moveH < 0 && _facingRight) {
            flip();
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask);
    }
    private void LateUpdate()
    {
        if(moveH != 0) {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

    }
    
    void flip() {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
