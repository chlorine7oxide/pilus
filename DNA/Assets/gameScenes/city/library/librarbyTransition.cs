using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class librarbyTransition : overworldInteractable
{
    public Vector3 pos;

    public override void interact()
    {
        StartCoroutine(transition());
    }

    public IEnumerator transition()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("library");
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("player") is not null);
        GameObject.FindGameObjectWithTag("player").transform.position = pos;
        Destroy(this.gameObject);
    }
}
