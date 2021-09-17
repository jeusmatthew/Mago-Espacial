using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruta_scr : MonoBehaviour
{
    [SerializeField] 
    AudioClip frutaSound;

    [SerializeField]
    bool isFloating;

    //[SerializeField] player_mov player;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        isFloating = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.collider.GetComponent<player_mov>().AñadirManzana();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(frutaSound);
        }
    
        if (isFloating) 
        {
            GetComponent<Rigidbody2D>().gravityScale = 5;
            isFloating = false;
        }

    }




}
