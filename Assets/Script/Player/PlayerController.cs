using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Mono.Cecil.Cil;
using Unity.Collections;
using Unity.Netcode.Components;
using UnityEngine.UI;
public class PlayerController : NetworkBehaviour
{

    Text Score;
    Rigidbody2D r2d;
    float moveH;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    
    private Animator anim; 
    bool _facingRight = true;
    public float point = 0;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask layerMask;
    private bool _isGrounded;

    [Header(" Elements ")]
    [SerializeField] private SpriteRenderer[] renderers;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsServer && IsOwner)
            ColorizeServerRpc(Color.red);

    }

    [ServerRpc]
    private void ColorizeServerRpc(Color color)
    {
        ColorizeClientRpc(color);
    }

    [ClientRpc]
    private void ColorizeClientRpc(Color color)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
    }

    void Start()
    {
        if (!IsOwner) return;
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Score = GameObject.Find("Score").GetComponent<Text>();
    }


    
    void Update()
    {
        if (!IsOwner) return;
        Score.text = point.ToString();
        
        moveH = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(moveH*velocity, r2d.velocity.y);
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            
            r2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
        }
        Debug.Log(point);
        if (moveH > 0 && !_facingRight )
        {
            flip();
        }else if (moveH < 0 && _facingRight) {
            flip();
        }
        if (Input.GetKey(KeyCode.K) && _isGrounded)
        {
            anim.SetTrigger("Punch");
        }
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask);
    }
    void finish()
    {
        if (!IsOwner) return;
        
    }

    private void LateUpdate()
    {
        if (!IsOwner) return;
        if (moveH != 0) {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        anim.SetFloat("verticalVelocity", r2d.velocity.y);
        anim.SetBool("isGrounded", _isGrounded);
    }
    
    void flip() {
        if (!IsOwner) return;
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag("Player") && anim.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {

            if (playerController.NetworkObjectId != this.NetworkObjectId)
                point += 50;
        }
    }
}
