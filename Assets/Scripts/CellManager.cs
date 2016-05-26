using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour {

    public GameObject cellLightObject;
    public Light cellLightLight;

    public GameObject plantPrefab;
    public GameObject birdPrefab;
    public GameObject lighterPrefab;

    bool plantHasSpawned = false;
    bool birdHasSpawned = false;
    bool lighterHasSpawned = false;

    bool killLight = false;
    bool lightDead = false;

    void Awake()
    {
        cellLightObject = transform.FindChild("CellLight").gameObject;
        cellLightLight = cellLightObject.transform.FindChild("Light").GetComponent<Light>();
    }

    void Update()
    {
        if (killLight && !lightDead && DreamController.loadedScene == Scenes.Cell)
        {
            lightDead = true;

            RenderSettings.ambientIntensity = 0.5f;
            cellLightLight.enabled = false;

            Renderer rend = cellLightObject.GetComponent<Renderer>();
            Material mat = rend.material;
            Color newC = mat.color * Mathf.LinearToGammaSpace(0);
            mat.SetColor("_EmissionColor", newC);
            DynamicGI.SetEmissive(rend, newC);
        }
    }

    public void SpawnPlant()
    {
        if(!plantHasSpawned)
        {
            GameObject plant;
            plant = Instantiate(plantPrefab);
            plant.transform.SetParent(GameObject.Find("Cell").transform);
        }
    }

    public void SpawnBird()
    {
        if (!birdHasSpawned)
        {
            GameObject bird;
            bird = Instantiate(birdPrefab);
            bird.transform.SetParent(GameObject.Find("Cell").transform);
        }
    }

    public void SpawnLighter()
    {
        if (!lighterHasSpawned)
        {
            GameObject lighter;
            lighter = Instantiate(lighterPrefab);
            lighter.transform.SetParent(GameObject.Find("Cell").transform);
        }
    }

    public void KillLight()
    {
        killLight = true;
    }

    /// <summary>
    /// True for show, False for hide.
    /// </summary>
	public void ShowHideCell(bool showHide)
    {
        MeshRenderer[] renderers;
        Collider[] colliders;
        Light[] lights;

        renderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
        lights = GetComponentsInChildren<Light>();

        foreach(MeshRenderer meshR in renderers)
        {
            meshR.enabled = showHide;
        }
        foreach (Collider col in colliders)
        {
            col.enabled = showHide;
        }
        foreach (Light l in lights)
        {
            l.enabled = showHide;
        }
    }

}
