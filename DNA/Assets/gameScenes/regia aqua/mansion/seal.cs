using System.Collections;
using UnityEngine;

public class seal : overworldInteractable
{
    public Sprite portrait, portrait2;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A red mayorial seal.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("This will definetely come in handy.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
        t = generalText.create("They actually still use these today.", portrait2, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("Could come in handy if we need some espionage.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
        playerData.items.Add("Red Seal");
        Destroy(gameObject);
    }
}
