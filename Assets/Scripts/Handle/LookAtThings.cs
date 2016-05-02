using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LookAtThings : MonoBehaviour {

    public Camera mainCamera;
    Ray ray;

    public GameObject[] interactables;
    public List<HandleScript> handles = new List<HandleScript>();

    public GameObject handle;
    public Canvas canvas;
    

    // Use this for initialization
    void Start () {

        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        for(int i = 0; i < interactables.Length; i++)
        {
            GameObject tempHandle = (GameObject)Instantiate(handle);

            //print(interactables[i].name);
            handles.Add(tempHandle.GetComponent<HandleScript>());
            tempHandle.transform.SetParent(canvas.transform, false);
            handles[i].handleTarget = interactables[i];
            

            //tempPanel.GetComponent<HandleScript>().SetAlpha();
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 screenCenter = new Vector2(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);

        for (int i = 0; i < handles.Count; i++)
        {
            //GameObject handleTarget = handles[i].handleTarget;
           if(RectTransformUtility.RectangleContainsScreenPoint(handles[i].dotRect,screenCenter,mainCamera))
            {
                print(handles[i].handleTarget.name);
            }

        }




        //ray = mainCamera.ScreenPointToRay(new Vector3( mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2));
        //RaycastHit hit;
        
        /*
        if (Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.tag == "Interactable")
        {
            GameObject target = hit.collider.gameObject;
            // target.GetComponent
            print(target.GetComponent<HandleScript>().handleTarget);

        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        */
        

    }
    void LateUpdate()
    {

        for (int i = 0; i < interactables.Length; i++)
        {
            Vector3 worldPos = interactables[i].transform.position;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            Vector3 currentPos = handles[i].transform.position;
            handles[i].transform.position = Vector3.Lerp(currentPos, screenPos, 5f);


        }
    }
}
