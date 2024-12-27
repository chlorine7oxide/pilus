using System.Collections;
using UnityEngine;

public class lootableBox : overworldInteractable
{
    public Sprite portrait;
    public Sprite face;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText g = generalText.create("Unboxing!", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.changeText("...");
        yield return new WaitUntil(() => g.done);
        g.changeText("It's just filled with dust...");
        yield return new WaitUntil(() => g.done);
        g.destroy();
        Destroy(this.gameObject);
    }
}
