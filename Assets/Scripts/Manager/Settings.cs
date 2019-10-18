using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Slider ddlDificulty;
    public Dropdown ddlLanguage;
    public Text returnButtonText;
    public Text LanguageText;
    public Text EasyText;
    public Text MediumText;
    public Text HardText;

    /// <summary>
    /// Se le llama en cuanto se instancia el objeto.
    /// </summary>
    private void Awake()
    {

    }
    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ddlDificulty.value = PlayerPrefs.GetFloat("dificulty");
        ddlLanguage.value = PlayerPrefs.GetInt("language");
        
    }
    /// <summary>
    /// Setea los textos en pantalla según el idioma actual.
    /// </summary>
    private void setTexts()
    {
        returnButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "VOLVER" : "RETURN");
        LanguageText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Elija Idioma" : "Choose Language");
        EasyText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Fácil" : "Easy");
        MediumText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Medio" : "Medium");
        HardText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Difícil" : "Hard");
    }
    /// <summary>
    /// Guarda la dificultad elegida en el singleton de configuración
    /// </summary>
    /// <param name="value">El valor entre 1 y 3 del slider de dificultad elegido</param>
    public void setDificulty(float value)
    {
        ConfigurationSettings.instance.setDificulty(value);
    }
    /// <summary>
    /// Setea el valor del índice del idioma elegido, guardándolo en local para mantener las preferencias del usuario.
    /// </summary>
    /// <param name="value">El valor entre 0 y 1 del dropdown de idioma elegido</param>
    public void setLanguage(int value)
    {
        ConfigurationSettings.instance.setLanguage(value);
       
    }

    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    void Update()
    {
        setTexts();
    }
}
