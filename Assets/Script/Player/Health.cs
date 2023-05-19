using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Health : NetworkBehaviour
{
    [SyncVar] float health = 0;
    [SyncVar] float maxHealth = 3f;

    private void Start()
    {
        health = maxHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<NetworkObject>(out NetworkObject networkObject) && networkObject.IsOwner)
        {
            TakeDamageServerRpc(1f);
        }
    }

    [ServerRpc]
    public void TakeDamageServerRpc(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
