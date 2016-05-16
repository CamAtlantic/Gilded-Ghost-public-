using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

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
    GameObject player;
    FirstPersonController _FPSController;
    #endregion

    public Scenes loadedScene = Scenes.Cell;

    void Awake()
    {
        _dreamText = GameObject.Find("Dream Canvas").GetComponent<DreamText>();
        _sleepingAndWaking = GameObject.Find("Player").GetComponent<SleepingAndWaking>();
        _needs = GameObject.Find("Player").GetComponent<Needs>();
        _dayManager = GetComponent<DayManager>();
        player = GameObject.Find("Player");
        _FPSController = player.GetComponent<FirstPersonController>();
        LoadScene(Scenes.Cell);
    }

	// Use this for initialization
	void Start () {
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

        //Determines which dream to start, then starts it.
        LoadScene(Scenes.Mountain);

        _needs.ToggleHungerEffects(false);
        _cellManager.ShowHideCell(false);
        QualitySettings.shadowDistance = 50;
        _FPSController.SetWalkSpeed(3);
    }
    
    public void EndDream()
    {
        _sleepingAndWaking.ResetPosition();
        Unload();
        loadedScene = Scenes.Cell;

        _needs.ToggleHungerEffects(true);
        _cellManager.ShowHideCell(true);
        QualitySettings.shadowDistance = 10;
        _FPSController.SetWalkSpeed(1);

        
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
