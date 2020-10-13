using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject startButton;
    public TMP_InputField playerName;

    public void ActivateStartButton()
    {
        PhotonNetwork.NickName = playerName.text;
        startButton.SetActive(true);
        playerName.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Loading...");
        startButton.GetComponent<Button>().interactable = false;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        string roomName = "Room: " + UnityEngine.Random.Range(1, 1000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = (byte)2,
            PlayerTtl = 5000,
            EmptyRoomTtl = 60000
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
}
