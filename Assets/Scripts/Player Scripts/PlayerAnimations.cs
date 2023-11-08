using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public enum AnimationState { IDLE, RUNNING_HORIZONTAL, RUNNING_UP, RUNNING_DOWN };
    [SerializeReference]private AnimationState currentState = AnimationState.IDLE;

    private Dictionary<AnimationState, string> animationStatePairs = new Dictionary<AnimationState, string>() 
    {
        {AnimationState.IDLE, "Player_Idle" },
        {AnimationState.RUNNING_HORIZONTAL, "Player_Movement_Horizontal" },
        {AnimationState.RUNNING_UP, "Player_Movement_Up" },
        {AnimationState.RUNNING_DOWN, "Player_Idle" }
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeAnimationState(AnimationState newState)
    {
        if (newState == currentState)
            return;
        try
        {
            animator.Play(animationStatePairs[newState]);
        }
        catch
        {
            Debug.LogError("Invalid Player Animation");
            return;
        }
        currentState = newState;
    }
}
