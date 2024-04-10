using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;

    public GameObject Damagetaken;

    public GameObject deathParticlePrefab;

    public AnimationClip Damage;

    public AudioClip[] voiceLines;




    public TextMeshProUGUI HealthText;
    public GameObject deathvfx;
    [PunRPC]
    public void TakeDamage(int _damage)
    {
        if (voiceLines.Length > 0 && isLocalPlayer)
        {
            int randomIndex = Random.Range(0, voiceLines.Length);
            GetComponent<AudioSource>().PlayOneShot(voiceLines[randomIndex]);
        }

        health -= _damage;
        Damagetaken.GetComponent<Animation>().Play(Damage.name);


        HealthText.text = health.ToString();

        if (health <= 0)
        {


            if (isLocalPlayer)
            {
                SpawnDeathParticle();
                RoomManager.instance.RespawnPlayer();

                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }


            Destroy(gameObject);

        }

        [PunRPC]
        void SpawnDeathParticle()
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
