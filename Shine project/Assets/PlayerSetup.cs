using Photon.Pun;
using TMPro;
using UnityEngine;


public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public GameObject cam;
    public string nickname;
    public TextMeshPro nicknametext;

    public Transform TPweaponHolder;

    public void IsLocalPlayer()
    {

        TPweaponHolder.gameObject.SetActive(false);
        movement.enabled = true;
        cam.SetActive(true);
    }

    [PunRPC]

    public void SetNickName(string _name)
    {
        nickname = _name;
        nicknametext.text = nickname;
    }

    [PunRPC]

    public void SetTpWeapon(int _weaponIndex)
    {
        foreach (Transform _weapon in TPweaponHolder)
        {
            _weapon.gameObject.SetActive(false);
        }

        TPweaponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }


}
