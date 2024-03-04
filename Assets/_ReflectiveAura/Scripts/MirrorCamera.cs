using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class MirrorCamera : MonoBehaviour {
    private float aspectRatio = 0;
    void Start() {
        RenderPipelineManager.beginCameraRendering += beginCameraRendering;
        RenderPipelineManager.endCameraRendering += endCameraRendering;
    }
    
    private void OnApplicationQuit() {
        RenderPipelineManager.beginCameraRendering -= beginCameraRendering;
        RenderPipelineManager.endCameraRendering -= endCameraRendering;
        Camera.main.ResetWorldToCameraMatrix();
        Camera.main.ResetProjectionMatrix();
    }

    void beginCameraRendering(ScriptableRenderContext context, Camera camera) {
        if (camera != Camera.main) return;
        GL.invertCulling = true;

        if (aspectRatio != Camera.main.aspect) {
            aspectRatio = Camera.main.aspect;

            Camera.main.ResetWorldToCameraMatrix();
            Camera.main.ResetProjectionMatrix();
            Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        }
    }

    void endCameraRendering(ScriptableRenderContext context, Camera camera) {
        if (camera != Camera.main) return;
        GL.invertCulling = false;
    }
}
