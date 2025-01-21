using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Diagnostics;
using static UnityEngine.GraphicsBuffer;

public class boss1Controller : MonoBehaviour
{
    public tentacle[] tentacles = new tentacle[8];
    public Sprite tent, projectileSprite;
    public static GameObject mc;
    public static Tilemap water;
    public static Tile waterTile;

    public Tilemap water_;
    public Tile waterTile_;

    public bool isCombat = false;

    public int phase = 0;

    public int mcHp, companionHp;

    public Vector3 playerPos = Vector3.zero;

    public Sprite[] submergeAnim;
    public Sprite[] emergeAnim;
    public Sprite[] slamAnim;
    public Sprite[] splashAnim;
    public Sprite[] slashAnim;

    public GameObject slamPrefab, slashPrefab;

    void Start()
    {
        tentacle.slamPrefab = slamPrefab;
        tentacle.emergeAnim = emergeAnim;
        tentacle.submergeAnim = submergeAnim;
        tentacle.slamAnim = slamAnim;
        tentacle.projectile = projectileSprite;
        tentacle.splashAnim = splashAnim;
        tentacle.slashAnim = slashAnim;
        tentacle.slashPrefab = slashPrefab;

        mc = GameObject.FindGameObjectWithTag("player");
        if (playerPos != Vector3.zero)
        {
            mc.transform.position = playerPos;
        }
        water = water_;
        waterTile = waterTile_;
        tentacle.baseSprite = tent;
        geyser.proj = projectileSprite;
        
        if (GameObject.Find("tentacle") != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            tentacle.controller = this;
            for (int i = 0; i < 8; i++)
            {
                tentacles[i] = tentacle.create();
            }
        }
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
        tentacles[0].activate(closestWater());
        print("starting");
        print(tentacles[0].gameObject.transform.position);
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
        tentacles[0].emerge(closestHorizontalWater());
        yield return new WaitForSeconds(tentacle.emergeTime);
        tentacles[0].slam(mc.transform.position);
        yield return new WaitForSeconds(2);
    }

    public IEnumerator phase1()
    {
        print("phase 1 begins");
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
            actives[0].emerge(closestHorizontalWater());
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
                try
                {
                    actives[1].slash(mc.transform.position);
                }
                catch
                {
                    yield break;
                }
                yield return new WaitForSeconds(tentacle.slashTime);
                actives[1].submerge();
                yield return new WaitForSeconds(tentacle.submergeTime);
            }
            actives[1].emerge(closestHorizontalWater());
            yield return new WaitForSeconds(tentacle.emergeTime);
            actives[1].slam(mc.transform.position);
        }
        else if(actives.Count == 3)
        {
            actives[0].submerge();
            actives[1].submerge();
            actives[2].submerge();

            actives[0].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime / 3);
            actives[1].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime / 3);
            actives[2].emerge(closestWater());
            yield return new WaitForSeconds(tentacle.emergeTime / 3);
            actives[0].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime / 3);
            actives[1].slash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime / 3);
            actives[2].splash(mc.transform.position);
            yield return new WaitForSeconds(tentacle.slashTime / 3);
            actives[0].slash(mc.transform.position);
            yield return new WaitUntil(() => actives[1].ready);
            actives[1].submerge();
            yield return new WaitUntil(() => actives[2].ready);
            actives[2].submerge();
            yield return new WaitUntil(() => actives[0].ready);
            actives[0].submerge();
            yield return new WaitForSeconds(tentacle.submergeTime);

            StartCoroutine(tentacleAttackRandom(actives[0], 4, 6));
            yield return new WaitForSeconds(tentacle.emergeTime / 3);
            StartCoroutine(tentacleAttackRandom(actives[1], 6, 8));
            yield return new WaitForSeconds(tentacle.emergeTime / 3);
            StartCoroutine(tentacleAttackRandom(actives[2], 8, 10));
        }
        else if(actives.Count == 4)
        {
            StartCoroutine(tentacleAttackRandom(actives[0], 5, 7));
            yield return new WaitForSeconds(tentacle.emergeTime / 4);
            StartCoroutine(tentacleAttackRandom(actives[1], 7, 9));
            yield return new WaitForSeconds(tentacle.emergeTime / 4);
            StartCoroutine(tentacleAttackRandom(actives[2], 8, 10));
            yield return new WaitForSeconds(tentacle.emergeTime / 4);
            StartCoroutine(tentacleAttackRandom(actives[3], 10, 12));

        }

        yield break;
    }

    public IEnumerator tentacleAttackRandom(tentacle t, int p1, int p2)
    {
        for (int i = 0; i < (int)Random.Range(p1, p2); i++)
        {
            try
            {
                t.emerge(closestWater());
            }
            catch
            {
                yield break;
            }
            yield return new WaitForSeconds(tentacle.emergeTime);
            switch ((int)Random.Range(0, 3))
            {
                case 0:
                    try
                    {
                        t.slash(mc.transform.position);
                    }
                    catch
                    {
                        yield break;
                    }
                    yield return new WaitForSeconds(tentacle.slashTime);
                    break;
                case 1:
                    try
                    {
                        t.splash(mc.transform.position);
                    }
                    catch
                    {
                        yield break;
                    }
                    yield return new WaitForSeconds(tentacle.splashTime);
                    break;
                case 2:
                    try
                    {
                        t.slash(mc.transform.position);
                    }
                    catch
                    {
                        yield break;
                    }
                    yield return new WaitForSeconds(tentacle.slashTime);
                    try
                    {
                        t.splash(mc.transform.position);
                    }
                    catch
                    {
                        yield break;
                    }
                    yield return new WaitForSeconds(tentacle.splashTime);
                    break;
            }
            try
            {
                t.submerge();
            }
            catch
            {
                yield break;
            }
        }
        try
        {
            t.emerge(closestHorizontalWater());
        }
        catch
        {
            yield break;
        }
        yield return new WaitForSeconds(tentacle.emergeTime);
        try
        {
            t.slam(mc.transform.position);
        }
        catch
        {
            yield break;

        }

    }

    public static Vector3 findWater(float distance)
    {
        try
        {
            List<Vector3> tiles = new();
            for (float angle = 0; angle < 360; angle++)
            {
                if (water.GetTile(water.WorldToCell(mc.transform.position + fromPolar(distance, angle))) != null)
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
        catch
        {
            return Vector3.zero;
        }
    }

    public static Vector3 HorizontalWater(float distance)
    {
        try
        {
            List<Vector3> tiles = new();
            for (float angle = 0; angle < 2; angle++)
            {
                if (water.GetTile(water.WorldToCell(mc.transform.position + fromPolar(distance, angle * 180))) != null)
                {
                    tiles.Add(mc.transform.position + fromPolar(distance, angle));
                }
            }
            if (tiles.Count == 0)
            {
                return Vector3.zero;
            }
            return -tiles[(int)Random.Range(0, tiles.Count)] + 2*  mc.transform.position;
        }
        catch
        {
            return Vector3.zero;
        }
    }

    public static Vector3 closestHorizontalWater()
    {
        Vector3 result = Vector3.zero;
        int count = 1;
        while (result == Vector3.zero)
        {
            result = HorizontalWater(count);
            count++;
            if (count == 20)
            {
                return Vector3.zero;
            }
        }
        return result;
    }

    public static Vector3 findWaterFrom(float distance, Vector3 target)
    {
        try
        {
            List<Vector3> tiles = new();
            for (float angle = 0; angle < 360; angle++)
            {
                if (water.GetTile(water.WorldToCell(target + fromPolar(distance, angle))) != null)
                {
                    tiles.Add(target + fromPolar(distance, angle));
                }
            }
            if (tiles.Count == 0)
            {
                return Vector3.zero;
            }
            return tiles[(int)Random.Range(0, tiles.Count)];
        }
        catch
        {
            return Vector3.zero;
        }
    }

    public static Vector3 closestWaterFrom(Vector3 target)
    {
        Vector3 result = Vector3.zero;
        int count = 1;
        while (result == Vector3.zero)
        {
            result = findWaterFrom(count, target);
            count++;
            if (count == 20)
            {
                return Vector3.zero;
            }
        }
        return result;
    }

    public static Vector3 closestWater()
    {
        Vector3 result = Vector3.zero;
        int count = 1;
        while (result == Vector3.zero)
        {
            result = findWater(count);
            count++;
            if (count == 20)
            {
                return Vector3.zero;
            }
        }
        return result;
    }

    public static Vector3 fromPolar(float magnitude, float angle)
    {
        return new Vector3(magnitude * Mathf.Cos(angle * Mathf.Deg2Rad), magnitude * Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public IEnumerator enterCombat(tentacle t)
    {
        yield return new WaitForFixedUpdate();
        DontDestroyOnLoad(this.gameObject);
        isCombat = true;
        geyserController.combat = true;
        foreach(tentacle c in tentacles)
        {
            c.transform.Translate(new Vector3(1000, 0, 0));
        }
        playerPos = mc.transform.position;
        combatStarter combatStarter = new combatStarter(2, 1, new string[] { "tentacle" }, this.gameObject);
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
        t.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        t.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        int numAlive = 0;
        foreach (tentacle c in tentacles)
        {
            if (c.alive)
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
            StartCoroutine(phase1());
        }
    }

}
