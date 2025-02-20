using UnityEngine;

public class chaseUp : MonoBehaviour
{
    public GameObject enemy;
    public bool done = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && !done && GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody2D>().linearVelocityY < 0)
        {
            done = true;
            StartCoroutine(enemy.GetComponent<chaseEnemy>().chaseUp());
        }
    }
}
