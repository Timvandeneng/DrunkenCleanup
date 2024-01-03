using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrashAmountModifier : MonoBehaviour
{
    public Slider FriendsAmount;
    public TextMeshProUGUI Display;

    void Start()
    {

    }


    void Update()
    {
        Display.text = FriendsAmount.value.ToString();
    }
}