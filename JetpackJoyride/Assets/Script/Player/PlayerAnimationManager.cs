using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public static PlayerAnimationManager Instance;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void TryTriggerRun()
    {
        animator.SetTrigger("Run");
    }

    public void TryTriggerJump()
    {
        animator.SetTrigger("Jump");
    }

    public void TryTriggerFall()
    {
        animator.SetTrigger("Fall");
    }
}
