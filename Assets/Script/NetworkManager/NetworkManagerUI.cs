using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using UnityEditor;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField]GameObject text; 
    private void Start()
    {
        ShowConnection();
        ManagerGame.onGameStateChanged += GameStateChangedCallback;
       
    }
    private void OnDestroy()
    {
        ManagerGame.onGameStateChanged -= GameStateChangedCallback; 
    }
    private void GameStateChangedCallback(ManagerGame.State gameState)
    {
        switch(gameState)
        {
            case ManagerGame.State.Game:
                HidePanel();
                
                break; 
        }
    }
    
    public void HostButtonCallBack()
    {
        NetworkManager.Singleton.StartHost();
        text.SetActive(true);
    }
    public void ClientButtonCallBack()
    {
        
        
        string ipAddress = IPAddress.instance.GetInputIP();
        
        UnityTransport utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.SetConnectionData(ipAddress, 7777);
        
        NetworkManager.Singleton.StartClient();
        
        
    }
    void ShowConnection()
    {
        var Menu = GameObject.Find("MenuIP");
        Menu.SetActive(true);
    }
    void HidePanel()
    {
        var Menu = GameObject.Find("MenuIP");
        Menu.SetActive(false);
    }
}
