using UnityEngine;

public abstract class EmotionBaseState
{
    //Abstract State for StateMachine

    public abstract void EnterState(EmotionStateManager emotion);

    public abstract void UpdateState(EmotionStateManager emotion);

    public abstract void onCollisionEnter(EmotionStateManager emotion, Collision collision);

}
