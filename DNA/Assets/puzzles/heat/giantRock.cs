using UnityEngine;

public class giantRock : MonoBehaviour
{
    void Update()
    {
        if (!this.gameObject.GetComponent<carryableItem>().isHeld)
        {
            Vector3 pos = this.gameObject.transform.position;
            pos.x = Mathf.Round(pos.x * 2) / 2;
            pos.y = Mathf.Round(pos.y * 2) / 2;
            this.gameObject.transform.position = pos;
        }
    }
}
