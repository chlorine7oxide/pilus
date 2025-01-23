using UnityEngine;

public class frontDesk : overworldInteractable
{
    public GameObject bookUI, cameraObj;

    public override void interact()
    {
        if (!bookUI.GetComponent<bookSelector>().active)
        {
            bookUI.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
            StartCoroutine(bookUI.GetComponent<bookSelector>().select());
        }
    }
}
