using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private ConfigurationSettings cs;

    private void Awake()
    {
        cs = ConfigurationSettings.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setDificulty(float value)
    {
        cs.setDificulty(value);
    }

    public void setLanguage(int value)
    {
        cs.setLanguage(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
