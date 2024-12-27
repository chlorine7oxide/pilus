using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class houseExit : MonoBehaviour
{
    public string sceneName;
    public Vector3 playerPosition;

    public bool contact = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            contact = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            contact = false;
        }
    }

    public void Update()
    {
        if (contact && Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(loadScene());
        }
    }

    public IEnumerator loadScene()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        GameObject player = GameObject.FindGameObjectWithTag("player");
        player.transform.position = playerPosition;
        Destroy(this.gameObject);
    }
}
