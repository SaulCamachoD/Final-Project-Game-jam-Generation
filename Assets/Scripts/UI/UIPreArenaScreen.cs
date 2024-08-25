using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreArenaScreen : MonoBehaviour
{
    public TestPlayerInfo[] players;
    public Transform leaderboardContainer;
    public GameObject playerInfoLeaderboard;

    private void Start()
    {
        PlayerInfoLeaderboardCreator();
    }

    public void PlayerInfoLeaderboardCreator() 
    {
        for (int i = 0; i < players.Length; i ++)  
        {
            // Instancia el prefab
            GameObject newItem = Instantiate(playerInfoLeaderboard);

            // Establece el nuevo objeto como hijo del contenedor
            newItem.transform.SetParent(leaderboardContainer);

            // Reinicia las propiedades de escala y posición para ajustarse al layout
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;

            newItem.GetComponent<UIPlayerInfoLeaderboard>().playerName.text = players[i].playerName;
            newItem.GetComponent<UIPlayerInfoLeaderboard>().playerHealthBar.value = players[i].playerHealthBar;
            newItem.GetComponent<UIPlayerInfoLeaderboard>().playerKills.text = players[i].playerKills.ToString();
            newItem.GetComponent<UIPlayerInfoLeaderboard>().playerPortrait.sprite = players[i].playerPortrait;
        }
        
    }
}
