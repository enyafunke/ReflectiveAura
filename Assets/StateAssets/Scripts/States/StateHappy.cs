using System.Collections;
using UnityEngine;

public class StateHappy : EmotionBaseState
{
    private IEnumerator coroutine;
    private float timerNeutral = 0.0f;

    public override void EnterState(EmotionStateManager emotion)
    {
        Debug.Log("Happy!");
        timerNeutral = 0.0f;

        coroutine = emotion.ColorLerp(emotion.input.GetColor("_Input_Color_2"), new Color32(255, 232, 128, 255), 3f);
        emotion.StartCoroutine(coroutine);
        emotion.backg.SetHappy();
        emotion.soundManager.HappySound();
        emotion.blendshapeDriver.SetHappy();
    }

    public override void UpdateState(EmotionStateManager emotion)
    {

        if (timerNeutral >= 2.0f)
        {
            if (emotion.limbs.head.transform.position.y > emotion.limbs.leftHand.transform.position.y || emotion.limbs.head.transform.position.y > emotion.limbs.rightHand.transform.position.y)
            {
                emotion.StopCoroutine(coroutine);
                emotion.SwitchState(emotion.StateNeutral);
            }
        }
        else
        {
            timerNeutral += Time.deltaTime;
        }
    }

    public override void onCollisionEnter(EmotionStateManager emotion, Collision collision)
    {

    }
}
