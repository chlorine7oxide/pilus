using UnityEngine;

public abstract class overworldInteractable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.Z) && other.gameObject.CompareTag("player"))
        {
            interact();
        }
    }

    public abstract void interact();
}
