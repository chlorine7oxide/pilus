using System.Collections;
using UnityEngine;

public class drinkStall : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
        
        
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("This market stall looks like it used to sell drinks.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);

        if (!playerData.items.Contains("Unlabeled drink"))
        {
            t.changeText("Just what I needed right now!");
            yield return new WaitUntil(() => t.done);
            playerData.items.Add("Unlabeled drink");
            playerData.items.Add("Unlabeled drink");
        }
        
        t.destroy();
        
    }
}




