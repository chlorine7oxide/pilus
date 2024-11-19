using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;

public class entitySelector : MonoBehaviour
{
    public static combatEntity selectedEntity = null;

    public Dictionary<int, combatEntity> entities = new Dictionary<int, combatEntity>();
    public int position;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            position++;
            position %= entities.Count;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            position--;
            if (position < 0)
            {
                position += entities.Count;
            }
            position %= entities.Count;
        }

        this.gameObject.transform.position = entities[position].entity.transform.position;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            selectedEntity = entities[position];
        }
    }
}


// forward is right and down
// back is left and up