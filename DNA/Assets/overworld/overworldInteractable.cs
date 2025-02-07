using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class overworldInteractable : MonoBehaviour
{
    public bool interactable = false;

    public static bool talkable = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            interactable = true;
            talkable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            interactable = false;
            talkable = false;
        }
    }

    protected virtual void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.Z))
        {
            GameObject g = GameObject.FindGameObjectWithTag("dialogue");
            GameObject h = GameObject.FindGameObjectWithTag("inv");
            GameObject b = GameObject.FindGameObjectWithTag("book");
            if (b is not null)
            {
                if (b.GetComponent<bookSelector>().active)
                {
                    return;
                }
            }
            if (g is null && !GameObject.FindGameObjectWithTag("inv").GetComponent<inventoryController>().active)
            {
                interact();
            }
        }
    }

    public abstract void interact();
}
