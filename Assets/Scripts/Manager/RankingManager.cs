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
    // Start is called before the first frame update
    void Start()
    {
        bestScorePointsText.text= "Best: "+ PlayerPrefs.GetInt("MaxScore");
        SecondbestScorePointsText.text = "2: " + PlayerPrefs.GetInt("2MaxScore");
        ThirdbestScorePointsText.text = "3: " + PlayerPrefs.GetInt("3MaxScore");
        FourthbestScorePointsText.text = "4: " + PlayerPrefs.GetInt("4MaxScore");
        FifthbestScorePointsText.text = "5: " + PlayerPrefs.GetInt("5MaxScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
