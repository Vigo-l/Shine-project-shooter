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
    public TextMeshProUGUI[] KillsTexts;
    public TextMeshProUGUI[] NameTexts;
    public TextMeshProUGUI[] DeathsTexts;
    private void Start()
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

        var sortedPlayerList = 
            (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            NameTexts[i].text = player.NickName;

            if (player.CustomProperties["kills"] != null)
            {
                KillsTexts[i].text = player.CustomProperties["kills"] + "";
                DeathsTexts[i].text = player.CustomProperties["deaths"] + "";
            }
            else
            {
                KillsTexts[i].text =  "0";
                DeathsTexts[i].text = "0";
            }

            i++;
        }




}
    private void Update()
    {
        playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
