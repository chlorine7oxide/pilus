using UnityEngine;

public class decisionController : MonoBehaviour
{
    public decision d;
    public Vector3 pos;
    public int place = 0;
    public GameObject text2, text;

    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 13;
    }

    void Update()
    {
        if (place == 0)
        {
            this.gameObject.transform.position = text.transform.position;
        }
        else
        {
            this.gameObject.transform.position = text2.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            d.endDecision(place == 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            place = 1 - place;
        }
        text2.transform.position = text.transform.position - new Vector3(0, 0.5f, 0);
    }
}
