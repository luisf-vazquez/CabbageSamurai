using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{

    public Text bestScorePointsText;
    public Text SecondbestScorePointsText;
    public Text ThirdbestScorePointsText;
    public Text FourthbestScorePointsText;
    public Text FifthbestScorePointsText;
    public Text ReturnButtonText;
    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        bestScorePointsText.text= (ConfigurationSettings.instance.getLanguage() == "Español" ? "Mejor: " : "Best: ") + PlayerPrefs.GetInt("MaxScore");
        SecondbestScorePointsText.text = "2: " + PlayerPrefs.GetInt("2MaxScore");
        ThirdbestScorePointsText.text = "3: " + PlayerPrefs.GetInt("3MaxScore");
        FourthbestScorePointsText.text = "4: " + PlayerPrefs.GetInt("4MaxScore");
        FifthbestScorePointsText.text = "5: " + PlayerPrefs.GetInt("5MaxScore");
        ReturnButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "VOLVER" : "RETURN");
    }

    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    void Update()
    {
        
    }
}
