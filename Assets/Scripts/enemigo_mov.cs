using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_mov : MonoBehaviour
{
    [SerializeField] 
    float velocidad;

    [SerializeField] 
    Rigidbody2D enemyRigidBody;

    [SerializeField]
    Vector2 direction;

    [SerializeField]
    int directionInt;

    private Renderer enemySprite;

    [SerializeField]
    private bool isStaticFirstTime;

    [SerializeField]
    private AudioClip aplastadoClip;

    private void Awake()
    {
        isStaticFirstTime = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySprite.isVisible)
        {
            isStaticFirstTime = false;
        }

        if (transform.position.x < -8)
        {
            Destruido();
        }

    }

    private void FixedUpdate()
    {
        //enemigo.velocity = new Vector2(velocidad * Time.deltaTime, enemigo.velocity.y);

        if (!isStaticFirstTime)
        {
            enemyRigidBody.velocity = new Vector2(directionInt * velocidad, enemyRigidBody.velocity.y);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<player_mov>().Daño();
        }

        if (collision.gameObject.CompareTag("Baculo"))
        {
            float bastonMass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            float enemyMass = enemyRigidBody.mass;

            if (bastonMass > enemyMass)
            {
                Destruido();
            }

        }

    }


    private void Destruido()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(aplastadoClip);
        Destroy(gameObject);
    }

}
