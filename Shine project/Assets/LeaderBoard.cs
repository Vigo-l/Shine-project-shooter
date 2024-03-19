using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class LeaderBoard : MonoBehaviour
{

    public GameObject playersHolder;
    [Space]
    public float refreshRate = 1f;
    [Space]
    public GameObject[] slots;
    [Space]
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] NameTexts;
    void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    // Update is called once per frame
    public void Refresh()
    {
        foreach (var slot in slots)
        {
            slot.SetActive(false);

        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            NameTexts[i].text = player.NickName;
            scoreTexts[i].text = player.GetScore().ToString();

            i++;
        }


}
    private void Update()
    {
        playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
