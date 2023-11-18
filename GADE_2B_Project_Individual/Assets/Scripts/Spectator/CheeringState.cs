using UnityEngine;

public class CheeringState : ISpectatorState
{
    private Animator animator;

    public CheeringState(Animator animator)
    {
        this.animator = animator;
    }

    public void HandleAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Cheering", true);
        animator.SetBool("Defeated", false);
    }
}