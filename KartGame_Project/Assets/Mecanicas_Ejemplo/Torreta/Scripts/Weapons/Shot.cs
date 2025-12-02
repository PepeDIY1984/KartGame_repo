using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform shotInitPoint;
    public GameObject bullet;
    public AudioSource shotSound;
    public float shotForce = 1000f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            //GameManager.Instance.gunAmmo--;
            GameObject newBullet = Instantiate(bullet, shotInitPoint.position, shotInitPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(shotInitPoint.forward * shotForce);
            Destroy(newBullet, 5);
            shotSound.Play();
            //GameManager.Instance.textAmmo.text = GameManager.Instance.gunAmmo.ToString();
        }
    }
}
