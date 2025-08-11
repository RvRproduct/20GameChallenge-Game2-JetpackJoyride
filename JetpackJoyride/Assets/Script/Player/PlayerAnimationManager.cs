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

    public void TryTriggerHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void TryTriggerDeath()
    {
        animator.SetTrigger("Death");
    }

    public void SetIsJumping(bool _isJumping)
    {
        animator.SetBool("IsJumping", _isJumping);
    }

    public void SetOnFloor(bool _onFloor)
    {
        animator.SetBool("OnFloor", _onFloor);
    }

    public bool GetOnFloor()
    {
        return animator.GetBool("OnFloor");
    }

    public void SetIsHurting(bool _isHurting)
    {
        animator.SetBool("IsHurting", _isHurting);
    }

    public bool GetIsHurting()
    {
        return animator.GetBool("IsHurting");
    }

}
