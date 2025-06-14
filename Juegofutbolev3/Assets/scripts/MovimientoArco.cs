using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoArco : MonoBehaviour
{
    public float velocidadOscilacion = 2f;
    public float amplitudMovimiento = 3f;

    private Vector3 puntoInicio;
    private bool arcoEnMovimiento;

    void Start()
    {
        puntoInicio = transform.position;

        arcoEnMovimiento = PlayerPrefs.GetInt("val_moverArco", 0) == 1;
    }

    void Update()
    {
        if (arcoEnMovimiento)
        {
            float desplazamientoX = Mathf.Sin(Time.time * velocidadOscilacion) * amplitudMovimiento;
            transform.position = new Vector3(puntoInicio.x + desplazamientoX, puntoInicio.y, puntoInicio.z);
        }
    }
}
