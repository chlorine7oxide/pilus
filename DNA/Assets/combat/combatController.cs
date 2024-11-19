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
                Debug.Log("Game Over");
                SceneManager.LoadScene("endFail");
                Destroy(this.gameObject);
            }
            else if (Enemies.All(e => !e.active))
            {
                Debug.Log("Victory");
                SceneManager.LoadScene("endSuccess");
                Destroy(this.gameObject);
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
        ((MC)Players[0]).entity.AddComponent<SpriteRenderer>().sprite = mcSprite;
        ((MC)Players[0]).entity.transform.Translate(new Vector3(-6, 0, 0));
        Players[1] = new friend(playerData.hp, playerData.def);
        ((friend)Players[1]).entity.AddComponent<SpriteRenderer>().sprite = friendSprite;
        ((friend)Players[1]).entity.transform.Translate(new Vector3(-2, 0, 0));
        for (int i = 0; i < enemys.Length; i++)
        {
            switch (enemys[i])
            {
                case "testenemy":
                    {
                        Enemies[i] = new testenemy(50, 0);
                        ((testenemy)Enemies[i]).entity.AddComponent<SpriteRenderer>().sprite = testenemySprite;
                        ((testenemy)Enemies[i]).entity.transform.Translate(new Vector3(2*i, 3, 0));
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

        if (Players[turnNum].hp <= 0)
        {
            readyForTurn = true;
            yield break;
        }

        // action type placeholder
        menuSelector.response = null;
        GameObject sel = GameObject.FindGameObjectWithTag("selector");
        sel.AddComponent<menuSelector>();
        yield return new WaitUntil(() => menuSelector.response != null);
        Debug.Log(menuSelector.response);
        string response = menuSelector.response;
        Destroy(sel.GetComponent<menuSelector>());
        sel.transform.Translate(new Vector3(100, 0, 0));

        // ability selector
        if (response == "ability")
        {
            if (turnNum == 0)
            {
                selector = new scroller(5, 3, playerData.MCabilities.ToArray(), 1, new Vector2(-5, -3)); 
            }
            else
            {
                selector = new scroller(5, 3, playerData.friendAbilities.ToArray(), 1, new Vector2(-5, -3));
            }
        }
        else
        {
            selector = new scroller(5, 3, playerData.items.ToArray(), 1, new Vector2(3, -3));
        }
        scroller.result = null;
        yield return new WaitUntil(() => scroller.result != null);

        combatEntity target = null;

        if (scroller.result == "attack" || scroller.result == "kick" || scroller.result == "insult" || scroller.result == "swing" || scroller.result == "molotov")
        {
            if (numEnemies > 1)
            {
                //choose enemy placeholder
                int count = 0;
                Dictionary<int, combatEntity> enemyDict = new Dictionary<int, combatEntity>();
                foreach (combatEntity c in getEnemies())
                {
                    enemyDict.Add(count, c);
                    count++;
                }
                entitySelector.selectedEntity = null;
                sel.AddComponent<entitySelector>();
                sel.GetComponent<entitySelector>().entities = enemyDict;
                yield return new WaitUntil(() => entitySelector.selectedEntity != null);
                target = entitySelector.selectedEntity;
                Destroy(sel.GetComponent<entitySelector>());
                sel.transform.Translate(new Vector3(100, 0, 0));
            }
            else
            {
                target = Enemies[0];
            }
        }

        if (scroller.result == "heal" || scroller.result == "meditate" || scroller.result == "potion" || scroller.result == "bigpotion")
        {
            if (numPlayers > 1)
            {
                int count = 0;
                Dictionary<int, combatEntity> playerDict = new Dictionary<int, combatEntity>();
                foreach (combatEntity c in getPlayers())
                {
                    playerDict.Add(count, c);
                    count++;
                }
                entitySelector.selectedEntity = null;
                sel.AddComponent<entitySelector>();
                sel.GetComponent<entitySelector>().entities = playerDict;
                yield return new WaitUntil(() => entitySelector.selectedEntity != null);
                target = entitySelector.selectedEntity;
                Destroy(sel.GetComponent<entitySelector>());
                sel.transform.Translate(new Vector3(100, 0, 0));
            }
            else
            {
                target = Players[0];
            }
        }

        if (turnNum == 1)
        {
            switch (scroller.result)
            {
                case "attack":
                    ((friend)Players[1]).attack(target);
                    break;
                case "heal":
                    ((friend)Players[1]).heal(target);
                    break;
                case "meditate":
                    ((friend)Players[1]).meditate(target);
                    break;
                case "swing":
                    ((friend)Players[1]).swing(target);
                    break;  
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
        Debug.Log("player tyurn");
        turnNum++;
        if (turnNum == 2)
        {
            turnNum = 0;
            playerTurn = false;
        }
        readyForTurn = true;
    }
}
