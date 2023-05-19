using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerElection : NetworkBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SpriteRenderer[] renderers;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsServer && IsOwner)
            ColorizeServerRpc(Color.red);

    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
   

}
