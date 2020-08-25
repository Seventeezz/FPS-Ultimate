using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    // public Transform tipTarget;
    // public Transform rootTarget;
    
    private float xRotation = 0f;    //上下旋转的角度

    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //左右看，旋转整个player
        playerBody.Rotate(Vector3.up * mouseX);
        //上下看，旋转摄像机
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // tipTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // rootTarget.localRotation = Quaternion.Euler(xRotation / 5f, 0f, 0f);
        
    }

    public IEnumerator ApplyScreenShake(float horizontalRecoilForce, float verticalRecoilForce)
    {
        float maxTime = 0.1f;
        int count = 1;
        int smoothLevel = 5;
        while (count <= smoothLevel)
        {
            playerBody.Rotate(Vector3.up * horizontalRecoilForce / smoothLevel);
            xRotation -= verticalRecoilForce / smoothLevel;
            yield return new WaitForSeconds(maxTime / smoothLevel);
            count++;
        }
    }
    
    
    
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position, transform.position + transform.forward * 50 );
    // }
}
