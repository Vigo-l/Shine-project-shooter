using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject Player;
    [Space]
    public Transform spawnPoint;
    

    [Space]

    public GameObject roomcam;
    public GameObject Crosshair;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("You are connected to a lobby now!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("you are connected to a room");

        roomcam.SetActive(false);
        Crosshair.SetActive(true);

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        
        GameObject _player = PhotonNetwork.Instantiate(Player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
    }
}
