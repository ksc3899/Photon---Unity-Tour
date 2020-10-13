using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System;

public class WaitManager : MonoBehaviourPunCallbacks
{
    public int maxTime;
    public TextMeshProUGUI timer;

    private int curTime;

    private void Start()
    {
        curTime = maxTime;
        timer.text = (curTime / 60).ToString("00") + ":" + (curTime % 60).ToString("00");

        StartCoroutine(TimerUpdate());

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            StopAllCoroutines();
            StartCoroutine(PlayersMatched());
        }
    }

    private IEnumerator TimerUpdate()
    {
        while (curTime > 0)
        {
            yield return new WaitForSeconds(1);

            curTime--;
            timer.text = (curTime / 60).ToString("00") + ":" + (curTime % 60).ToString("00");
        }

        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Main");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        StopAllCoroutines();
        StartCoroutine(PlayersMatched());
    }

    private IEnumerator PlayersMatched()
    {
        timer.text = PhotonNetwork.LocalPlayer.NickName + " vs. " + PhotonNetwork.PlayerListOthers[0].NickName;

        yield return new WaitForSeconds(7);

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Game");
    }
}
