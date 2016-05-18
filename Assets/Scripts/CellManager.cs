using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour {

    public GameObject plantPrefab;

    bool plantHasSpawned = false;

    void Awake()
    { 
   
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
