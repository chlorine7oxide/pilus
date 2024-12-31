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
            print(combatData.scene);
            SceneManager.LoadScene(combatData.scene);
            StartCoroutine(locate());
        }
    }

    public IEnumerator locate()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("player") != null);
        GameObject.FindGameObjectWithTag("player").transform.position = combatData.playerPos;
        Destroy(this.gameObject);
    }
}
