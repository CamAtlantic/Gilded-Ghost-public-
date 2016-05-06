using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DreamController : MonoBehaviour {

    Scene cellAsset;

    void Awake()
    {
        cellAsset = SceneManager.GetSceneByName("Cell");
        if (!cellAsset.isLoaded)
            LoadCell();
    }

	// Use this for initialization
	void Start () {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void LoadCell()
    {
        SceneManager.LoadSceneAsync("Cell",LoadSceneMode.Additive);
    }

    public void UnloadCell()
    {
        SceneManager.UnloadScene("Cell");
    }

    public void LoadMountainDream()
    {
        SceneManager.LoadSceneAsync("Mountain Dream", LoadSceneMode.Additive);
        
        SceneManager.UnloadScene(1);
    }

    public void SetActiveScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
}
