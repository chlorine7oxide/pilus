using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lostTransition : MonoBehaviour
{
    public string direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            int rand = Random.Range(1, 3);
            direction = direction + rand.ToString();
            SceneManager.LoadScene(direction);
        }
    }
}
