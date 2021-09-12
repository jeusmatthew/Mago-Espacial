using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Salir()
    {
        Debug.Log("Saliendo . . . ");
        Application.Quit();
    }

    public void cargarEscena(int index)
    {
        SceneManager.LoadScene(index);
    }


}
