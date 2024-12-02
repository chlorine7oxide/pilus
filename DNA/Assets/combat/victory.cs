using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victory : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(combatData.scene);
            StartCoroutine(locate());
        }
    }

    public IEnumerator locate()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("player").transform.position = combatData.playerPos;
        Destroy(this.gameObject);
    }
}
