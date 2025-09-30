using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartIdle()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Shooting", false);
        animator.SetBool("Dead", false);
    }
    public void StartWalking()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
        animator.SetBool("Shooting", false);
        animator.SetBool("Dead", false);
    }
    public void StartRuning()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Shooting", false);
        animator.SetBool("Dead", false);
    }
    public void StartShooting()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Shooting", true);
        animator.SetBool("Dead", false);
    }
    public void StartDead()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Shooting", false);
        animator.SetBool("Dead", true);
    }
}
