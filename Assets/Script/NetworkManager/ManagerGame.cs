using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerGame : NetworkBehaviour
{
    public static ManagerGame instance;
    // Start is called before the first frame update
    public enum State { Menu, Game, Win, Lose}
    private State gameState;
    private int connectedPlayers;
    public static Action<State> onGameStateChanged;
    GameObject leaderBoard;
    public float time = 60f;
    Text timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        leaderBoard = GameObject.Find("LeaderBoard");
        leaderBoard.SetActive(false);
        timer = GameObject.Find("Timer").GetComponent<Text>();
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
        timer = GameObject.Find("Timer").GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update()
    {   
        if(connectedPlayers == 3 && IsHost)
        {
            time -= Time.deltaTime;
        }
        else if (IsClient && !IsHost)
        {
            time -= Time.deltaTime;
           
        }
        
        timer.text = time.ToString("0");

        if(time <= 0)
        {
            leaderBoard.SetActive(true);
            Time.timeScale = 0f;
        }

            

    }
}
