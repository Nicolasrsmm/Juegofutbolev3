using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    public TMPro.TMP_InputField input;
    public Toggle toggleDificultad;
    public Slider slider;
    public AudioSource audioSource;



    private void Start()
    {
        RestaurarPreferencias();
    }

    public void getinput()
    {
        string name = input.text;
        Debug.Log("El nombre es: " + name);
        PlayerPrefs.SetString("val_nombre", name);
    }

    public void getToggle()
    {
        bool moverArco = toggleDificultad.isOn;
        Debug.Log("Mover arco: " + moverArco);
        PlayerPrefs.SetInt("val_moverArco", moverArco ? 1 : 0); // Guardar como entero
    }

    public void setAudio()
    {
        float volumen = slider.value;
        audioSource.volume = volumen;
        PlayerPrefs.SetFloat("val_volumen", volumen);
    }

    public void RestaurarPreferencias()
    {
        // Nombre
        if (PlayerPrefs.HasKey("val_nombre"))
        {
            input.text = PlayerPrefs.GetString("val_nombre");
        }

        // Toggle
        if (PlayerPrefs.HasKey("val_moverArco"))
        {
            int moverArco = PlayerPrefs.GetInt("val_moverArco");
            toggleDificultad.isOn = moverArco == 1;
        }

        // Volumen
        if (PlayerPrefs.HasKey("val_volumen"))
        {
            float volumen = PlayerPrefs.GetFloat("val_volumen");
            slider.value = volumen;
            audioSource.volume = volumen;
        }
        else
        {
            setAudioInicial();
        }
    }

    public void setAudioInicial()
    {
        float volumen = 0.3f;
        slider.value = volumen;
        audioSource.volume = volumen;
        PlayerPrefs.SetFloat("val_volumen", volumen);
    }
}
