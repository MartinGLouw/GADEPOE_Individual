using UnityEngine;

public class IdleState : ISpectatorState
{
    private Animator animator;

    public IdleState(Animator animator)
    {
        this.animator = animator;
    }

    public void HandleAnimation()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Cheering", false);
        animator.SetBool("Defeated", false);
    }
}