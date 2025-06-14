using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatearPelota : MonoBehaviour
{
    public Rigidbody cuerpoBalon;
    public float tiempoMaxCarga = 3f;
    public float multiplicadorFuerza = 10f;

    public GameObject panelAnotacion;
    public GameObject panelGanador;
    public Transform posicionInicio;
    public TextMeshProUGUI textoGanador;
    public GameObject efectoParticulas;
    public controladorAudio gestorSonido;

    private float tiempoCarga = 0f;
    private bool estaCargando = false;
    private bool puedeDisparar = true;
    private int cantidadGoles = 0;
    private const int golesNecesarios = 3;
    private bool esperandoReinicio = false;

    void Start()
    {
        panelAnotacion.SetActive(false);
        panelGanador.SetActive(false);
        ResetearBalon();
    }

    void Update()
    {
        if (puedeDisparar && !esperandoReinicio)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                estaCargando = true;
                tiempoCarga = 0f;
            }

            if (Input.GetKey(KeyCode.Space) && estaCargando)
            {
                tiempoCarga += Time.deltaTime;
                tiempoCarga = Mathf.Clamp(tiempoCarga, 0f, tiempoMaxCarga);
            }

            if (Input.GetKeyUp(KeyCode.Space) && estaCargando)
            {
                estaCargando = false;
                float fuerzaFinal = tiempoCarga * multiplicadorFuerza;

                cuerpoBalon.velocity = Vector3.zero;
                cuerpoBalon.angularVelocity = Vector3.zero;
                cuerpoBalon.AddForce(transform.forward * fuerzaFinal, ForceMode.Impulse);

                gestorSonido.loadClip(2);

                puedeDisparar = false;
                esperandoReinicio = true;
                StartCoroutine(EsperarIntentoFallido(3f));
            }
        }
    }

    private void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Arco") && esperandoReinicio)
        {
            cantidadGoles++;

            if (cantidadGoles >= golesNecesarios)
            {
                MostrarGanador();
            }
            else
            {
                StartCoroutine(MostrarPanelAnotacion());
            }

            esperandoReinicio = false;
            StartCoroutine(ReiniciarDespuesDe(2f));
        }
    }

    IEnumerator MostrarPanelAnotacion()
    {
        gestorSonido.loadClip(1);
        panelAnotacion.SetActive(true);
        yield return new WaitForSeconds(2f);
        panelAnotacion.SetActive(false);
    }

    void MostrarGanador()
    {
        panelGanador.SetActive(true);

        string nombreJugador = PlayerPrefs.GetString("val_nombre", "Jugador");

        if (textoGanador != null)
        {
            textoGanador.text = "¡Waaaaaoooo! ¡Felicidades, hiciste un hat-trick increíble! " + nombreJugador + "! Ganaste el juego.";
        }

        if (efectoParticulas != null)
        {
            efectoParticulas.SetActive(true);
            StartCoroutine(DesactivarEfecto(10f));
        }

        gestorSonido.loadClip(3);

        cuerpoBalon.velocity = Vector3.zero;
        cuerpoBalon.angularVelocity = Vector3.zero;
    }

    IEnumerator DesactivarEfecto(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        efectoParticulas.SetActive(false);
    }

    IEnumerator EsperarIntentoFallido(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (esperandoReinicio)
        {
            esperandoReinicio = false;
            ResetearBalon();
        }
    }

    IEnumerator ReiniciarDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        ResetearBalon();
    }

    void ResetearBalon()
    {
        cuerpoBalon.velocity = Vector3.zero;
        cuerpoBalon.angularVelocity = Vector3.zero;
        transform.position = posicionInicio.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        puedeDisparar = true;
        esperandoReinicio = false;
    }
}

