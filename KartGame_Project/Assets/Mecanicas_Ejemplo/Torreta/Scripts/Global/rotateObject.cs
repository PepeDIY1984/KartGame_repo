using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    public float speed = 0.03f;

    void Update()
    {
        transform.Rotate(Vector3.forward*speed);
    }
}
