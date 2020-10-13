using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField playerName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        playerName.interactable = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Sucessfully established connection!");
    }
}
