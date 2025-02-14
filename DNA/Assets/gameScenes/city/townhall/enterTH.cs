using UnityEngine;
using UnityEngine.SceneManagement;

public class enterTH : overworldInteractable
{
    public override void interact()
    {
        SceneManager.LoadScene("townhall");
    }
}
