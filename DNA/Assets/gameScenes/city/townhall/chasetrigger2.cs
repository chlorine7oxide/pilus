using UnityEngine;

public class chasetrigger2 : MonoBehaviour
{
    public GameObject chaseController;

    private void Start()
    {
        if (playerData.guard2stage == -1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "player" && playerData.chaseLockedDoorSeen)
        {
            StartCoroutine(chaseController.GetComponent<chaseRoomWalk>().returnHere());
            
        }
    }
}
