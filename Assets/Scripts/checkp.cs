using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkp : MonoBehaviour
{
    [SerializeField] Transform respawn;

    [SerializeField]
    AudioClip checkpointClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawn.position = transform.position;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(checkpointClip);
            gameObject.SetActive(false);
        }
    }

}
