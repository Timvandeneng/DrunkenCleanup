using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeerSettings : MonoBehaviour
{
    public Slider DrunknessLevel;
    public TextMeshProUGUI Display;

    void Start()
    {
        
    }


    void Update()
    {
        Display.text = DrunknessLevel.value.ToString();
    }
}
