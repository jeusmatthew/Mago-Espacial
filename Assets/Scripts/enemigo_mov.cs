using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_mov : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] Rigidbody2D enemigo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        enemigo.velocity = new Vector2(velocidad * Time.deltaTime, enemigo.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<player_mov>().Daño();
        }
    }

}
