using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Text playButtonText;
    public Text settingsButtonText;
    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        playButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Jugar" : "Play");
        settingsButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Configuración" : "Settings");
    }

    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    void Update()
    {
        
    }
}
