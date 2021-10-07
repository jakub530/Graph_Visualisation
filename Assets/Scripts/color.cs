using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    private Renderer rend;

    [SerializeField]
    private Color colorToChange = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = colorToChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
