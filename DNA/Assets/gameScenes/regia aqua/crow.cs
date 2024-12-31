using UnityEngine;
using UnityEngine.SceneManagement;

public class crow : MonoBehaviour
{
    public bool entered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (!entered)
            {
                entered = true;
                combatData.playerPos = GameObject.FindGameObjectWithTag("player").transform.position;
                combatData.scene = SceneManager.GetActiveScene().name;
                combatStarter combatStarter = new combatStarter(2, 2, new string[] { "crow", "boulder" }, this.gameObject);
            }
        }
    }
}
