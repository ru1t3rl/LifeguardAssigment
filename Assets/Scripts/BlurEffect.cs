using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]

public class BlurEffect : MonoBehaviour
{
    public Material material;

	void Start()
    {
        if (material == null || material.shader == null || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
		Graphics.Blit(source, destination, material);
	}
}