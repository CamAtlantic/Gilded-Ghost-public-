using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// DO NOT choose System. That's a bad one.
/// </summary>
public enum Scenes { System,Cell,Mountain,Columns,Fire};
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
    CamBackgroundColor m_CamBackground;
    Transform cellSleepingTransform;
    Transform mountainTransform;
    #endregion

    #region Dream Variables
    [Header("Mountain")]
    public bool mountain_door = false;
    /// <summary>
    /// Triggered when player chooses the plant in the mountain dream.
    /// </summary>
    public bool mountain_plant = false;
    [Header("Columns")]
    public bool columns_water = false;
    public bool columns_orb = false;
    [Header("Fire")]
    public bool fire_fire = false;
    public bool fire_thrones = false;
    [Space(10)]
    #endregion

    public Color mountainSkyColor;
    public Color columnSkyColor;
    public Color fireSkyColor;

    public Scenes loadedScene = Scenes.Cell;

    void Awake()
    {
        player = GameObject.Find("Player");
        _sleepingAndWaking = player.GetComponent<SleepingAndWaking>();
        _needs = player.GetComponent<Needs>();
        _FPSController = player.GetComponent<FirstPersonController>();
        _dreamText = GameObject.Find("Dream Canvas").GetComponent<DreamText>();
        _dayManager = GetComponent<DayManager>();
        m_CamBackground = Camera.main.gameObject.GetComponent<CamBackgroundColor>();
        LoadScene(Scenes.Cell);
    }

	// Use this for initialization
	void Start () {
        _sun = GameObject.Find("Sun").GetComponent<Sun>();
        _cellManager = GameObject.Find("CellManager").GetComponent<CellManager>();
        cellSleepingTransform = GameObject.Find("CellSleepTransform").transform;
        SetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {

        //need to replace this with a dream-specific implementation.
        //should display text and then wake the player.

        if (_sleepingAndWaking.sleepState == SleepState.asleep)
        {
            if (loadedScene != Scenes.Cell && _dreamText.DisplayText())
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
        if (!mountain_door && !mountain_plant)
        {
            LoadScene(Scenes.Mountain);
            m_CamBackground.SetBackgroundColor(mountainSkyColor);
        }
        else if(mountain_plant)
        {
            LoadScene(Scenes.Columns);
            m_CamBackground.SetBackgroundColor(columnSkyColor);
        }
        else if(mountain_door || columns_orb || columns_water)
        {
            LoadScene(Scenes.Fire);
            m_CamBackground.SetBackgroundColor(fireSkyColor);
        }

        _needs.ToggleHungerEffects(false);
        _cellManager.ShowHideCell(false);
        QualitySettings.shadowDistance = 50;
        _FPSController.SetWalkSpeed(3);
    }
    
    public void EndDream()
    {
        Unload();
        loadedScene = Scenes.Cell;

        m_CamBackground.SetSkyBox();

        _sleepingAndWaking.SetPosition(cellSleepingTransform);
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
