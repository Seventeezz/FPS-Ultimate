using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetTransform : ScriptableObject
{
    [MenuItem("GameObject/Reset Transform #r")]
    public static void Reset()
    {
        var go = Selection.activeGameObject;
        if (go != null)
        {
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }
    }
 
}
