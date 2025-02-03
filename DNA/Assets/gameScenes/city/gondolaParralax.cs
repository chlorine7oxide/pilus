using UnityEngine;

public class gondolaParralax : MonoBehaviour
{
    public GameObject mc;

    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, mc.transform.position.y-3, 0);
    }
}
