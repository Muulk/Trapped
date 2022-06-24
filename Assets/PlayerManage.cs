using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public List<Player> playerList = new List<Player>();
    private int activePlayer;
    // Start is called before the first frame update
    void Start()
    {
        activePlayer = 0;
        playerList[activePlayer].GetComponent<Player>().isActive = true;

        Debug.Log(playerList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (activePlayer < playerList.Count -1)
            {
                playerList[activePlayer].GetComponent<Player>().isActive = false;

                activePlayer++;
                playerList[activePlayer].GetComponent<Player>().isActive = true;
            } else if (activePlayer == playerList.Count -1)
            {
                playerList[activePlayer].GetComponent<Player>().isActive = false;

                activePlayer = 0;
                playerList[activePlayer].GetComponent<Player>().isActive = true;

            }
        }
    }
}
