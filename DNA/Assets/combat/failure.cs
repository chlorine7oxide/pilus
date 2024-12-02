using UnityEngine;
using UnityEngine.SceneManagement;

public class failure : MonoBehaviour
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
            GameObject.FindGameObjectWithTag("player").transform.position = combatData.playerPos;
            Destroy(this.gameObject);
        }
    }
}
