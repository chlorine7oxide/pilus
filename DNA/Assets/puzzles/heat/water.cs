using UnityEngine;
using UnityEngine.Tilemaps;

public class water : MonoBehaviour
{
    void Update()
    {
        switch (heatController.heat)
        {
            case < 0:
                this.gameObject.GetComponent<Tilemap>().color = Color.white;
                this.gameObject.GetComponent<Rigidbody2D>().excludeLayers = 1 << 3 | 1 << 7;
                break;
            case 0:
                this.gameObject.GetComponent<Tilemap>().color = Color.blue;
                this.gameObject.GetComponent<Rigidbody2D>().excludeLayers = 1 << 7;
                break;
            case > 0:
                this.gameObject.GetComponent<Tilemap>().color = Color.red;
                this.gameObject.GetComponent<Rigidbody2D>().excludeLayers = 1 << 7;
                break;
        }
    }
}
