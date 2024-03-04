using System.Collections.Generic;
using UnityEngine;
using Captury;
using System;
using System.Linq;

[RequireComponent(typeof(CapturyNetworkPlugin))]
public class CapturyHook : MonoBehaviour {
    [Serializable]
    public struct TrackedSkeleton {
        public string name;
        public int id;

        private CapturySkeleton skeleton;
        public CapturySkeletonJoint[] joints;
        private Dictionary<string, int> transforms;

        public GameObject Target {
            get {
                if (skeleton == null) return null;
                return skeleton.Reference;
            }
        }

        public string[] jointNames() {
            return transforms.Keys.ToArray();
        }

        public Transform Transform(string name) {
            int id;
            if (!transforms.TryGetValue(name, out id)) return null;
            else return joints[id].transform;
        }

        public TrackedSkeleton(CapturySkeleton capturySkeleton) {
            name = capturySkeleton.name;
            id = capturySkeleton.id;
            skeleton = capturySkeleton;
            joints = capturySkeleton.joints;
            transforms = new Dictionary<string, int>();
            for (int i = 0; i < joints.Length; ++i) transforms.Add(joints[i].name, i);
        }
    }


    private CapturyNetworkPlugin capturyPlugin;
    public List<TrackedSkeleton> skeletons;

    void Start() {
        capturyPlugin = GetComponent<CapturyNetworkPlugin>();
        capturyPlugin.SkeletonFound += OnSkeletonFound;
        capturyPlugin.SkeletonLost += OnSkeletonLost;
    }

    private void OnApplicationQuit() {
        capturyPlugin.SkeletonFound -= OnSkeletonFound;
        capturyPlugin.SkeletonLost -= OnSkeletonLost;
    }

    private void OnSkeletonFound(CapturySkeleton capturySkeleton) {
        if (capturySkeleton == null) return;
        TrackedSkeleton skeleton = new TrackedSkeleton(capturySkeleton);
        skeletons.Add(skeleton);
    }

    private void OnSkeletonLost(CapturySkeleton capturySkeleton) {
        if (capturySkeleton == null) return;
        foreach (TrackedSkeleton skeleton in skeletons) {
            if (skeleton.id == capturySkeleton.id) {
                skeletons.Remove(skeleton);
                break;
            }
        }
    }
}
