using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class player_mov : MonoBehaviour
{
    [SerializeField] float velocidad_base;
    [SerializeField] float fuerza_salto;
    [SerializeField] float longitud_rayo;

    [SerializeField] Rigidbody2D player;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float velocidad;

    [SerializeField]
    float aceleracion, desaceleracion;

    [SerializeField] GameObject baculo;
    [SerializeField] Rigidbody2D baculoRG;
    [SerializeField] Animator player_anim;

    [SerializeField] Transform respawn;
    
    [SerializeField] AudioClip jump_sound;
    [SerializeField] AudioClip baculoSoundOn;
    [SerializeField] AudioClip baculoSoundOff;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip hitAudio;

    //[SerializeField] Text energiasText;
    //[SerializeField] Text vidasText;

    [SerializeField]
    TMP_Text vidaUI, manzanasUI, escalaUI;

    //[SerializeField]
    //GameObject baston;

    bool esta_tocando_suelo;
    [SerializeField] bool running;
    [SerializeField] bool walking;
    
    [SerializeField] 
    int vida = 1;


    public int totalManzanas;

    private void Awake()
    {
        vidaUI.text = "jug x " + vida;
        manzanasUI.text = "man x " + totalManzanas;
    }

    // Start is called before the first frame update
    void Start()
    {
        velocidad = velocidad_base;
        transform.position = respawn.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main.GetComponent<player_camera>().guiMenu.activeInHierarchy && !Camera.main.GetComponent<player_camera>().guiConfig.activeInHierarchy)
        {

            Debug.DrawRay(transform.position, Vector3.down * longitud_rayo, Color.red);
            if (Physics2D.Raycast(transform.position, Vector3.down, longitud_rayo))
            {
                esta_tocando_suelo = true;
            }
            else
            {
                esta_tocando_suelo = false;
            }

            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                sprite.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                sprite.flipX = false;
            }

            if (Input.GetKeyDown(KeyCode.Z) && esta_tocando_suelo)
            {
                Jump();
            }

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                walking = true;

            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                walking = false;

            }

            player_anim.SetBool("anim_walking", walking);
            player_anim.SetBool("running", running);

            if (Input.GetKey(KeyCode.X) && walking)
            {
                velocidad = Mathf.Clamp(velocidad += (aceleracion * Time.deltaTime), velocidad_base, velocidad_base * 1.5f);
                running = true;
            }
            else
            {
                running = false;
                velocidad = Mathf.Clamp(velocidad -= (desaceleracion * Time.deltaTime), velocidad_base, velocidad_base * 1.5f);
            }

            //if (Input.GetKey(KeyCode.X) && walking)
            //{
            //    if (velocidad < velocidad_base * 1.5)
            //    {
            //        //velocidad = velocidad_base * 2f;
            //        velocidad += 0.1f;
            //        running = true;
            //    }

            //}
            //else
            //{
            //    running = false;
            //    if (velocidad > velocidad_base)
            //    {
            //        velocidad -= 0.1f;
            //    }
            //}



            // Codigo del baculo
            if (Input.GetKeyDown(KeyCode.C))
            {
                baculo.transform.SetPositionAndRotation(new Vector3(player.position.x, player.position.y, 0), Quaternion.Euler(Vector3.zero));

                if (baculo.activeInHierarchy)
                {
                    baculo.GetComponent<baculo_movement>().DesactivarBaculo();
                    //escalaUI.text = "estado\n";
                    //baston.SetActive(false);
                }
                //else if (energia > 0)
                else
                {
                    ActivarBaculo();
                }

            }

            if (Input.GetKey(KeyCode.C) && baculo.activeInHierarchy)
            {
                player.velocity = new Vector2(0, player.velocity.y); 
                baculoRG.angularVelocity = 0;
                //baculoRG.freezeRotation = true;
                baculoRG.gravityScale = 0;
                baculoRG.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidad, Input.GetAxis("Vertical") * velocidad);
            }
            else
            {
                player.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidad, player.velocity.y);
                baculoRG.gravityScale = 5;
                //baculoRG.freezeRotation = false;
            }
        }

    }


    private void FixedUpdate()
    {
        


        //if (baculo.activeInHierarchy)
        //{
        //    baculoRG.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidad, Input.GetAxis("Vertical") * velocidad);
        //}
        //else
        //{
        //    player.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidad, player.velocity.y);
        //}


        //player.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * velocidad, 0));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Camera.main.GetComponent<player_camera>().GameOverFinal();
        }
    }

    private void Jump()
    {
        //Camera.main.GetComponent<AudioSource>().PlayOneShot(jump_sound);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(jump_sound);
        player.AddForce(Vector2.up * fuerza_salto, ForceMode2D.Impulse);

        //if (running)
        //{
        //    player.AddForce(Vector2.up * (fuerza_salto * 1.20f));
        //}
        //else
        //{
        //    player.AddForce(Vector2.up * fuerza_salto);
        //}
    }

    public void Daño()
    {
        vida--;

        vidaUI.text = "jug x " + vida;

        if (vida == 0)
        {
            Camera.main.GetComponent<player_camera>().GameOver();
        }
        else
        {
            transform.position = respawn.position;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(hitAudio);
        }
    }

    public void AñadirManzana()
    {
        totalManzanas++;
        manzanasUI.text = "man x " + (totalManzanas);
        //baculo.GetComponent<baculo_movement>().tiempo -= baculo.GetComponent<baculo_movement>().multiplicador;

        ReducirEscalaBaston();



    }

    public void ReducirEscalaBaston()
    {

        int tamañoMinimo = baculo.GetComponent<baculo_movement>().multiplicador; 
        int tamañoMaximo = baculo.GetComponent<baculo_movement>().tamañoMaximo;
        float tiempoReducido;

        // Le reduzco el tiempo sin procesar
        tiempoReducido = baculo.GetComponent<baculo_movement>().tiempo -= totalManzanas;

        // Le asigno el tiempo reducido procesado con minimo y maximo
        baculo.GetComponent<baculo_movement>().tiempo = Mathf.Clamp(tiempoReducido, tamañoMinimo, tamañoMaximo); 

        // Actualiza el tiempo en la pantalla
        escalaUI.text = "tam x " + Mathf.Floor(baculo.GetComponent<baculo_movement>().tiempo);

    }

    public void ActivarBaculo()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(baculoSoundOn);
        baculo.SetActive(true);
        
        //escalaUI.text = "estado\n";
        //energiasText.text = "Energia\n" + (--energia);
        //baston.SetActive(true);
    }

    //public void GameOver()
    //{
    //    Camera.main.GetComponent<AudioSource>().PlayOneShot(gameOver);
    //    gameObject.SetActive(false);
    //}


}
