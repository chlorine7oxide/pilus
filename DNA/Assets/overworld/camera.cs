using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    public float maxY = 1000;
    public float minY = -1000;
    public float maxX = 1000;
    public float minX = -1000;

    void Update()
    {
        this.gameObject.transform.position = new Vector3(Mathf.Min(Mathf.Max(player.transform.position.x, minX), maxX), Mathf.Min(Mathf.Max(player.transform.position.y, minY), maxY), -10);
    }
}
