using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax_backgrounf : MonoBehaviour
{
    [SerializeField] Transform player;
    float xd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        else
        {
            transform.position = new Vector3(player.position.x * 1.5f, transform.position.y, transform.position.z);
        }
    }
}
