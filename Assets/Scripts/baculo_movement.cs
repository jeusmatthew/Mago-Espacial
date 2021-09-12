using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class baculo_movement : MonoBehaviour
{
    //[SerializeField] 
    //GameObject player;

    [SerializeField]
    private player_camera jugadorCamaraScr;

    public int tamañoMaximo;

    [SerializeField] 
    AudioClip baculoSoundOff;

    [SerializeField]
    private TMP_Text escalaUI;

    public int multiplicador;

    public float tiempo;
    public int tiempoEntero;

    public bool CreceSolo;

    //private float offsetB;
    
    public void DesactivarBaculo()
    {
        gameObject.transform.position = Vector3.zero;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(baculoSoundOff);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        tiempo = multiplicador;
        escalaUI.text = "tam x " + tiempo;
    }

    // Start is called before the first frame update
    void Start()
    {
        //offsetB = Camera.main.GetComponent<player_camera>().offset;
        
    }

    // Update is called once per frame
    void Update()
    {




        //transform.localScale = new Vector3(player.GetComponent<player_mov>().energia * multiplicador, transform.localScale.y);


        // Si se esta moviendo, que crezca
        //if (Input.GetKey(KeyCode.C) && !CreceSolo)
        //{
        //    tiempo += Time.deltaTime * multiplicador;
        //}
        //else if (CreceSolo)
        //{
        //    tiempo += Time.deltaTime * multiplicador;
        //}

        //// Esto acumula el tiempo que pasa
        tiempo += Time.deltaTime * multiplicador;

        // Lo redondea para que muestre los segundos en entero
        tiempoEntero = Mathf.FloorToInt(tiempo);

        //Debug.Log(tamañoMaximo - (tiempoEntero * multiplicador));

        escalaUI.text = "tam x " + (tiempoEntero);


        // Convierte el tamaño en el tiempo pasado segun el multiplicador 
        transform.localScale = new Vector3(tiempo, transform.localScale.y);



        // Si la escala llega a un punto maximo explota XDD
        if (gameObject.transform.localScale.x > tamañoMaximo)
        {
            jugadorCamaraScr.GameOver();
            gameObject.SetActive(false);
        }
        


        // Si no lo vez, vuelve a ti
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            DesactivarBaculo();
        }

    }


}
