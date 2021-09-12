using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class opcionesScr : MonoBehaviour
{
    Resolution[] resoluciones;
    public TMP_Dropdown resDropdown, calidadDropdown;
    public Toggle togleFullscren;
    public Slider sliderVolumen;

    int resolucionActual;

    // Start is called before the first frame update
    public void Start()
    {

        PonerPreferencias();

        //AudioListener.volume = sliderVolumen.value;

        resoluciones = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> opciones = new List<string>();
        resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            //string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            string opcion = resoluciones[i].ToString();

            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }

        resDropdown.AddOptions(opciones);
        resDropdown.value = resolucionActual;
        resDropdown.RefreshShownValue();

        if (Screen.fullScreen)
        {
            togleFullscren.isOn = true;
        }
        else
        {
            togleFullscren.isOn = false;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarVolumen(float valor)
    {
        PlayerPrefs.SetFloat("volumenGeneral", valor);
        AudioListener.volume = valor;

    }

    public void SetCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Calidad", index);
    }

    public void SetPantallaResolucion(int index)
    {
        Resolution resolucion = resoluciones[index];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void PonerPreferencias()
    {
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenGeneral", 0.75f);
        calidadDropdown.value = PlayerPrefs.GetInt("Calidad", 3);
    }



}
