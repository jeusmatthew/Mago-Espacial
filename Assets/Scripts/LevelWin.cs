using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
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
                LoadNextLevel();
            }
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
