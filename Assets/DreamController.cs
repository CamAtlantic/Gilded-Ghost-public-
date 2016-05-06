using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// DO NOT choose System. That's a bad one.
/// </summary>
public enum Scenes { System,Cell,Mountain};
//Make sure this enum has the scenes listed in the correct order.


public class DreamController : MonoBehaviour {

    DreamText _dreamText;
    SleepingAndWaking _sleepingAndWaking;

    public Scenes loadedScene = Scenes.Cell;
    bool sceneHasLoaded = false;

    void Awake()
    {
        _dreamText = GameObject.Find("Dream").GetComponent<DreamText>();
        _sleepingAndWaking = GameObject.Find("Player").GetComponent<SleepingAndWaking>();
        EndDream();
    }

	// Use this for initialization
	void Start () {
        SetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
        //need to replace this with a dream-specific implementation.
        //should display text and then wake the player.
        if (loadedScene == Scenes.Mountain)
            if (_dreamText.DisplayText() && _sleepingAndWaking.sleepState == SleepState.asleep)
                _sleepingAndWaking.WakeUp();
    }

    public void StartDream()
    {
        //Determines which dream to start, then starts it.
        LoadScene(Scenes.Mountain);
        QualitySettings.shadowDistance = 50;
    }
    
    public void EndDream()
    {
        LoadScene(Scenes.Cell);
        QualitySettings.shadowDistance = 10;
    }
    
    private void LoadScene(Scenes scene)
    {
        loadedScene = scene;
        SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        SceneManager.UnloadScene(1);
    }

    public void SetActiveScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
}
