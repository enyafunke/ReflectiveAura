using Captury;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFix : MonoBehaviour
{
    private CapturySimpleAvatarManager avatarManager = null;
    private List<CapturySkeleton> trackedSkeletons = null;
    public float fixDegree = 90;

    // Start is called before the first frame update
    void Start()
    {
        avatarManager = GetComponent<CapturySimpleAvatarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateSkeletonOriginalRotationY()
    {
        trackedSkeletons = avatarManager.getTrackedSkeletons();
        foreach (CapturySkeleton skel in trackedSkeletons)
        {
            foreach (CapturySkeletonJoint joint in skel.joints)
            {
                if (joint.name.Equals("Hips"))
                {
                    joint.originalRotation.y = fixDegree;
                }
            }
        }
    }
}
