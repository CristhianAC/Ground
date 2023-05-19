using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;  

public class ManagerGame : NetworkBehaviour
{
    public static ManagerGame instance;
    // Start is called before the first frame update
    public enum State { Menu, Game, Win, Lose}
    private State gameState;
    private int connectedPlayers;
    public static Action<State> onGameStateChanged;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        NetworkManager.OnServerStarted += NetworkManager_OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        NetworkManager.OnServerStarted-= NetworkManager_OnServerStarted;
    }
    private void NetworkManager_OnServerStarted()
    {
        if (!IsServer)
            return;
        connectedPlayers++;
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        
    }
    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        connectedPlayers++;
        if (connectedPlayers >= 2)
            StartGame();

            
    }
    void StartGame()
    {
        StartGameClientRpc();
    }
    [ClientRpc]
    private void StartGameClientRpc()
    {
        gameState = State.Game;
        onGameStateChanged?.Invoke(gameState);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
