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
        // Instancia el prefab
        GameObject newItem = Instantiate(playerInfoLeaderboard);

        // Establece el nuevo objeto como hijo del contenedor
        newItem.transform.SetParent(leaderboardContainer);

        // Reinicia las propiedades de escala y posición para ajustarse al layout
        newItem.transform.localScale = Vector3.one;
        newItem.transform.localPosition = Vector3.zero;
    }
}
