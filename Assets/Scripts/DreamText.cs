using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DreamText : MonoBehaviour {

    Text dreamText;
    public AnimationCurve alphaCurve;

    float displayTimer;
    public float displayTimerMax = 10;

	// Use this for initialization
	void Start () {
        dreamText = transform.FindChild("Dream Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// Returns true when it's finished displaying.
    /// </summary>
    /// <returns></returns>
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
