using System.Collections;
using UnityEngine;

public class StateSad : EmotionBaseState
{
    private IEnumerator coroutine;
    private float timerNeutral = 0.0f;

    public override void EnterState(EmotionStateManager emotion)
    {
        Debug.Log("Sad!");
        timerNeutral = 0.0f;

        coroutine = emotion.ColorLerp(emotion.input.GetColor("_Input_Color_2"), new Color32(0, 0, 75, 255), 3f);
        emotion.StartCoroutine(coroutine);
        emotion.backg.SetSad();
        emotion.soundManager.SadSound();
        emotion.blendshapeDriver.SetSad();
    }

    public override void UpdateState(EmotionStateManager emotion)
    {

        if (timerNeutral >= 2.0f)
        {
            if (emotion.limbs.head.transform.position.y >= 1.4)
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
