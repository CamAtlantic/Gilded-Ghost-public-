using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// DO NOT choose System. That's a bad one.
/// </summary>
public enum Scenes { System,Cell,Mountain};
//Make sure this enum has the scenes listed in the correct order.

public class DreamController : MonoBehaviour {

    #region references
    CellManager _cellManager;
    DreamText _dreamText;
    SleepingAndWaking _sleepingAndWaking;
    Sun _sun;
    Needs _needs;
    DayManager _dayManager;
    #endregion

//    Scene cell;

    public Scenes loadedScene = Scenes.Cell;
//    bool sceneHasLoaded = false;

    void Awake()
    {
        _dreamText = GameObject.Find("Dream Canvas").GetComponent<DreamText>();
        _sleepingAndWaking = GameObject.Find("Player").GetComponent<SleepingAndWaking>();
        _needs = GameObject.Find("Player").GetComponent<Needs>();
        _dayManager = GetComponent<DayManager>();

        LoadScene(Scenes.Cell);
        //cell = SceneManager.GetSceneAt(1);
    }

	// Use this for initialization
	void Start () {
        //will probably break when cell unloads
        _sun = GameObject.Find("Sun").GetComponent<Sun>();
        _cellManager = GameObject.Find("CellManager").GetComponent<CellManager>();
        SetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
        //need to replace this with a dream-specific implementation.
        //should display text and then wake the player.

        if (_sleepingAndWaking.sleepState == SleepState.asleep)
        {
            if (loadedScene == Scenes.Mountain && _dreamText.DisplayText())
                _sleepingAndWaking.WakeUp();

            if (loadedScene == Scenes.Cell)
                _sleepingAndWaking.WakeUp();
        }
    }

    public void StartDream()
    {
        _sun.UpdateSunWhileSleeping();
        _needs.UpdateHungerWhileSleeping();
        _dayManager.UpdateTimeWhileSleeping();

        _cellManager.ShowHideCell(false);

        //Determines which dream to start, then starts it.
        LoadScene(Scenes.Mountain);
        QualitySettings.shadowDistance = 50;
    }
    
    public void EndDream()
    {
        loadedScene = Scenes.Cell;
        _cellManager.ShowHideCell(true);
        QualitySettings.shadowDistance = 10;

        Unload();
    }
    
    private void LoadScene(Scenes scene)
    {
        loadedScene = scene;
        SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);

        Unload();
    }

    void Unload()
    {
        if (SceneManager.sceneCount > 2)
            SceneManager.UnloadScene(SceneManager.GetSceneAt(2).name);
    }

    public void SetActiveScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount-1));
    }
}
