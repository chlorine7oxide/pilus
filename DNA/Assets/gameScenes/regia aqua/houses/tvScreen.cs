using System.Collections;
using UnityEngine;

public class tvScreen : overworldInteractable
{

    public Sprite portrait;
    public Sprite face;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText g = generalText.create("It's an old TV.", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.changeText("There's no power outlets in sight.");
        yield return new WaitUntil(() => g.done);
        g.destroy();
    }
}
