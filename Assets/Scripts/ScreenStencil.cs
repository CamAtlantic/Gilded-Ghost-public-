using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenStencil : MonoBehaviour
{
    public float mask;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Stencils/StencilMaskGeometry"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        material.SetFloat("_StencilMask", mask);
        Graphics.Blit(source, destination, material);
    }
}