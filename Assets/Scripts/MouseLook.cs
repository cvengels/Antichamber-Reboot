using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000f;

    public Transform playerBody;

    RaycastHit hit;
    Ray forwardRay;
    public Transform previewCube;

    public TextMeshProUGUI xAngleTextUI;
    public TextMeshProUGUI yAngleTextUI;
    public TextMeshProUGUI zAngleTextUI;

    float xRotation = 0f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Preview Cube
        forwardRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(forwardRay, out hit) && Input.GetMouseButton(0))
        {
            xAngleTextUI.text = "X: " + (int)hit.normal.x;
            yAngleTextUI.text = "Y: " + (int)hit.normal.y;
            zAngleTextUI.text = "Z: " + (int)hit.normal.z;

            Debug.Log(hit.point.ToString());
            previewCube.position = new Vector3( (Mathf.Round(hit.point.x / 0.2f) * 0.2f) + ((int)hit.normal.x * previewCube.transform.localScale.x / 2), 
                                                (Mathf.Round(hit.point.y / 0.2f) * 0.2f) + ((int)hit.normal.y * previewCube.transform.localScale.y / 2), 
                                                (Mathf.Round(hit.point.z / 0.2f) * 0.2f) + ((int)hit.normal.z * previewCube.transform.localScale.z / 2));
        }
    }
}
