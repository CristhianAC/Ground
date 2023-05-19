using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
    public void HostButtonCallBack()
    {
        NetworkManager.Singleton.StartHost();

    }
    public void ClientButtonCallBack()
    {
        string ipAddress = IPAddress.instance.GetInputIP();

        UnityTransport utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.SetConnectionData(ipAddress, 7777);

        NetworkManager.Singleton.StartClient();
    }
}
