using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class combatController : MonoBehaviour
{
    public int numPlayers, numEnemies;

    public combatEntity[] Players, Enemies;

    public bool readyForTurn = false;
    public bool playerTurn = true; // if false then its the enemies turn
    public int turnNum = 0;

    void Start()
    {
        Players = getPlayers();
        Enemies = getEnemies();
    }

    public static combatEntity[] getPlayers()
    {
        LinkedList<combatEntity> entities = new LinkedList<combatEntity>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("player");
        foreach(GameObject p in playerObjects)
        {
            entities.AddLast(p.GetComponent<combatEntity>());
        }
        return entities.ToArray();
    }
    public static combatEntity[] getEnemies()
    {
        LinkedList<combatEntity> entities = new LinkedList<combatEntity>();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject e in enemyObjects)
        {
            entities.AddLast(e.GetComponent<combatEntity>());
        }
        return entities.ToArray();
    }

    private void Update()
    {
        if (Players.All(p => !p.active))
        {
            Debug.Log("Game Over"); //////////////////////////////////////////////////////// placeholder for game over screen
        }
        else if (Enemies.All(e => !e.active))
        {
            Debug.Log("Victory"); /////////////////////////////////////////////////////////////// placeholder for victory screen
        }

        if (readyForTurn)
        {
            readyForTurn = false;
            if (playerTurn)
            {

            } 
            else
            {
                if (Enemies[turnNum].active)
                {
                    switch (Enemies[turnNum].GetType().ToString())
                    {
                        case "testenemy":
                            testenemy enemy = (testenemy)Enemies[turnNum];
                            enemy.bite(Players[Random.Range(0, Players.Length)]);
                            StartCoroutine(enemyAnimate());
                            break;
                    }
                    turnNum++;
                    if (turnNum >= Enemies.Length)
                    {
                        playerTurn = true;
                    }
                }
            }
        }
    }

    public IEnumerator enemyAnimate()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }
}
