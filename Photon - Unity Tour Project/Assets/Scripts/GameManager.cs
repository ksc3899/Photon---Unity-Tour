using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoints;
    public CinemachineFreeLook thirdPersonCam;

    private void Awake()
    {
        GameObject g = PhotonNetwork.Instantiate("Player", spawnPoints[Random.Range(0, 2)].position, Quaternion.identity, 0) as GameObject;
        thirdPersonCam.Follow = g.transform.GetChild(2).transform;
        thirdPersonCam.LookAt = g.transform.GetChild(2).transform;
    }
}
