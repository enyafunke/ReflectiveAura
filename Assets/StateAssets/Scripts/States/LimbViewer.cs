using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbViewer : MonoBehaviour
{
    GameObject trackingArea;
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject head;
    public GameObject neck;

    public GameObject leftElbow;
    public GameObject rightElbow;

    public GameObject hips;

    public GameObject shoulders;

    void Start()
    {
        trackingArea = GameObject.Find("TrackingArea");
    }

    void Update()
    {

        //Hände
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/LeftShoulder/LeftArm/LeftForeArm/LeftHand"))
        {
            leftHand = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/LeftShoulder/LeftArm/LeftForeArm/LeftHand");
        }
        else
        {
            Debug.Log("leftHand doesn't exist!");
        }

        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/RightShoulder/RightArm/RightForeArm/RightHand"))
        {
            rightHand = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/RightShoulder/RightArm/RightForeArm/RightHand");
        }
        else
        {
            Debug.Log("rightHand doesn't exist!");
        }


        //Kopf
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4/Neck/Head/HeadEE"))
        {
            head = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4/Neck/Head/HeadEE");
        }
        else
        {
            Debug.Log("head doesn't exist!");
        }

        //Neck
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4/Neck"))
        {
            neck = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4/Neck");
        }
        else
        {
            Debug.Log("neck doesn't exist!");
        }


        //Ellenbögen
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/LeftShoulder/LeftArm/LeftForeArm"))
        {
            leftElbow = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/LeftShoulder/LeftArm/LeftForeArm");
        }
        else
        {
            Debug.Log("leftElbow doesn't exist!");
        }

        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/RightShoulder/RightArm/RightForeArm"))
        {
            rightElbow = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/RightShoulder/RightArm/RightForeArm");
        }
        else
        {
            Debug.Log("rightElbow doesn't exist!");
        }

        //Hips
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips"))
        {
            hips = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips");
        }
        else
        {
            Debug.Log("hips doesn't exist!");
        }

        //Shoulders
        if (GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4"))
        {
            shoulders = GameObject.Find("CapturyAvatar(Clone)/defaultLiveHands/Root/Hips/Spine/Spine1/Spine2/Spine3/Spine4");
        }
        else
        {
            Debug.Log("shoulders doesn't exist!");
        }
    }
}
