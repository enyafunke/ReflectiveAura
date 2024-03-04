using System.Collections;
using UnityEngine;

public class StateNeutral : EmotionBaseState
{
    public IEnumerator coroutine;
    private float timerAngry = 0.0f;
    private float timerSad = 0.0f;
    private float timerHappy = 0.0f;

    public override void EnterState(EmotionStateManager emotion)
    {
        Debug.Log("Neutral!");
        timerAngry = 0.0f;
        coroutine = emotion.ColorLerp(emotion.input.GetColor("_Input_Color_2"), new Color32(187, 177, 160, 255), 2f);
        emotion.StartCoroutine(coroutine);
        emotion.backg.SetNeutral();
        emotion.soundManager.NeutralSound();
        emotion.blendshapeDriver.SetNeutral();
    }

    public override void UpdateState(EmotionStateManager emotion)
    {

        if (emotion.limbs.head && emotion.limbs.leftHand && emotion.limbs.rightHand && emotion.limbs.neck && emotion.limbs.leftElbow && emotion.limbs.rightElbow && emotion.limbs.shoulders != null)
        {

            //Sad
            if (emotion.limbs.hips.transform.position.y > emotion.limbs.leftHand.transform.position.y && emotion.limbs.hips.transform.position.y > emotion.limbs.rightHand.transform.position.y) //Hände unter Hüften
            {            
                if (emotion.limbs.head.transform.position.y < 1.4)
                {
                    timerSad += Time.deltaTime;
                    if (timerSad >= 1.5f)
                    { //Dauer bis Statewechsel
                        timerSad = 0.0f;
                        emotion.StopCoroutine(coroutine);
                        emotion.SwitchState(emotion.StateSad);
                    }
                }
                //emotion.limbs.neck.transform.position.y > emotion.limbs.head.transform.position.y
            }

            //Happy
            if (emotion.limbs.head.transform.position.y > 1.4)
            {
                if (emotion.limbs.leftHand.transform.position.y > emotion.limbs.head.transform.position.y && emotion.limbs.rightHand.transform.position.y > emotion.limbs.head.transform.position.y)
                {            //Hände über Kopf
                    timerHappy += Time.deltaTime;
                    if (timerHappy >= 1.5f)
                    { //Dauer bis Statewechsel
                        timerHappy = 0.0f;
                        emotion.StopCoroutine(coroutine);
                        emotion.SwitchState(emotion.StateHappy);
                    }
                }
            }

            //Angry
            if (emotion.limbs.head.transform.position.y > 1.4)
            {
                if (emotion.limbs.leftHand.transform.position.y > emotion.limbs.hips.transform.position.y && emotion.limbs.shoulders.transform.position.y > emotion.limbs.leftHand.transform.position.y)
                {        //Linke Hand zwischen Schultern und Hüften
                    if (emotion.limbs.rightHand.transform.position.y > emotion.limbs.hips.transform.position.y && emotion.limbs.shoulders.transform.position.y > emotion.limbs.rightHand.transform.position.y)
                    {  //Rechte Hand zwischen Schultern und Hüften
                        timerAngry += Time.deltaTime;
                        if (timerAngry >= 1.5f)
                        { //Dauer bis Statewechsel
                            timerAngry = 0.0f;
                            emotion.StopCoroutine(coroutine);
                            emotion.SwitchState(emotion.StateAngry);
                        }
                    }
                }
            }
        }
    }

    public override void onCollisionEnter(EmotionStateManager emotion, Collision collision)
    {

    }
}
