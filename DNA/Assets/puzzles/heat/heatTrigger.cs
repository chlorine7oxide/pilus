using UnityEngine;

public class heatTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            heatController.increaseHeat();
        }
    }
}
