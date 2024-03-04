using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeDriver : MonoBehaviour
{
    GameObject trackingArea;
    public GameObject avatarBody;
    private SkinnedMeshRenderer avatarMesh = null;


    void Update()
    {
        //Hände
        if (GameObject.Find("CapturyAvatar(Clone)/IK/mirevi/Body"))
        {
            avatarBody = GameObject.Find("CapturyAvatar(Clone)/IK/mirevi/Body");
            avatarMesh = avatarBody.GetComponent<SkinnedMeshRenderer>();
        }
    }

    public void SetAngry()
    {
        if (avatarMesh != null)
        {
            avatarMesh.SetBlendShapeWeight(0, 0.0f);
            avatarMesh.SetBlendShapeWeight(1, 0.0f);
            avatarMesh.SetBlendShapeWeight(2, 100.0f);
            avatarMesh.SetBlendShapeWeight(3, 0.0f);
        }
    }

    public void SetSad()
    {
        if (avatarMesh != null)
        {
            avatarMesh.SetBlendShapeWeight(0, 0.0f);
            avatarMesh.SetBlendShapeWeight(1, 100.0f);
            avatarMesh.SetBlendShapeWeight(2, 0.0f);
            avatarMesh.SetBlendShapeWeight(3, 0.0f);
        }
    }

    public void SetHappy()
    {
        if (avatarMesh != null)
        {
            avatarMesh.SetBlendShapeWeight(0, 100.0f);
            avatarMesh.SetBlendShapeWeight(1, 0.0f);
            avatarMesh.SetBlendShapeWeight(2, 0.0f);
            avatarMesh.SetBlendShapeWeight(3, 0.0f);
        }
    }

    public void SetNeutral()
    {
        if (avatarMesh != null)
        {
            avatarMesh.SetBlendShapeWeight(0, 0.0f);
            avatarMesh.SetBlendShapeWeight(1, 0.0f);
            avatarMesh.SetBlendShapeWeight(2, 0.0f);
            avatarMesh.SetBlendShapeWeight(3, 0.0f);
        }
    }
}
