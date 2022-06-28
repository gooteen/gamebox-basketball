using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 100f;
    [SerializeField] private Transform playerBody;

    [SerializeField] private bool isPickable;

    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycastLayers;
    private float rotX = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        isPickable = Physics.Raycast(transform.position, transform.forward, raycastDistance, raycastLayers); 
    }

    void Update()
    {
        
        float mouseInputX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseInputY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        rotX -= mouseInputY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseInputX);
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
    }

    public bool CheckIfPickable()
    {
        return isPickable;
    }
}
