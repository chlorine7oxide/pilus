using UnityEngine;

public abstract class overworldInteractable : MonoBehaviour
{
    public bool interactable = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            interactable = false;
        }
    }

    protected virtual void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.Z) && !GameObject.FindGameObjectWithTag("inv").GetComponent<inventoryController>().active)
        {
            GameObject g = GameObject.FindGameObjectWithTag("dialogue");
            if (g is null)
            {
                interact();
            }
        }
    }

    public abstract void interact();
}
