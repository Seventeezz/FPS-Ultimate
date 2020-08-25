using System;
using System.Collections;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class ItemWUI : MonoBehaviour
{
	private Transform targetCamera;

	private void Start()
	{
		transform.parent.GetComponent<ItemManager>().onPickup += OnPickup;
		Debug.Assert(Camera.main != null, "Camera.main != null");
		targetCamera = Camera.main.transform;
	}

	public void LookAtCamera()
	{
		transform.LookAt(transform.position + targetCamera.forward, targetCamera.up);
	}

	public void OnPickup()
	{
		transform.gameObject.SetActive(false);
	}
	
}