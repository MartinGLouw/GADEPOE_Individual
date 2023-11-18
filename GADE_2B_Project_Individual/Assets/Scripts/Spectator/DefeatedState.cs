using UnityEngine;

public class DefeatedState : ISpectatorState
{
    private Animator animator;

    public DefeatedState(Animator animator)
    {
        this.animator = animator;
    }

    public void HandleAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Cheering", false);
        animator.SetBool("Defeated", true);
    }
}