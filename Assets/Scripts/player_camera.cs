using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_camera : MonoBehaviour
{
    [SerializeField]
    GameObject player, baculo, gameOverUI, gameOverFinalUI;

    public GameObject guiMenu, guiConfig;

    public float offset;

    new AudioSource audio;

    [SerializeField] 
    AudioClip gameOver, musicaFinal, musicaGameOver;



    public void GameOver()
    {
        player.transform.position = new Vector3(player.transform.position.x, 0);
        audio.Stop();

        audio.PlayOneShot(musicaGameOver);
        audio.PlayOneShot(gameOver);
        player.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void GameOverFinal()
    {
        player.transform.position = new Vector3(player.transform.position.x, 0);
        audio.Stop();
        audio.PlayOneShot(musicaFinal);

        player.SetActive(false);
        baculo.SetActive(false);
        gameOverFinalUI.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        else
        {
            transform.position = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);
        }


        // Si el jugador se cae posicionarlo a un nuevo lugar y ocultarlo
        if (player.transform.position.y < -8)
        {
            player.GetComponent<player_mov>().Daño();
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (guiMenu.activeInHierarchy)
            {
                guiMenu.SetActive(false);
                Time.timeScale = 1;
                audio.UnPause();
            }
            else if (guiConfig.activeInHierarchy)
            {
                //guiMenu.SetActive(true);
                //Time.timeScale = 0;
                guiConfig.SetActive(false);
                Time.timeScale = 1;
                audio.UnPause();
            }
            else
            {
                guiMenu.SetActive(true);
                Time.timeScale = 0;

                audio.Pause();
            }



        }



        //Debug.Log(GameObject.FindGameObjectWithTag("Baculo").transform.position);

    }
}
