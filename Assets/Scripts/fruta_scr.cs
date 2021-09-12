using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruta_scr : MonoBehaviour
{
    [SerializeField] AudioClip frutaSound;

    //[SerializeField] player_mov player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.collider.GetComponent<player_mov>().A�adirManzana();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(frutaSound);
        }


    }




}
