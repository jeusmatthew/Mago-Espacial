using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class player_mov : MonoBehaviour
{
    [SerializeField] 
    float baseVelocity, jumpForce, raySize, playerVelocity, aceleracion, desaceleracion;

    [SerializeField] 
    Rigidbody2D playerRigidBody2D, baculoRG;
    
    [SerializeField] 
    SpriteRenderer playerSprite;

    [SerializeField]
    Animator playerAnimator;

    [SerializeField] 
    GameObject baston, escalaUIObject;

    [SerializeField] 
    Transform respawn;
    
    [SerializeField] 
    AudioClip jump_sound, baculoSoundOn, baculoSoundOff, gameOver, hitAudio;

    [SerializeField]
    TMP_Text vidaUI, manzanasUI, escalaUI;

    [SerializeField]
    TMP_FontAsset bastonOnFont;

    [SerializeField]
    Material material;

    [SerializeField]
    bool canJump, inputJumping, running, walking, debugMode, rayCanJump;
    
    [SerializeField] 
    int vida = 1;

    public int totalManzanas;

    [SerializeField]
    Vector2 playerMovement, bastonMovement;

    [SerializeField]
    Vector3 colliderOffset;


    private void Awake()
    {
        vidaUI.text = "jug x " + vida;
        manzanasUI.text = "man x " + totalManzanas;
    }

    // Start is called before the first frame update
    void Start()
    {

        ResetPlayer();

        if (debugMode)
        {
            Debug.LogWarning("El debug esta activo");
        }
               
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main.GetComponent<player_camera>().guiMenu.activeInHierarchy && !Camera.main.GetComponent<player_camera>().guiConfig.activeInHierarchy)
        {

            // DEBUG Controles
            if (debugMode)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    Debug.LogWarning("Recargado");
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    AñadirManzana();
                    Debug.LogWarning("Añadido manzana");
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    baston.GetComponent<baculo_movement>().tiempo = baston.GetComponent<baculo_movement>().multiplicador;
                    Debug.LogWarning("Reseteado baston");
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Daño();
                }

            }

            rayCanJump = 
                Physics2D.Raycast(transform.position, Vector3.down, raySize) || 
                Physics2D.Raycast(transform.position + colliderOffset, Vector3.down, raySize) ||
                Physics2D.Raycast(transform.position - colliderOffset, Vector3.down, raySize);

            // Si el rayo toca el suelo, puedes saltar sino, gravedad++
            Debug.DrawRay(transform.position, Vector3.down * raySize, Color.red);
            Debug.DrawRay(transform.position + colliderOffset, (Vector3.down * raySize), Color.red);
            Debug.DrawRay(transform.position - colliderOffset, (Vector3.down * raySize), Color.red);



            //if (rayCanJump)
            //{
            //    canJump = true;
            //}
            //else
            //{
            //    canJump = false;
            //}

            // Si va a la izq o der, que se gire sies necesario
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                playerSprite.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                playerSprite.flipX = false;
            }

            // Si puede saltar y presiona z que salte pq no?
            if (Input.GetKeyDown(KeyCode.Z) && rayCanJump)
            {
                inputJumping = true;
            }

            // Si esta presionando el boton de caminar, pues esta caminando
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                walking = true;

            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                walking = false;

            }

            // Si se mantiene x y esta caminando, permitirle correr 
            if (Input.GetKey(KeyCode.X) && walking)
            {
                // ClampGOD, acelera hasta un maximo
                playerVelocity = Mathf.Clamp(playerVelocity += (aceleracion * Time.deltaTime), baseVelocity, baseVelocity * 1.5f);
                running = true;
            }
            else
            {
                // Desacelera hasta un minimo
                playerVelocity = Mathf.Clamp(playerVelocity -= (desaceleracion * Time.deltaTime), baseVelocity, baseVelocity * 1.5f);
                running = false;
            }

            // cambia las animaciones segun los booleanos
            playerAnimator.SetBool("anim_walking", walking);
            playerAnimator.SetBool("running", running);

            // Codigo antiguo
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
            // Si presionas C lo activas
            if (Input.GetKeyDown(KeyCode.C))
            {
                // Que lo posicione reiniciando su posicion y rotacion
                baston.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.Euler(Vector3.zero));


                if (!baston.activeInHierarchy)
                {

                    ActivarBaculo();
                }
                else
                {
                    baston.GetComponent<baculo_movement>().DesactivarBaculo();
                }

            }

            // Si lo mantienes lo mueves
            if (Input.GetKey(KeyCode.C) && baston.activeInHierarchy)
            {
                //playerRigidBody2D.velocity = new Vector2(0, playerRigidBody2D.velocity.y);
                //baculoRG.velocity = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, Input.GetAxis("Vertical") * playerVelocity);

                // A el jugador lo dejas quieto, haces que el baston flote y lo puedas mover Nice
                baculoRG.angularVelocity = 0;
                baculoRG.gravityScale = 0;
                playerMovement = new Vector2(0, playerRigidBody2D.velocity.y);
                bastonMovement = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, Input.GetAxis("Vertical") * playerVelocity);
            }
            else
            {
                // Sino mueve al jugador y resetea la gravedad del baston
                //playerRigidBody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, playerRigidBody2D.velocity.y);
                playerMovement = new Vector2(Input.GetAxis("Horizontal") * playerVelocity, playerRigidBody2D.velocity.y);
                bastonMovement = new Vector2(baculoRG.velocity.x, baculoRG.velocity.y);
                baculoRG.gravityScale = 5;
            }
        }

    }


    private void FixedUpdate()
    {
        playerRigidBody2D.velocity = playerMovement;
        baculoRG.velocity = bastonMovement;

        if (inputJumping)
        {
            inputJumping = false;
            Jump();
        }


        // Codigo antiguo
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
        if (collision.CompareTag("Finish"))
        {
            Camera.main.GetComponent<player_camera>().GameOverFinal();
        }
    }

    private void Jump()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(jump_sound);
        playerRigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //if (running)
        //{
        //    playerRigidBody2D.AddForce(Vector2.up * (jumpForce * 1.20f));
        //}
        //else
        //{
        //    playerRigidBody2D.AddForce(Vector2.up * jumpForce);
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
            Hit();
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

        int tamañoMinimo = baston.GetComponent<baculo_movement>().multiplicador; 
        int tamañoMaximo = baston.GetComponent<baculo_movement>().tamañoMaximo;
        float tiempoReducido;

        // Le reduzco el tiempo sin procesar
        tiempoReducido = baston.GetComponent<baculo_movement>().tiempo -= totalManzanas;

        // Le asigno el tiempo reducido procesado con minimo y maximo
        baston.GetComponent<baculo_movement>().tiempo = Mathf.Clamp(tiempoReducido, tamañoMinimo, tamañoMaximo); 

        // Actualiza el tiempo en la pantalla
        escalaUI.text = "tam x " + Mathf.Floor(baston.GetComponent<baculo_movement>().tiempo);

    }

    public void ActivarBaculo()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(baculoSoundOn);
        baston.SetActive(true);
        escalaUI.font = bastonOnFont;
        escalaUI.color = Color.white;
        escalaUI.fontMaterial = material;
        //escalaUI.color = Color.white;
    }

    public void Hit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hitAudio);
        ResetPlayer();
    }

    public void ResetPlayer()
    { 
        gameObject.SetActive(true);

        playerVelocity = baseVelocity;

        if (respawn != null)
        {
            transform.SetPositionAndRotation(respawn.position + Vector3.up, Quaternion.Euler(Vector3.zero));
        }
    }
    //public void GameOver()
    //{
    //    Camera.main.GetComponent<AudioSource>().PlayOneShot(gameOver);
    //    gameObject.SetActive(false);
    //}


}
