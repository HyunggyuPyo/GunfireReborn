using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxResultAnimator : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(bool result)
    {
        if(result)
        {
            animator.SetBool("Clear", true);
        }
        else
        {
            animator.SetBool("Clear", false);
        }
    }
}
