using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title_screen : MonoBehaviour
{
    [SerializeField]
    opcionesScr opciones;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        opciones.PonerPreferencias();
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    SceneManager.LoadScene(1);
        //}
    }



}
