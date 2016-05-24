using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DreamText : MonoBehaviour {

    Text dreamText;
    public AnimationCurve alphaCurve;

    float displayTimer;
    public float displayTimerMax = 10;

    #region Lines
    [HideInInspector]
    public string mountain_intro = "You made a mistake, but nobody cares about your intentions. Will you confront the truth, or turn aside?";
    [HideInInspector]
    public string mountain_door = " You step into the darkness. This will not be easy. Maybe things could have been different.";
    [HideInInspector]
    public string mountain_plant = "Your guilt grows inside you, like a strangling vine. Maybe things could have been different.";


    [HideInInspector]
    public string columns_intro = "Guilt is a thing that can drown a soul, and the water is rising. Will you fall?";
    [HideInInspector]
    public string columns_water = "The murk swallows you, but perhaps this is the first step toward absolution. Maybe things could have been different.";
    [HideInInspector]
    public string columns_orb = " Each logical turn brings you away from the darkness, but it remains. Maybe things could have been different.";


    [HideInInspector]
    public string fire_intro = "No more than three folk have gazed on their true souls, and lived. You can still look away.";
    [HideInInspector]
    public string fire_fire = "The light sears your vision, but you hold steady. Maybe things could have been different.";
    [HideInInspector]
    public string fire_thrones = "Ignored, the coals smolder behind you. Maybe things could have been different.";
    #endregion

    // Use this for initialization
    void Start () {
        dreamText = transform.FindChild("Dream Text").GetComponent<Text>();
	}
    
    /// <param name="dreamTextLine">Choose from DreamText public strings.</param>
    public void SetDreamText(string dreamTextLine)
    {
        dreamText.text = dreamTextLine;
    }

    /// <summary>Returns true when it's finished displaying.</summary>
    public bool DisplayText()
    {
        if (displayTimer > displayTimerMax)
        {
            displayTimer = 0;
            return true;
        }
        displayTimer += Time.deltaTime;
        dreamText.color = new Color(dreamText.color.r, dreamText.color.g, dreamText.color.b, alphaCurve.Evaluate(displayTimer / displayTimerMax));

        return false;
    }
}
