using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;

    public TextMeshProUGUI HealthText;
    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        HealthText.text = health.ToString();

        if (health <= 0)
        {

            if (isLocalPlayer) 
            {
                RoomManager.instance.RespawnPlayer();
            }
            Destroy(gameObject);
            
        }
    }
}
