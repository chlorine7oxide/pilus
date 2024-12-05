using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;

public class testEnemyOverworld : MonoBehaviour
{
    public Sprite proj;
    public bool dashingShort = false;
    public bool dashingLong = false;
    public bool throughDashing = false;
    public bool phased = false;
    public int phase;

    public float speed, dashspeed;

    public Sprite line, target;


    void Start()
    {
        indicateDash.line = line;
        indicateDash.target = target;
        StartCoroutine(idle());
    }

    private void Update()
    {
        if (!dashingShort && !dashingLong && !throughDashing && !phased)
        {
            
            if ((this.gameObject.transform.position - GameObject.FindGameObjectWithTag("player").transform.position).magnitude < 3)
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                StopAllCoroutines();
                StartCoroutine(dashShort());
            }
            if ((this.gameObject.transform.position - GameObject.FindGameObjectWithTag("player").transform.position).magnitude > 11f)
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                StopAllCoroutines();
                StartCoroutine(dashLong());
            }
            
        }
    }

    public IEnumerator idle()
    {
        yield return new WaitForSeconds(1);
        dashingLong = false;
        dashingShort = false;
        throughDashing = false;
        phased = false;

        List<IEnumerator> actions = new()
        {
            barrage(),
            snipe(),
            orbit(),
            barrage(),
            snipe(),
            orbit(),
            throughDash(),
            barrage(),
            snipe(),
            orbit(),
            throughDash(),
            bulletHell(phase),
            bulletHell(phase)
        };

        int r = (int)Random.Range(0, actions.Count);
        StartCoroutine(actions[r]);
    }

    public IEnumerator bulletHell(int difficulty)
    {
        phased = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 0.5f);

        Transform player = GameObject.FindGameObjectWithTag("player").transform;

        for (int i = 0; i < 8 + 2* difficulty; i++)
        {
            enemyUtils.projectile(25, 3.5f * Vector3.down, proj, 10, player.position + new Vector3(Random.Range(-9.5f, 9.5f), 6, 0));
            enemyUtils.projectile(25, 3.5f * Vector3.down, proj, 10, player.position + new Vector3(Random.Range(-9.5f, 9.5f), 6, 0));
            if (difficulty == 2)
            {
                enemyUtils.projectile(25, 3.5f * Vector3.down, proj, 10, this.transform.position);
                enemyUtils.projectile(25, 3.5f * Vector3.down, proj, 10, this.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
            enemyUtils.projectile(25, 3.5f * Vector3.up, proj, 10, player.position + new Vector3(Random.Range(-9.5f, 9.5f), -6, 0));
            enemyUtils.projectile(25, 3.5f * Vector3.up, proj, 10, player.position + new Vector3(Random.Range(-9.5f, 9.5f), -6, 0));
            if (difficulty == 2)
            {
                enemyUtils.projectile(25, 3.5f * Vector3.up, proj, 10, this.transform.position);
                enemyUtils.projectile(25, 3.5f * Vector3.up, proj, 10, this.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
            enemyUtils.projectile(25, 3.5f * Vector3.left, proj, 10, player.position + new Vector3(10, Random.Range(-5, 5), 0));
            enemyUtils.projectile(25, 3.5f * Vector3.left, proj, 10, player.position + new Vector3(10, Random.Range(-5, 5), 0));
            if (difficulty == 2)
            {
                enemyUtils.projectile(25, 3.5f * Vector3.left, proj, 10, this.transform.position);
                enemyUtils.projectile(25, 3.5f * Vector3.left, proj, 10, this.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
            enemyUtils.projectile(25, 3.5f * Vector3.right, proj, 10, player.position + new Vector3(-10, Random.Range(-5, 5), 0));
            enemyUtils.projectile(25, 3.5f * Vector3.right, proj, 10, player.position + new Vector3(-10, Random.Range(-5, 5), 0));
            if (difficulty == 2)
            {
                enemyUtils.projectile(25, 3.5f * Vector3.right, proj, 10, this.transform.position);
                enemyUtils.projectile(25, 3.5f * Vector3.right, proj, 10, this.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(1);
        phased = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(idle());
    }

    public IEnumerator throughDash()
    {
        throughDashing = true;
        Vector3 p1 = GameObject.FindGameObjectWithTag("player").transform.position;
        Vector3 target = 2 * p1 - this.gameObject.transform.position;
        indicateDash d = indicateDash.create(target, this.gameObject.transform.position);
        yield return new WaitForSeconds(1);
        StartCoroutine(dashTo(target, 0.25f));
        d.delete();
        for (int i = 0; i < 5; i++)
        {
            enemyUtils.projectile(25, 5 * Vector3.Cross(this.gameObject.GetComponent<Rigidbody2D>().linearVelocity, new Vector3(0, 0, 1)).normalized, proj, 2, this.transform.position);
            enemyUtils.projectile(25, -5 * Vector3.Cross(this.gameObject.GetComponent<Rigidbody2D>().linearVelocity, new Vector3(0, 0, 1)).normalized, proj, 2, this.transform.position);
            yield return new WaitForSeconds(0.2f * (target - this.gameObject.transform.position).magnitude / dashspeed);

        }
        throughDashing = false;
        StartCoroutine(idle());
    }
    public IEnumerator orbit()
    {
        StartCoroutine(moveTo(GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-5, 0, 0) : new Vector3(5, 0, 0)), 0.75f));
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(orbitMovement(GameObject.FindGameObjectWithTag("player"), 2, 5, this.gameObject.transform.position));
        for(int i  = 0; i< 10; i++)
        {
            yield return new WaitForSeconds(0.3f);
            enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        }
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        StartCoroutine(dashShort());

    }
    public IEnumerator orbitMovement(GameObject target, float time, float distance, Vector3 origin)
    {
        Debug.Log("orbiting");
        float angleDeg = 0;
        float direction = ((int)Random.Range(0, 1.99f)) * 2 - 1;

        float OGangle = polarAngle(origin - target.transform.position);

        GameObject orbitPoint = new GameObject();
        orbitPoint.transform.position = ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-5, 0, 0) : new Vector3(5, 0, 0));
        while (angleDeg * direction < 180)
        {
            angleDeg += direction * 180 * Time.deltaTime / time;
            orbitPoint.transform.position = target.transform.position + fromPolar(distance, angleDeg + OGangle);
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Mathf.PI * distance / time * (orbitPoint.transform.position - this.gameObject.transform.position).normalized;
            yield return new WaitForFixedUpdate();
        }
        Destroy(orbitPoint);


        Debug.Log("orbit stop");

    }
    public IEnumerator barrage()
    {
        StartCoroutine(moveTo(GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.y - this.gameObject.transform.position.y > 0) ? new Vector3(0, -5, 0) : new Vector3(0, 5, 0)), 1));
        yield return new WaitForSeconds((GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.y - this.gameObject.transform.position.y > 0) ? new Vector3(0, -5, 0) : new Vector3(0, 5, 0)) - this.gameObject.transform.position).magnitude / speed);
        float dist = Random.Range(1, 5);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(-dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        StartCoroutine(moveTo(new Vector3(GameObject.FindGameObjectWithTag("player").transform.position.x, this.transform.position.y, 0), 0.5f));
        yield return new WaitForSeconds((new Vector3(GameObject.FindGameObjectWithTag("player").transform.position.x, this.transform.position.y, 0) - this.gameObject.transform.position).magnitude / speed);
        dist = Random.Range(1, 5);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.5f);
        dist = Random.Range(1, 5);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(dist, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        StartCoroutine(idle());
    }
    public IEnumerator dashLong()
    {
        dashingLong = true;
        yield return new WaitForSeconds(0.5f);
        
        Vector3 p1 = GameObject.FindGameObjectWithTag("player").transform.position;
        indicateDash d = indicateDash.create(p1, this.gameObject.transform.position);
        yield return new WaitForSeconds(1);
        StartCoroutine(dashTo(p1, 0.25f));
        d.delete();
        yield return new WaitForSeconds((p1- this.gameObject.transform.position).magnitude / dashspeed);
        enemyUtils.projectile(25, 5 * Vector2.up, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.down, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.left, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.right, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.up + Vector2.right).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.up + Vector2.left).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.down + Vector2.right).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.down + Vector2.left).normalized, proj, 2, this.transform.position);
        dashingLong = false;
        StartCoroutine(idle());        
    }
    public IEnumerator snipe()
    {
        StartCoroutine(moveTo(GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-7, 0, 0) : new Vector3(7, 0, 0)), 1));
        yield return new WaitForSeconds((GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-7, 0, 0) : new Vector3(7, 0, 0)) - this.gameObject.transform.position).magnitude / speed);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.1f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 6, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.1f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 7, proj, 2, this.transform.position);
        StartCoroutine(moveTo(new Vector3(this.gameObject.transform.position.x, GameObject.FindGameObjectWithTag("player").transform.position.y), 0.5f));
        yield return new WaitForSeconds((new Vector3(this.gameObject.transform.position.x, GameObject.FindGameObjectWithTag("player").transform.position.y) - this.transform.position).magnitude / speed);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.1f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 6, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.1f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 7, proj, 2, this.transform.position);
        StartCoroutine(idle());
    }
    public IEnumerator dashShort()
    {
        dashingShort = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.75f);
        Vector3 p1 = GameObject.FindGameObjectWithTag("player").transform.position;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(dashTo(2f * p1 - this.gameObject.transform.position, 0.3f));
        yield return new WaitForSeconds((2f * p1 - this.gameObject.transform.position).magnitude/dashspeed);
        Vector3 p2 = GameObject.FindGameObjectWithTag("player").transform.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(dashTo(2.5f * p2 - 1.5f * this.gameObject.transform.position, 0.4f));
        yield return new WaitForSeconds((2.5f * p2 - 1.5f * this.gameObject.transform.position).magnitude/dashspeed);
        StartCoroutine(idle());
        StartCoroutine(stopDashAfter(1));
    }
    public IEnumerator stopDashAfter(float time)
    {
        yield return new WaitForSeconds(time);
        dashingShort = false;
        dashingLong = false;
        throughDashing = false;
    }
    public IEnumerator moveTo(Vector3 pos, float time)
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (pos - this.transform.position).normalized * speed;
        yield return new WaitForSeconds((pos - this.transform.position).magnitude / speed);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
    public IEnumerator dashTo(Vector3 pos, float time)
    {
        /*
        float timeTotal = (pos - this.gameObject.transform.position).magnitude / dashspeed;
        float timeCur = 0;
        */

        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (pos - this.transform.position).normalized * dashspeed;
        yield return new WaitForSeconds((pos - this.transform.position).magnitude / dashspeed);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    public static Vector3 fromPolar(float magnitude, float angle)
    {
        return new Vector3(magnitude * Mathf.Cos(angle * Mathf.Deg2Rad), magnitude * Mathf.Sin(angle * Mathf.Deg2Rad));
    }
    public static float polarAngle(Vector3 v)
    {
        return (float)(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<realcombatController>().takeDamage(10);
            StopAllCoroutines();
            StartCoroutine(idle());
            StartCoroutine(stopDashAfter(1));
            StartCoroutine(moveTo(-collision.transform.position + 2 * this.gameObject.transform.position, 0.5f));
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            StopAllCoroutines();
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            StartCoroutine(stopDashAfter(1));
            if (!throughDashing)
            {
                StartCoroutine(throughDash());
            }
            else
            {
                StartCoroutine(dashLong());
            }

            
        }
    }
}
