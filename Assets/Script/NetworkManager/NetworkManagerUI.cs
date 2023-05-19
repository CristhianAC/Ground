using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button Host;
    [SerializeField] private Button Server;
    [SerializeField] private Button Client;

    private void Awake()
    {
        Server.onClick.AddListener(() =>
        { NetworkManager.Singleton.StartServer();
        });
        Host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();

        });
        Client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
