using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class boss1Controller : MonoBehaviour
{
    public tentacle[] tentacles = new tentacle[8];
    public Sprite tent;
    public static GameObject mc;
    public static Tilemap water;
    public static Tile waterTile;

    public Tilemap water_;
    public Tile waterTile_;

    public bool isCombat = false;

    public int phase = 0;

    public Vector3 playerPos = Vector3.zero;

    void Start()
    {
        tentacle.controller = this;
        mc = GameObject.FindGameObjectWithTag("player");
        if (playerPos != Vector3.zero)
        {
            mc.transform.position = playerPos;
        }
        water = water_;
        waterTile = waterTile_;
        tentacle.baseSprite = tent;
        geyser.proj = tent;
        
        for (int i = 0;i < 8;i++) {
            tentacles[i] = tentacle.create();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == mc && phase == 0)
        {
            startFight();
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void startFight()
    {
        //////////////////////// block exit placeholder
        tentacles[0].activate(findWater(8));
        geyserController.geyserTime = 5;
        geyserController.cd = 0;
        StartCoroutine(phase0());
    }

    public IEnumerator phase0()
    {
        yield return new WaitForSeconds(tentacle.emergeTime + 2);
        tentacles[0].submerge();
        yield return new WaitForSeconds(tentacle.submergeTime + 1);
        tentacles[0].emerge(closestWater());
        yield return new WaitForSeconds(tentacle.emergeTime);
        tentacles[0].slash(mc.transform.position);
        yield return new WaitForSeconds(tentacle.slashTime);
        tentacles[0].submerge();
        yield return new WaitForSeconds(tentacle.submergeTime + 1);
        tentacles[0].emerge(closestWater());
        yield return new WaitForSeconds(tentacle.emergeTime);
        tentacles[0].splash(mc.transform.position);
        yield return new WaitForSeconds(tentacle.splashTime);
        tentacles[0].submerge();
        yield return new WaitForSeconds(tentacle.submergeTime + 1);
        tentacles[0].emerge(closestWater());
        yield return new WaitForSeconds(tentacle.emergeTime);
        tentacles[0].slam(mc.transform.position);
        yield return new WaitForSeconds(2);
    }

    public IEnumerator phase1()
    {
        Debug.Log("phase 1 begins");
        int numAlive = 0;
        List<tentacle> actives = new();
        foreach (tentacle t in tentacles)
        {
            if (t.alive)
            {
                numAlive++;
                if (t.active)
                {
                    actives.Add(t);
                }
            }
        }


        switch (numAlive)
        {
            case 7:
                {
                    tentacles[1].activate(findWater(8));
                    tentacles[2].activate(findWater(8));
                    actives.Add(tentacles[1]);
                    actives.Add(tentacles[2]);
                    break;
                }
            case 6:
                {
                    tentacles[3].activate(findWater(8));
                    tentacles[4].activate(findWater(8));
                    actives.Add(tentacles[3]);
                    actives.Add(tentacles[4]);
                    break;
                }
            case 5:
                tentacles[5].activate(findWater(8));
                tentacles[6].activate(findWater(8));
                actives.Add(tentacles[5]);
                actives.Add(tentacles[6]);
                break;
            case 4:
                tentacles[7].activate(findWater(8));
                actives.Add(tentacles[7]);
                break;
        }

        if (actives.Count == 2)
        {
            actives[0].submerge();
            actives[1].submerge();
            yield return new WaitForSeconds(tentacle.submergeTime + 3);
            actives[0].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].emerge(closestWater());
            actives[0].slash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime - tentacle.emergeTime);
            actives[0].submerge();
            yield return new WaitForSeconds(tentacle.splashTime + tentacle.emergeTime - tentacle.slashTime);
            actives[1].submerge();
            actives[0].emerge(closestWater());
            yield return new WaitForSeconds(Mathf.Max(tentacle.emergeTime, tentacle.submergeTime));
            actives[1].emerge(closestWater());
            actives[0].slash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime - tentacle.emergeTime);
            actives[0].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.splashTime + tentacle.emergeTime - tentacle.slashTime);
            actives[1].submerge();
            yield return new WaitForSeconds(tentacle.slashTime - tentacle.emergeTime);
            actives[0].submerge();
            yield return new WaitForSeconds(tentacle.submergeTime);
            actives[0].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].emerge(closestWater());
            actives[0].slash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime - tentacle.emergeTime);
            actives[0].submerge();
            yield return new WaitForSeconds(tentacle.splashTime + tentacle.emergeTime - tentacle.slashTime);
            actives[1].submerge();
            actives[0].emerge(closestWater());
            yield return new WaitForSeconds(Mathf.Max(tentacle.emergeTime, tentacle.submergeTime));
            actives[1].emerge(closestWater());
            actives[0].slash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime - tentacle.emergeTime);
            actives[0].slam(mc.transform.position);
            yield return new WaitForSeconds(tentacle.splashTime + tentacle.emergeTime - tentacle.slashTime);
            actives[1].submerge();
            yield return new WaitForSeconds(tentacle.submergeTime);
            for (int i = 0; i < 5; i++)
            {
                actives[1].emerge(closestWater());
                yield return new WaitForSeconds(tentacle.emergeTime);
                actives[1].slash(mc.transform.position);
                yield return new WaitForSeconds(tentacle.slashTime);
                actives[1].submerge();
                yield return new WaitForSeconds(tentacle.submergeTime);
            }
            actives[1].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].slam(mc.transform.position);
        }

        yield break;
    }

    public static Vector3 findWater(float distance)
    {
        List<Vector3> tiles = new();
        for (float angle = 0; angle < 360; angle++)
        {
            if (water.GetTile(water.WorldToCell(mc.transform.position + fromPolar(distance, angle))) == waterTile)
            {
                tiles.Add(mc.transform.position + fromPolar(distance, angle));
            }
        }
        if (tiles.Count == 0)
        {
            return Vector3.zero;
        }
        return tiles[(int)Random.Range(0, tiles.Count)];
    }

    public static Vector3 closestWater()
    {
        Vector3 result = Vector3.zero;
        int count = 1;
        while (result == Vector3.zero)
        {
            result = findWater(count);
            count++;
        }
        return result;
    }

    public static Vector3 fromPolar(float magnitude, float angle)
    {
        return new Vector3(magnitude * Mathf.Cos(angle * Mathf.Deg2Rad), magnitude * Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public IEnumerator enterCombat(tentacle t)
    {
        isCombat = true;
        geyserController.combat = true;
        foreach(tentacle c in tentacles)
        {
            c.transform.Translate(new Vector3(1000, 0, 0));
        }
        
        playerPos = mc.transform.position;
        combatStarter combatStarter = new combatStarter(2, 1, new string[] { "tentacle" }, this.gameObject);
        print("tried combat");
        yield return new WaitUntil(() => !isCombat);
        SceneManager.LoadScene("Boss1");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Boss1");
        foreach (tentacle c in tentacles)
        {
            c.transform.Translate(new Vector3(-1000, 0, 0));
        }
        mc.transform.position = playerPos;
        geyserController.combat = false;
        t.alive = false;
        int numAlive = 0;
        foreach (tentacle c in tentacles)
        {
            if (t.alive)
            {
                numAlive++;
            }
        }
        if (numAlive == 2)
        {
            print("phase 2");
        }
        else
        {
            print(numAlive);
            StartCoroutine(phase1());
        }
    }

}
