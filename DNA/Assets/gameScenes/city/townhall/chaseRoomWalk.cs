using System.Collections;
using UnityEngine;

public class chaseRoomWalk : MonoBehaviour
{    
    void Start()
    {
        StartCoroutine(walk());
    }

    public GameObject guard;

    public Sprite[] up, down, side;

    public float speed;

    public Sprite guardmask;

    public IEnumerator walk()
    {
        int animProgress = 0;
        guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];

        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().flipX = false;
        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }
        for (int i = 0; i < (10 / speed); i++)
        {
            guard.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        for (int i = 0; i < (6 / speed); i++)
        {
            guard.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }
    }
}
