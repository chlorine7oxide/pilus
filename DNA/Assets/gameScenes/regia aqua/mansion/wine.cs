using System.Collections;
using UnityEngine;

public class wine : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A bottle of wine.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It's so old its become...");
        yield return new WaitUntil(() => t.done);
        t.changeText("Aged?");
        yield return new WaitUntil(() => t.done);
        playerData.items.Add("\"Aged\" wine");
        Destroy(gameObject);
        t.destroy();
    }
}
