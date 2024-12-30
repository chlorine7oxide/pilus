using System.Collections;
using UnityEngine;

public class gaming : overworldInteractable
{
    public Sprite portrait;
    public Sprite portrait2;
    public Sprite face;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText g = generalText.create("This would've quite a machine in it's time.", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.destroy();
        g = generalText.create("I think I had one of these as a kid.", portrait2, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.changeText("The day it stopped working is probably still the saddest day of my life.");
        yield return new WaitUntil(() => g.done);
        g.destroy();

    }
}
