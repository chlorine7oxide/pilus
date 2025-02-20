using UnityEngine;

public class chaseBack : MonoBehaviour
{
    public GameObject enemy;
    public bool done = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && !done && GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody2D>().linearVelocityX > 0)
        {
            done = true;
            StartCoroutine(enemy.GetComponent<chaseEnemy>().chaseBack());
        }
    }
}
