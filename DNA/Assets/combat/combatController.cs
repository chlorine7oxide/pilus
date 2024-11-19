using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using Mono.Cecil;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class combatController : MonoBehaviour
{
    public int numPlayers, numEnemies;

    public combatEntity[] Players, Enemies;
    public string[] enemys;

    public bool readyForTurn = false;
    public bool playerTurn = true; // if false then its the enemies turn
    public int turnNum = 0;
    public bool started = false;

    public static Sprite mcSprite, friendSprite, testenemySprite;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("combatBasic");

        StartCoroutine(starter());
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

        if (started)
        {
            if (Players.All(p => !p.active))
            {
                Debug.Log("Game Over"); //////////////////////////////////////////////////////// placeholder for game over screen
            }
            else if (Enemies.All(e => !e.active))
            {
                Debug.Log("Victory"); /////////////////////////////////////////////////////////////// placeholder for victory screen
            }
        }
        

        if (readyForTurn)
        {
            readyForTurn = false;
            if (playerTurn)
            {
                StartCoroutine(takePlayerTurn());
            } 
            else
            {
                if (Enemies[turnNum].active)
                {
                    switch (Enemies[turnNum].GetType().ToString())
                    {
                        case "testenemy":
                            testenemy enemy = (testenemy)Enemies[turnNum];
                            combatEntity t = Players[Random.Range(0, Players.Length)];
                            enemy.bite(t);
                            Debug.Log(t + " " + t.hp);
                            StartCoroutine(enemyAnimate());
                            break;
                    }
                    turnNum++;
                    if (turnNum >= Enemies.Length)
                    {
                        playerTurn = true;
                        turnNum = 0;
                    }
                }
            }
        }
    }

    public IEnumerator starter()
    {
        yield return new WaitForSeconds(1);
        Players = new combatEntity[numPlayers];
        Enemies = new combatEntity[numEnemies];

        Players[0] = new MC(playerData.hp, playerData.def);
        ((MC)Players[0]).mc.AddComponent<SpriteRenderer>().sprite = mcSprite;
        ((MC)Players[0]).mc.transform.Translate(new Vector3(-6, 0, 0));
        Players[1] = new friend(playerData.hp, playerData.def);
        ((friend)Players[1]).friendObj.AddComponent<SpriteRenderer>().sprite = friendSprite;
        ((friend)Players[1]).friendObj.transform.Translate(new Vector3(-2, 0, 0));
        for (int i = 0; i < enemys.Length; i++)
        {
            switch (enemys[i])
            {
                case "testenemy":
                    {
                        Enemies[i] = new testenemy(50, 0);
                        ((testenemy)Enemies[i]).testEnemy.AddComponent<SpriteRenderer>().sprite = testenemySprite;
                        ((testenemy)Enemies[i]).testEnemy.transform.Translate(new Vector3(2*i, 3, 0));
                        break;
                    }

            }
        }
        readyForTurn = true;
        started = true;
    }

    public IEnumerator enemyAnimate()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }

    protected scroller selector;

    public IEnumerator takePlayerTurn()
    {
        // action type placeholder
        yield return new WaitForSeconds(1);

        // ability selector

        if (turnNum == 0)
        {
            selector = new scroller(5, 3, playerData.MCabilities.ToArray(), 1, new Vector2(-5, -3));
            scroller.result = null;
        }
        else
        {
            selector = new scroller(5, 3, playerData.friendAbilities.ToArray(), 1, new Vector2(-5, -3));
            scroller.result = null;
        }
        
        yield return new WaitUntil(() => scroller.result != null);

        combatEntity target = null;

        if (scroller.result == "attack" || scroller.result == "kick" || scroller.result == "insult" || scroller.result == "swing")
        {
            if (numEnemies > 1)
            {
                //choose enemy placeholder
                target = Enemies[0];
            }
            else
            {
                target = Enemies[0];
            }
        }

        if (scroller.result == "heal" || scroller.result == "meditate")
        {
            if (numPlayers > 1)
            {
                //choose player placeholder
                target = Players[0];
            }
            else
            {
                target = Players[0];
            }
        }

        if (turnNum == 1)
        {
            if (target is not null)
            {
                
            }
            else
            {
                
            }
        } 
        else
        {
            switch (scroller.result)
            {
                case "attack":
                    ((MC)Players[0]).attack(target);
                    break;
                case "heal":
                    ((MC)Players[0]).heal(target);
                    break;
                case "kick":
                    ((MC)Players[0]).kick(target);
                    break;
                case "insult":
                    ((MC)Players[0]).insult(target);
                    break;

            }
        }      

        turnNum++;
        if (turnNum == 2)
        {
            turnNum = 0;
            playerTurn = false;
        }
        readyForTurn = true;
    }
}
