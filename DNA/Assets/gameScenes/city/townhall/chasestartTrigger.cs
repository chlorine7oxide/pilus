using UnityEngine;

public class chasestartTrigger : MonoBehaviour
{
    public GameObject enemy;
    public bool done = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && !done)
        {
            done = true;
            enemy.SetActive(true);
            StartCoroutine(enemy.GetComponent<chaseEnemy>().chase());
        }
    }
}
