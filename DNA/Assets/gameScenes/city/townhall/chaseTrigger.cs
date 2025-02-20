using UnityEngine;

public class chaseTrigger : MonoBehaviour
{
    public GameObject chaseController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" && chaseController.gameObject.GetComponent<chaseRoomWalk>().ready)
        {
            StartCoroutine(chaseController.gameObject.GetComponent<chaseRoomWalk>().startChase());
        }
    }
}
