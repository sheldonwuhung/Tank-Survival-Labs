using System.Collections;
using UnityEngine;
public class General : MonoBehaviour
{
    public float GetAngleFromPos(Vector2 to)
    {
        return Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg;
    }
    
    public float GetAngleBetweenTwoVectors(Vector2 from, Vector2 to)
    {
        Vector2 direction = to - from;
        return GetAngleFromPos(direction);
    }

    public void Delayed(float delay)
    {
        StartCoroutine(DelayedIE(delay));
    }
    public System.Collections.IEnumerator DelayedIE(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
