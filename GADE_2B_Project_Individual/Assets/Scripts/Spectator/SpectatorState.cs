using System.Collections;
using UnityEngine;

public class SpectatorController : MonoBehaviour
{
    private ISpectatorState currentState;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetState(new IdleState(animator));
        float randomDelay = Random.Range(0f, 3f); 
        StartCoroutine(StateDelay(randomDelay)); 
    }

    private IEnumerator StateDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StateTimer());
    }


    private void SetState(ISpectatorState state)
    {
        currentState = state;
        currentState.HandleAnimation();
    }

    private IEnumerator StateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5)); // Changes state of spectators every 1 to 5 seconds

            int randomState = Random.Range(0, 3);  // Chooses a random state
            switch (randomState)
            {
                case 0:
                    SetState(new IdleState(animator));
                    break;
                case 1:
                    SetState(new CheeringState(animator));
                    break;
                case 2:
                    SetState(new DefeatedState(animator));
                    break;
            }
        }
    }
}