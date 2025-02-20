using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class chaseEnemy : MonoBehaviour
{
    public Sprite[] side, up, down;

    public float speed;

    public IEnumerator chase()
    {
        this.gameObject.transform.position = new Vector3(3.5f, 2, 0);

        int animProgress = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = side[animProgress];
        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        for (int i = 0; i < (1.5f / speed); i++)
        {
            this.gameObject.transform.Translate(speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.transform.position = new Vector3(1000, 0, 0);

        yield break;
    }

    public IEnumerator chaseBack() {

        this.gameObject.transform.position = new Vector3(9, -10f, 0);

        int animProgress = 0;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = side[animProgress];
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitForSeconds(0.5f);

        int i = 0;

        while (GameObject.FindGameObjectWithTag("player").transform.position.x + 5 > this.gameObject.transform.position.x)
        {
            i++;
            this.gameObject.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.transform.position = new Vector3(1000, 0, 0);

        yield break;
    }

    public IEnumerator chaseUp()
    {

        this.gameObject.transform.position = new Vector3(23.5f, -4, 0);

        int animProgress = 0;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = up[animProgress];
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitForSeconds(0.5f);

        int i = 0;

        while (GameObject.FindGameObjectWithTag("player").transform.position.y - 5 < this.gameObject.transform.position.y)
        {
            i++;
            this.gameObject.transform.Translate(0, speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = up[animProgress];
            }
        }

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.transform.position = new Vector3(1000, 0, 0);

        yield break;
    }
}
