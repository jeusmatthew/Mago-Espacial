using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrecimientoEscala : MonoBehaviour
{
    [SerializeField]
    float timer;

    [SerializeField]
    [Range(0, 1)]
    private float bastonTimeNormalized;

    [SerializeField]
    private baculo_movement bastonScr;

    [SerializeField]
    private TMP_Text ui;

    public Gradient gradiante, sombraGradiante;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        bastonTimeNormalized = (bastonScr.tiempo - bastonScr.multiplicador) / (bastonScr.tamañoMaximo - bastonScr.multiplicador);

        transform.localScale = new Vector3(bastonTimeNormalized + 1, bastonTimeNormalized + 1);


        //Debug.Log(Mathf.Clamp01(bastonScr.tiempo));

        //ui.color = Color.Lerp(Color.Lerp(Color.Lerp(Color.white, Color.green, lerpColor), Color.yellow, lerpColor), Color.red, lerpColor);

        //Debug.Log(bastonScr.tiempo);

    }

    private void ChangeColor()
    {
        // ResultadoNormalizado = valor - minimo / maximo - minimo
        ui.color = gradiante.Evaluate(bastonTimeNormalized);
        //ui.outlineColor = sombraGradiante.Evaluate(lerpColor);
    }
}
