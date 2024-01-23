using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 15f;
    [SerializeField] private float horizontalSpeed = 7.5f;
    [SerializeField] private float rotationSpeed = 10f;
    private float newY;

    private void Update()
    {
        VerticalMovement();
        HorizontalMovement();
        Rotation();
    }

    private void VerticalMovement()
    {
        newY = -Input.GetAxis("Mouse ScrollWheel") * 100 * verticalSpeed * Time.deltaTime;

        if (transform.position.y + newY > 5f && transform.position.y + newY < 25f)
            transform.position += new Vector3(0, newY, 0);
    }
    private void HorizontalMovement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 0, horizontalSpeed * Time.deltaTime * 5f);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, 0, -horizontalSpeed * Time.deltaTime * 5f);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-horizontalSpeed * Time.deltaTime * 5f, 0, 0);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(horizontalSpeed * Time.deltaTime * 5f, 0, 0);
    }
    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.eulerAngles += new Vector3(0, -rotationSpeed * Time.deltaTime * 10f, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime * 10f, 0);
        }
    }

}
