using UnityEditor;
using UnityEngine;

public class vaultTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            GameObject.Find("vaultController").GetComponent<vaultContorller>().threshold += 9;
            Destroy(this.gameObject);
        }
    }
}
