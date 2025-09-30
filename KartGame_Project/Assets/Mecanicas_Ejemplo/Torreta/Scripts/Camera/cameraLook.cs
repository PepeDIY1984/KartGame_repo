using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook : MonoBehaviour
{
    public float mouseSense = 80f;
    public GameObject player;
    public float clock = 0;
    public Transform lookAtTarget;

    float verticalRotation = 0;
    float mouseX;
    float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //El raton no se escapa de la vista "game"
        transform.localRotation = Quaternion.Euler(0, 0, 0); // Asignar giro vertical a la camara NO al gameobject del player
    }
    void Update()
    {
         mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;// Capturar giro en horizontal con el movimiento del raton * sensibilidad personalizada * Tiempo estandarizado de Unity
         mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;// Capturar giro en vertical con el movimiento del raton * sensibilidad personalizada * Tiempo estandarizado de Unity
        player.GetComponent<Transform>().Rotate(Vector3.up * mouseX);// Asignar giro horizontal

        verticalRotation -= mouseY;
        clock += Time.deltaTime;
        if (clock < 1)
        {
            verticalRotation = 0;
            player.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0); 

        }
        //Debug.Log(clock);
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limitacion de giro vertical
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0); // Asignar giro vertical a la camara NO al gameobject del player
    }
}





