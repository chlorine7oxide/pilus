using System.Collections;
using UnityEngine;

public class deskKey : overworldInteractable
{
    public bool key = true;
    public GameObject keyObj;
    public Sprite portrait;
    public Sprite face;

    public override void interact()
    {
        if (!playerData.items.Contains("Desk Key"))
        {
            playerData.items.Add("Desk Key");
            StartCoroutine(text());
        }
    }

    public IEnumerator text()
    {
        if (key)
        {
            key = false;
            generalText g = generalText.create("There's a small key sitting on the desk.", portrait, face);
            if (g == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => g.done);
            g.changeText("Probably a good idea to take it.");
            yield return new WaitUntil(() => g.done);
            g.destroy();
            Destroy(keyObj);
        }
        
    }
}
