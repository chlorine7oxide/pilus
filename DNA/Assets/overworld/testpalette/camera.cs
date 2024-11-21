using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
