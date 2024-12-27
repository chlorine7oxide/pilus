using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class oldComputer : overworldInteractable
{
    public Sprite portrait;
    public Sprite face;

    public override void interact()
    {
        if (!playerData.items.Contains("Old Computer"))
        {
            StartCoroutine(text());
        }
    }

    public IEnumerator text()
    {
        generalText g = generalText.create("It's an old broken computer.", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.changeText("It might be able to be fixed, so I think I'll take it");
        yield return new WaitUntil(() => g.done);
        g.destroy();
        playerData.items.Add("Old Computer");
        Destroy(this.gameObject);
    }
}
