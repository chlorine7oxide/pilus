using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mayorOfficeEnter : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        if (playerData.items.Contains("Townhall Key"))
        {
            playerData.items.Remove("Townhall Key");
            playerData.townhallUnlock = true;
            StartCoroutine(unlock());
        }
        else if (playerData.townhallUnlock)
        {
            SceneManager.LoadScene("mayorOfficeHallway");
        }
        else
        {
            StartCoroutine(text());
        }
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("The door is locked.", portrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }

    public IEnumerator unlock()
    {
        generalText t = generalText.create("The key fits!", portrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.destroy();
        SceneManager.LoadScene("mayorOfficeHallway");
    }
}
