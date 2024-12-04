using System.Collections;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;

public class testEnemyOverworld : MonoBehaviour
{
    public Sprite proj;
    public bool dashingShort = false;
    public bool dashingLong = false;

    public Sprite line, target;


    void Start()
    {
        indicateDash.line = line;
        indicateDash.target = target;
        StartCoroutine(idle());
    }

    private void Update()
    {
        if (!dashingShort && !dashingLong)
        {
            
            if ((this.gameObject.transform.position - GameObject.FindGameObjectWithTag("player").transform.position).magnitude < 3)
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                StopAllCoroutines();
                StartCoroutine(dashShort());
            }
            if ((this.gameObject.transform.position - GameObject.FindGameObjectWithTag("player").transform.position).magnitude > 8.8f)
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
        
        
        float r = (int)Random.Range(0, 2.99f);
        switch (r)
        {
            case 0:
                StartCoroutine(barrage());
                break;
            case 1:
                StartCoroutine(snipe());
                break;
            case 2:
                StartCoroutine(orbit());
                break;
        }

    }

    public IEnumerator orbit()
    {
        StartCoroutine(moveTo(GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-5, 0, 0) : new Vector3(5, 0, 0)), 0.75f));
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(orbitMovement(GameObject.FindGameObjectWithTag("player"), 3, 5, this.gameObject.transform.position));
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
        yield return new WaitForSeconds(1);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(3f, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(-3f, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        StartCoroutine(moveTo(new Vector3(GameObject.FindGameObjectWithTag("player").transform.position.x, this.transform.position.y, 0), 0.5f));
        yield return new WaitForSeconds(0.5f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(3f, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position + new Vector3(-3f, 0, 0)).normalized * 5, proj, 2, this.transform.position);
        StartCoroutine(idle());
    }
    public IEnumerator dashLong()
    {
        dashingLong = true;
        yield return new WaitForSeconds(0.5f);
        
        Vector3 p1 = GameObject.FindGameObjectWithTag("player").transform.position;
        indicateDash d = indicateDash.create(p1, this.gameObject.transform.position);
        yield return new WaitForSeconds(1);
        StartCoroutine(moveTo(p1, 0.25f));
        d.delete();
        yield return new WaitForSeconds(0.25f);
        enemyUtils.projectile(25, 5 * Vector2.up, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.down, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.left, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * Vector2.right, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.up + Vector2.right).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.up + Vector2.left).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.down + Vector2.right).normalized, proj, 2, this.transform.position);
        enemyUtils.projectile(25, 5 * (Vector2.down + Vector2.left).normalized, proj, 2, this.transform.position);
        StartCoroutine(idle());
        yield return new WaitForSeconds(1f);
        dashingLong = false;
    }
    public IEnumerator snipe()
    {
        StartCoroutine(moveTo(GameObject.FindGameObjectWithTag("player").transform.position + ((GameObject.FindGameObjectWithTag("player").transform.position.x - this.gameObject.transform.position.x > 0) ? new Vector3(-7, 0, 0) : new Vector3(7, 0, 0)), 1));
        yield return new WaitForSeconds(1);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.2f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
        yield return new WaitForSeconds(0.2f);
        enemyUtils.projectile(25, (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position).normalized * 5, proj, 2, this.transform.position);
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
        StartCoroutine(moveTo(2f * p1 - this.gameObject.transform.position, 0.3f));
        yield return new WaitForSeconds(0.4f);
        Vector3 p2 = GameObject.FindGameObjectWithTag("player").transform.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(moveTo(2.5f * p2 - 1.5f * this.gameObject.transform.position, 0.4f));
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(idle());
        StartCoroutine(stopDashAfter(1));
    }
    public IEnumerator stopDashAfter(float time)
    {
        yield return new WaitForSeconds(time);
        dashingShort = false;
        dashingLong = true;
    }
    public IEnumerator moveTo(Vector3 pos, float time)
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (pos - this.transform.position) / time;
        yield return new WaitForSeconds(time);
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
            dashingShort = false;
            StartCoroutine(moveTo(-collision.transform.position + 2 * this.gameObject.transform.position, 0.5f));
        }
    }
}
