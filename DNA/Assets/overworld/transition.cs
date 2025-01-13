using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{
    public string SceneName;
    public Vector3 playerPos;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            transitionAction();
        }
    }

    public IEnumerator transitionScene()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(SceneName);
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("player") != null);
        yield return new WaitForEndOfFrame();
        GameObject.FindGameObjectWithTag("player").transform.position = playerPos;
        Destroy(this.gameObject);
    }

    public virtual void transitionAction()
    {
        StartCoroutine(transitionScene());
    }
}
