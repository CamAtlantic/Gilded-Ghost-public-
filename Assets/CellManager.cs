using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellManager : MonoBehaviour {

    //Holds all data pertaining to the Cell
    //which is to be persistent.

    Sun _sun;
    Tray _tray;
    ItemLocation[] itemLocations;


    void Awake()
    { 
        _sun = GameObject.Find("Sun").GetComponent<Sun>();
        _tray = GameObject.Find("Tray").GetComponent<Tray>();
        itemLocations = FindObjectsOfType<ItemLocation>();
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
