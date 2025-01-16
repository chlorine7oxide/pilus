using System.Collections;
using UnityEngine;

public class itemBarrel : overworldInteractable
{
    public Sprite portrait;
    public bool looted = false;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An old barrel.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        if (!looted)
        {
            if (Random.Range(0, 2) < 1)
            {
                t.changeText("Inside, there's an unlabeled drink, it looks safe!");
                playerData.items.Add("Unlabeled drink");
                yield return new WaitUntil(() => t.done);
            }
            else
            {
                t.changeText("Inside, there's some old rations, they look safe!");
                playerData.items.Add("Old rations");
                yield return new WaitUntil(() => t.done);
            }
            looted = true;
        }
        else
        {
            t.changeText("Refreshments!");
            yield return new WaitUntil(() => t.done);
        }
        t.destroy();
    }
}
