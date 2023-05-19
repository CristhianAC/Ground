using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefabA; //agregar prefab en el inspector
    [SerializeField] private GameObject playerPrefabB; //agregar prefab en el inspector

    [ServerRpc(RequireOwnership = false)] //el servidor posee este objeto pero el cliente puede solicitar un spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer;
        if (prefabId == 0)
            newPlayer = (GameObject)Instantiate(playerPrefabA);
        else
            newPlayer = (GameObject)Instantiate(playerPrefabB);

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);

        
    }

    
    

   
}
