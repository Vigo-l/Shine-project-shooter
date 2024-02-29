using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public GameObject cam;
    public string nickname; 

    public void IsLocalPlayer()
    {
        movement.enabled = true;
        cam.SetActive(true);
    }

    [PunRPC]

    public void SetNickName(string _name)
    {
        nickname = _name;
    }
}
