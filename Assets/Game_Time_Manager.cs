using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Time_Manager : MonoBehaviour
{
    [Header("Game physics")]
    public float GameMinutes;
    float GameTime;

    [Header("Atttributes")]
    public TextMeshProUGUI Minutes;
    public TextMeshProUGUI Seconds;

    // Start is called before the first frame update
    void Start()
    {
        GameTime = GameMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;

        Minutes.text = "" + GameTime;
        Seconds.text = "" + (GameTime / 60);
    }
}
