using System.Collections;
using UnityEngine;

public class buttonText : overworldInteractable
{
    public Sprite portrait;
    public Sprite face;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText g = generalText.create("A conveniently shaped hole in the ice.", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.destroy();
    }
}
