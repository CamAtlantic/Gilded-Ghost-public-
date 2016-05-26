using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour {

    public GameObject plantPrefab;
    public GameObject birdPrefab;

    bool plantHasSpawned = false;
    bool birdHasSpawned = false;
    
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
