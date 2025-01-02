using UnityEngine;

public class populate : MonoBehaviour
{
    public GameObject canvas;

    public Sprite crowTop, crowIdle, crowSide;

    private void Start()
    {
        generalText.canvas = canvas;

        combatController.crowTop = crowTop;
        combatController.crowIdle = crowIdle;
        combatController.crowSide = crowSide;

    }
}
