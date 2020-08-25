using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static IEnumerator SetActiveLater(GameObject go, bool isActive, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        go.SetActive(isActive);
    }
    
    
    
}
