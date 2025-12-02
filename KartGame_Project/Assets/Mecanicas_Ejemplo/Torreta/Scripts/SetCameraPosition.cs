using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    public Transform Player;
    public float coordx;
    public float coordy;
    public float coordz;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(coordx, coordy, coordz) + Player.transform.position;
    }
}
