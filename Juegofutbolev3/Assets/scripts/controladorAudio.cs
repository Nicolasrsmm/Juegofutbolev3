using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] Sounds; // [0] Juego, [1] Perdiste
    public Slider slider;

    void Start()
    {
        // Obtener volumen guardado o valor por defecto
        float volumen = PlayerPrefs.GetFloat("val_volumen", 0.3f);
        audioSource.volume = volumen;

        if (slider != null)
            slider.value = volumen;

        loadClip(0); // Música del juego por defecto
    }

    public void setAudio()
    {
        float volumen = slider.value;
        audioSource.volume = volumen;
        PlayerPrefs.SetFloat("val_volumen", volumen);
    }

    public void loadClip(int indice)
    {
        if (indice >= 0 && indice < Sounds.Length)
        {
            audioSource.Stop();
            audioSource.clip = Sounds[indice];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Índice de clip inválido.");
        }
    }

    // Método para aplicar volumen desde otro script (opcional)
    public void AplicarVolumenGuardado(float volumen)
    {
        audioSource.volume = volumen;
        if (slider != null)
            slider.value = volumen;
    }
}

