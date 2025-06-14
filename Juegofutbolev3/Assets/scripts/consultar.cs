using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consultar : MonoBehaviour
{
    public Data data;
    void Start()
    {
        data = FindObjectOfType<Data>();

        string nombreGuardado = PlayerPrefs.GetString("val_nombre", "").Trim();
        if (string.IsNullOrEmpty(nombreGuardado))
        {
            nombreGuardado = "Player";
            PlayerPrefs.SetString("val_nombre", nombreGuardado);
        }

        bool moverArco = PlayerPrefs.GetInt("val_moverArco", 0) == 1;
        float volumen = PlayerPrefs.GetFloat("val_volumen", 0.3f); // valor por defecto

        Debug.Log("Nombre: " + nombreGuardado);
        Debug.Log("Mover arco?: " + moverArco);
        Debug.Log("Volumen: " + volumen);
    }
}
