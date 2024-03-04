using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    enum State {Angry = 0, Happy = 1, Sad = 2, Neutral = 3};

    [SerializeField] List<Animator> animators;

    public void SetAngry()
    {
        foreach (var animator in animators)
        {
            animator.SetInteger("State", (int)State.Angry);
        }
    }

    public void SetHappy()
    {
        foreach (var animator in animators)
        {
            animator.SetInteger("State", (int)State.Happy);
        }
    }

    public void SetSad()
    {
        foreach (var animator in animators)
        {
            animator.SetInteger("State", (int)State.Sad);
        }
    }

    public void SetNeutral()
    {
        foreach (var animator in animators)
        {
            animator.SetInteger("State", (int)State.Neutral);
        }
    }
}
