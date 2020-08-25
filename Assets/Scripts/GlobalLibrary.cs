using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLibrary : MonoBehaviour
{

    public static GlobalLibrary instance;
    public GameObject player;
    
    private void Awake()
    {
        instance = this;
    }
    
    [SerializeField]
    private Canvas canvas = null;

    public float GetCanvasScaleFactor()
    {
        return canvas.scaleFactor;
    }

}
