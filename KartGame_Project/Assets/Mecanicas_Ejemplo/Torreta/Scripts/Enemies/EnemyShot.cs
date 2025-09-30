using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemy_Bullet;
    public Transform initPoint_Bullet;
    public float speed_Bullet = 100;

    public Transform position_Player;
    public bool startShooting = false;


    private void Start()
    {
        //position_Player = GameObject.Find("Player").transform;
        //Invoke("shotToPlayer", 1);
    }

   public void shotToPlayer()
    {
        if (startShooting)
        {
        Vector3 directionPlayer = position_Player.position - transform.position;

        GameObject bullet = Instantiate(enemy_Bullet, initPoint_Bullet.position, initPoint_Bullet.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(directionPlayer*speed_Bullet);
        Invoke("shotToPlayer", .3f);
        }
    }



}
