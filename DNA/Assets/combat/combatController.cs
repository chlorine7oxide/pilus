using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using Mono.Cecil;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class combatController : MonoBehaviour
{
    public int numPlayers, numEnemies;

    public int mcHp = -1, friendHp = -1;

    public combatEntity[] Players, Enemies;
    public bool[] defended;
    public string[] enemys;

    public bool readyForTurn = false;
    public bool playerTurn = true; // if false then its the enemies turn
    public int turnNum = 0;
    public bool started = false;

    public static Sprite mcSprite, friendSprite, testenemySprite;

    public static GameObject[] buttons = new GameObject[4];

    public GameObject tentaclePrefab;

    public static Sprite crowTop, crowIdle, crowSide;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("combatBasic");

        StartCoroutine(starter());
    }

    public combatEntity[] getPlayers()
    {
        return Players;
    }
    public combatEntity[] getEnemies()
    {
        return Enemies;
    }

    private void Update()
    {

        if (started)
        {
            if (Players.All(p => !p.active))
            {
                SceneManager.LoadScene("endFail");
                Destroy(this.gameObject);
            }
            else if (Enemies.All(e => !e.active))
            {
                if (GameObject.Find("boss1") != null)
                {
                    GameObject g = GameObject.Find("boss1");
                    g.GetComponent<boss1Controller>().isCombat = false;
                    g.GetComponent<boss1Controller>().mcHp = Players[0].hp;
                    g.GetComponent<boss1Controller>().companionHp = Players[1].hp;
                    Destroy(this.gameObject);
                }
                else if (enemys.Contains("rat"))
                {
                    playerData.ratBeaten = true;
                    SceneManager.LoadScene("endSuccess");
                    Destroy(this.gameObject);
                }
                else
                {
                    SceneManager.LoadScene("endSuccess");
                    Destroy(this.gameObject);
                }
               
            }
            else if (enemys.Contains("boulder"))
            {
                if (Enemies[0].hp <= 0)
                {
                    playerData.crowBeaten = true;
                    SceneManager.LoadScene("endSuccess");
                    
                    Destroy(this.gameObject);
                }
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
                        case "tentacleEnemy":
                            tentacleEnemy tent = (tentacleEnemy)Enemies[turnNum];
                            switch ((int)Random.Range(0, 2)) {
                                case 0:
                                    {
                                        tent.slash(Players);
                                        StartCoroutine(tentacleAnimate());
                                        break;
                                    }
                                case 1:
                                    {
                                        tent.slam(Players[(int)Random.Range(0, 2)]);
                                        StartCoroutine(tentacleAnimate());
                                        break;
                                    }
                            }

                            StartCoroutine(tentacleAnimate());
                            break;
                        case "crowEnemy":
                            {
                                crowEnemy crow = (crowEnemy)Enemies[turnNum];
                                if (Random.Range(0, 2) == 0)
                                {
                                    combatEntity target = Players[Random.Range(0, Players.Length)];
                                    
                                    StartCoroutine(crowDive(target));
                                }
                                else
                                {
                                    crow.doubleDive(Players[0], Players[1]);
                                    StartCoroutine(crowDoubleDive(Players[0]));

                                }
                                break;
                            }
                        case "boulderEnemy":
                            {
                                boulderEnemy boulder = (boulderEnemy)Enemies[turnNum];
                                boulder.shield(Enemies[Random.Range(0, Enemies.Length)]);
                                StartCoroutine(boulderAnimate());
                                break;
                            }
                        case "ratEnemy":
                            {
                                ratEnemy rat = (ratEnemy)Enemies[turnNum];
                                if(Random.Range(0, 2) == 0)
                                {
                                    rat.scratch(Players[Random.Range(0, Players.Length)]);
                                    StartCoroutine(ratScratch());
                                }
                                else
                                {
                                    rat.dodge();
                                    StartCoroutine(ratDodge());
                                }
                                if (!Enemies[1].active)
                                {
                                    rat.def = Mathf.Min(0, rat.def);
                                    rat.scratch(Players[Random.Range(0, Players.Length)]);
                                    if (!rat.enrageSeen)
                                    {
                                        rat.enrageSeen = true;
                                        generalText.create("It's gotten angrier!", ratEnemy.portrait, null, true);
                                    }
                                }
                                break;
                            }
                        case "ratTailEnemy":
                            {
                                ratTailEnemy ratTail = (ratTailEnemy)Enemies[turnNum];
                                ratTail.hit(Players[0], Players[1]);
                                StartCoroutine(ratScratch());
                                break;
                            }
                        case "skeleton":
                            {
                                StartCoroutine(boneJangle());
                                break;
                            }
                    }
                    turnNum++;
                    if (turnNum >= Enemies.Length)
                    {
                        playerTurn = true;
                        turnNum = 0;
                    }
                }
                else
                {
                    readyForTurn = true;
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
        yield return new WaitForSeconds(0.01f);
        Players = new combatEntity[numPlayers];
        Enemies = new combatEntity[numEnemies];
        defended = new bool[numPlayers];

        Players[0] = new MC(playerData.hp, playerData.def);
        if (mcHp != -1)
        {
            Players[0].hp = mcHp;
        }
        ((MC)Players[0]).entity.AddComponent<SpriteRenderer>().sprite = mcSprite;
        ((MC)Players[0]).entity.transform.Translate(new Vector3(-6, 0, 0));
        Players[0].hpBar.transform.position = (Players[0].entity.transform.position);
        Players[0].hpBar2.transform.position = (Players[0].entity.transform.position);
        if (Players.Length == 2)
        {
            Players[1] = new friend(playerData.hp, playerData.def);
            if (friendHp != -1)
            {
                Players[1].hp = friendHp;
            }
            ((friend)Players[1]).entity.AddComponent<SpriteRenderer>().sprite = friendSprite;
            ((friend)Players[1]).entity.transform.Translate(new Vector3(-2, 0, 0));
            Players[1].hpBar.transform.position = (Players[1].entity.transform.position);
            Players[1].hpBar2.transform.position = (Players[1].entity.transform.position);
        }
        
        for (int i = 0; i < enemys.Length; i++)
        {
            switch (enemys[i])
            {
                case "testenemy":
                    {
                        Enemies[i] = new testenemy(50, 0);
                        ((testenemy)Enemies[i]).entity.AddComponent<SpriteRenderer>().sprite = testenemySprite;
                        ((testenemy)Enemies[i]).entity.transform.Translate(new Vector3(2*i, 3, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "tentacle":
                    {
                        Enemies[i] = new tentacleEnemy(80, 0);
                        ((tentacleEnemy)Enemies[i]).entity.transform.Translate(new Vector3(2 * i, 3, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "crow":
                    {
                        Enemies[i] = new crowEnemy(180, 0);
                        ((crowEnemy)Enemies[i]).entity.transform.Translate(new Vector3(2, 3, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "boulder":
                    {
                        Enemies[i] = new boulderEnemy(10000, 0);
                        ((boulderEnemy)Enemies[i]).entity.transform.Translate(new Vector3(2, 2, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "rat":
                    {
                        Enemies[i] = new ratEnemy(120, 0);
                        ((ratEnemy)Enemies[i]).entity.transform.Translate(new Vector3(1.5f, 2, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "ratTail":
                    {
                        Enemies[i] = new ratTailEnemy(50, 0);
                        ((ratTailEnemy)Enemies[i]).entity.transform.Translate(new Vector3(2.5f, 2, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
                        break;
                    }
                case "skeleton":
                    {
                        Enemies[i] = new skeletonEnemy(100, 0);
                        ((skeletonEnemy)Enemies[i]).entity.transform.Translate(new Vector3(2, 3, 0));
                        Enemies[i].hpBar.transform.position = (Enemies[i].entity.transform.position);
                        Enemies[i].hpBar2.transform.position = (Enemies[i].entity.transform.position);
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
    public IEnumerator tentacleAnimate()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }
    public IEnumerator crowDive(combatEntity target)
    {

        Animator anim = Enemies[0].entity.GetComponent<Animator>();
        
        anim.SetInteger("mode", 1);
        anim.StartPlayback();

        yield return new WaitForSeconds(0.2f);

        anim.StopPlayback();
        Enemies[0].entity.GetComponent<SpriteRenderer>().sprite = crowTop;

        for (int i = 0; i < 15; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(0, 0.3f, 0));
            yield return new WaitForFixedUpdate();
        }

        Enemies[0].entity.transform.position = new Vector3(target.entity.transform.position.x, Enemies[0].entity.transform.position.y, 0);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 40; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(0, -0.3f, 0));
            yield return new WaitForFixedUpdate();
        }

        ((crowEnemy)Enemies[0]).dive(target);
        Enemies[0].entity.GetComponent<SpriteRenderer>().sprite = crowIdle;
        Enemies[0].entity.GetComponent<SpriteRenderer>().flipX = false;
        Enemies[0].entity.transform.position = new Vector3(2, 7.5f, 0);

        for (int i = 0; i < 15; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(0, -0.3f, 0));
            yield return new WaitForFixedUpdate();
        }

        readyForTurn = true;
    }
    public IEnumerator crowDoubleDive(combatEntity target1)
    {
        Animator anim = Enemies[0].entity.GetComponent<Animator>();

        anim.SetInteger("mode", 1);
        anim.StartPlayback();
        Enemies[0].entity.GetComponent<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(0.2f);

        Enemies[0].entity.GetComponent<SpriteRenderer>().sprite = crowSide;

        for (int i = 0; i < 30; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(0.3f, 0, 0));
            yield return new WaitForFixedUpdate();
        }

        Enemies[0].entity.transform.position = new Vector3(-Enemies[0].entity.transform.position.x, target1.entity.transform.position.y, 0);

        yield return new WaitForSeconds(0.1f);

        anim.StopPlayback();

        for (int i = 0; i < 60; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(0.4f, 0, 0));
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.1f);

        Enemies[0].entity.transform.position = new Vector3(11, 3, 0);

        for (int i = 0; i < 30; i++)
        {
            Enemies[0].entity.transform.Translate(new Vector3(-0.3f, 0, 0));
            yield return new WaitForFixedUpdate();
        }
        readyForTurn = true;

        Enemies[0].entity.GetComponent<SpriteRenderer>().sprite = crowIdle;
        Enemies[0].entity.GetComponent<SpriteRenderer>().flipX = false;
    }
    public IEnumerator boulderAnimate()
    {
        float time = 0;

        while (time < 0.6f)
        {
            Enemies[0].entity.transform.localScale = new Vector3(-1.5f * time * (time - 0.6f) + 1, -1.5f* time * (time - 0.6f) + 1, 1);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        readyForTurn = true;
    }
    public IEnumerator ratScratch()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }
    public IEnumerator ratDodge()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }
    public IEnumerator ratTail()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }
    public IEnumerator boneJangle()
    {
        yield return new WaitForSeconds(1);
        readyForTurn = true;
    }

    protected dynamicSelectorText selector;

    public IEnumerator takePlayerTurn()
    {

        if (enemys.Contains("rat"))
        {
            if (!Enemies[1].active)
            {
                ((ratEnemy)Enemies[0]).msgSeen = true;
            }
        }
        
        if (turnNum >= Players.Length)
        {
            turnNum = 0;
        }

        if (Players[turnNum].hp <= 0)
        {
            readyForTurn = true;
            yield break;
        }

        if (defended[turnNum])
        {
            Players[turnNum].def -= 10;
            defended[turnNum] = false;
        }

        // action type placeholder
        staticSelector s = staticSelector.create(buttons, 2, mcSprite);
        yield return new WaitUntil(() => s.done);
        int res = s.result;
        s.destroy();

        // ability selector
        switch (res)
        {
            case 0: // ability
                Vector3[] pos = new Vector3[5]
                {
                    new Vector3(-3, -2, 0),
                    new Vector3(-3, -2.5f, 0),
                    new Vector3(-3, -3, 0),
                    new Vector3(-3, -3.5f, 0),
                    new Vector3(-3, -4, 0),
                };
                selector = dynamicSelectorText.create(pos, turnNum == 0 ? playerData.MCabilities.ToArray() : playerData.friendAbilities.ToArray(), mcSprite, new Vector3(0, 0.3f, 0));
                yield return new WaitUntil(() => selector.done);
                string ability = turnNum == 0 ? playerData.MCabilities[selector.result] : playerData.friendAbilities[selector.result];
                selector.destroy();

                // target selector
                if (ability == "Punch" || ability == "Slam" || ability == "attack" || ability == "swing" || ability == "Insult" || ability == "Check")
                {
                    staticSelector se = staticSelector.create(Enemies.Select(e => e.entity).ToArray(), 1, mcSprite);
                    yield return new WaitUntil(() => se.done);
                    combatEntity res2 = Enemies[se.result];

                    switch (ability)
                    {
                        case "Punch":
                            ((MC)Players[turnNum]).punch(res2);
                            break;
                        case "Slam":
                            ((MC)Players[turnNum]).slam(res2);
                            break;
                        case "attack":
                            ((friend)Players[turnNum]).attack(res2);
                            break;
                        case "swing":
                            ((friend)Players[turnNum]).swing(res2);
                            break;
                        case "Insult":
                            ((MC)Players[turnNum]).insult(res2);
                            break;
                        case "Check":
                            ((MC)Players[turnNum]).check(res2);
                            break;
                    }
                }
                else if (ability == "heal" || ability == "Focus" || ability == "meditate")
                {
                    staticSelector se = staticSelector.create(Players.Select(e => e.entity).ToArray(), 1, mcSprite);
                    yield return new WaitUntil(() => se.done);
                    combatEntity res2 = Players[se.result];
                    switch (ability)
                    {
                        case "heal":
                            ((friend)Players[turnNum]).heal(res2);
                            break;
                        case "Focus":
                            ((MC)Players[turnNum]).focus(res2);
                            break;
                        case "meditate":
                            ((friend)Players[turnNum]).meditate(res2);
                            break;
                    }
                }
                break;
            case 2: // defend
                defended[turnNum] = true;
                break;
            case 1: // item
                Vector3[] poss = new Vector3[5]
                {
                    new Vector3(0, -2, 0),
                    new Vector3(0, -2.5f, 0),
                    new Vector3(0, -3, 0),
                    new Vector3(0, -3.5f, 0),
                    new Vector3(0, -4, 0),
                };

                selector = dynamicSelectorText.create(poss, playerData.items.ToArray(), mcSprite);
                yield return new WaitUntil(() => selector.done);
                Debug.Log(selector.result + " result");
                Debug.Log(playerData.items.Count + " length"); 
                string item = playerData.items[selector.result];
                selector.destroy();
                

                switch (item)
                {
                    case "Potion":
                        Players[turnNum].heal(20);
                        break;
                    case "Defend":
                        defended[turnNum] = true;
                        break;
                }
                playerData.items.Remove(item);

                break;
            case 3: // run
                break;
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
