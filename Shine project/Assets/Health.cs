using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;


    public TextMeshProUGUI HealthText;
    public GameObject deathvfx;
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

                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }
            Destroy(gameObject);

        }
    }
}
