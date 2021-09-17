using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver_anim : MonoBehaviour
{
    [SerializeField]
    private player_mov playerScr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main.GetComponent<player_camera>().guiMenu.activeInHierarchy &&
            !Camera.main.GetComponent<player_camera>().guiConfig.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("reiniciado?");

                //SceneManager.LoadScene(0);

            }
        }
        

    }

}
