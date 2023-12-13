using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Time_Manager : MonoBehaviour
{
    [Header("Game physics")]
    public float TimeinMinutes;
    [HideInInspector]
    public float GameTime;

    [Header("Atttributes")]
    public TextMeshProUGUI TimeUI;
    // Start is called before the first frame update
    void Start()
    {
        GameTime = TimeinMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameTime > 0)
        {
            GameTime -= Time.deltaTime;
        }
        else
        {
            GameTime = 0;
        }
        DisplayTime();

    }

    void DisplayTime()
    {

        if(GameTime < 0)
        {
            GameTime = 0;
        }
        float minutes = Mathf.FloorToInt(GameTime / 60);
        float seconds = Mathf.FloorToInt(GameTime % 60);

        TimeUI.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}
