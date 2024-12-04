using UnityEngine;

public class indicateDash : MonoBehaviour
{
    public static Sprite line, target;
    public GameObject lineObj, targetObj;

    public static indicateDash create(Vector3 target, Vector3 origin)
    {
        Vector3 mid = (target + origin) / 2;
        GameObject line = new();
        line.AddComponent<SpriteRenderer>().sprite = indicateDash.line;
        line.GetComponent<SpriteRenderer>().sortingOrder = 8;
        line.transform.position = mid;
        float angle = Mathf.Atan2(target.y - origin.y, target.x - origin.x) * Mathf.Rad2Deg;
        line.transform.rotation = Quaternion.Euler(0, 0, angle);
        line.transform.localScale = new Vector3((target - origin).magnitude, 0.1f, 1);
        GameObject targ = new();
        targ.AddComponent<SpriteRenderer>().sprite = indicateDash.target;
        targ.GetComponent<SpriteRenderer>().sortingOrder = 9;
        targ.transform.position = target;
        line.AddComponent<indicateDash>();
        line.GetComponent<indicateDash>().lineObj = line;
        line.GetComponent<indicateDash>().targetObj = targ;

        return line.GetComponent<indicateDash>();
    }

    public void delete()
    {
        Destroy(targetObj);
        Destroy(lineObj);
    }
}
