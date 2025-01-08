using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meetingRoom : MonoBehaviour
{
    public Sprite portrait;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (playerData.items.Contains("FishKey"))
            {
                SceneManager.LoadScene("meetingRoom");
            }
            else
            {
                StartCoroutine(text());
            }
        }
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("The door is locked, and I don't have the key.", portrait, null);
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
