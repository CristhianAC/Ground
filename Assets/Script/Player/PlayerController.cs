using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Mono.Cecil.Cil;
using Unity.Collections;
using Unity.Netcode.Components;

public class PlayerController : NetworkBehaviour
{
    
    Rigidbody2D r2d;
    float moveH;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    
    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData { 
            _int = 56,
            _bool = true,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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

    public struct MyCustomData : INetworkSerializable{
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    
    void Update()
    {
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

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams) { 
        Debug.Log("TestServerRpc" + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);

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
