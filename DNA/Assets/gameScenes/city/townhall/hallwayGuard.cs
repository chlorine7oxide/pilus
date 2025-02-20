using System.Collections;
using UnityEngine;

public class hallwayGuard : MonoBehaviour
{
    public GameObject textPrefabUI, canvas;
    public Sprite box;

    void Start()
    {
        generalText.textPrefabUI = textPrefabUI;
        generalText.canvas = canvas;
        generalText.box = box;

        
        if (playerData.guard2stage == 1)
        {
            StartCoroutine(walk());
            StartCoroutine(text());
        }
        else
        {
            Destroy(guard);
            Destroy(barrier1);
            Destroy(barrier2);
        }
        
    }

    public GameObject guard, barrier1, barrier2;

    public Sprite[] up, down, side;

    public float speed;

    public Sprite guardmask;

    public IEnumerator walk()
    {
        int animProgress = 0;
        guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];

        for (int i = 0; i < (13 / speed); i++)
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
        for (int i = 0; i < (3 / speed); i++)
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
        Destroy(guard);
        playerData.guard2stage = 2;
    }
    public IEnumerator text()
    {
        generalText t = generalText.create("Just this way.", guardmask, null);

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }
}
