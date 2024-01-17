using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trash_Manager : MonoBehaviour
{
    [Header("SpawnPhysics")]
    public Transform[] boundingbox;
    public Transform[] Parents; //This is a WRONG way of doing things!! i'm doing this because of a deadline :'(
    public GameObject trashpile;
    public GameObject waterPile;
    public GameObject[] BigTrash;
    public GameObject Human;
    public bool willspawnTrash, willspawnPuddles, willspawnBigTrash, willspawnHumans;
    public int minTrash, maxTrash, minpuddle, maxpuddle, minBigTrash, maxBigTrash, minHuman, maxHuman;

    [Header("Ground Physisc")]
    public Transform ground;
    public float groundOffset;

    [Header("Trash Physics")]
    public float SuctionSpeed;
    public float Maxpuddlesize;
    public float MinPuddleSize;

    [Header("Game Physics")]
    public float WinPercentage;
    public int SmallTrashValue, Bigtrashvalue, WaterPuddleValue, HumanValue;

    [Header("Misc")]
    [HideInInspector]
    public float currentSmallTrashAmount;
    float StartTrash = 0;
    float StartWater = 0;
    float StartBigTrash = 0;
    float StartHumans = 0;
    [HideInInspector]
    public float currentWaterAmount;
    public float currentBigTrashAmount;
    public float CurrentShardsAmmount;
    public float currentHumanAmmount;
    public float MaxShards;

    Game_Time_Manager time;

    public float WinReturnToMain;
    Pause_Game_Handler pause;

    [Header("U.I. Elements")]
    public TextMeshProUGUI PercentText;
    public Image LoadingBar;

    public Transform Topfloor, BottomFloor, Idle;

    void Start()
    {
        if (willspawnTrash)
        {
            SpawnTrash();
        }
        if (willspawnPuddles)
        {
            SpawnPuddles();
        }
        if (willspawnBigTrash)
        {
            SpawnBigTrash();
        }
        if (willspawnHumans)
        {
            SpawnHumans();
        }

        pause = FindFirstObjectByType<Pause_Game_Handler>();
        time = GetComponent<Game_Time_Manager>();
    }

    void SpawnTrash()
    {
            StartTrash = Random.Range(minTrash, maxTrash);
            StartTrash = StartTrash * SmallTrashValue;

            for (int i = 0; i < StartTrash / SmallTrashValue; i++)
            {
                currentSmallTrashAmount = StartTrash;
                int WhichBox = Random.Range(0, boundingbox.Length);
                Vector3 trashPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (boundingbox[WhichBox].position.y), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
                GameObject instance = Instantiate(trashpile, trashPos, Quaternion.identity);
                instance.transform.parent = Parents[WhichBox];
            }
    }

    void SpawnPuddles()
    {
        StartWater = Random.Range(minpuddle, maxpuddle);
        StartWater = StartWater * WaterPuddleValue;
        for (int i = 0; i < StartWater / WaterPuddleValue; i++)
        {
            currentWaterAmount = StartWater;
            int WhichBox = Random.Range(0, boundingbox.Length);
            Vector3 waterPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (boundingbox[WhichBox].position.y), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
            GameObject curWater = Instantiate(waterPile, waterPos, Quaternion.identity);
            float scale = Random.Range(1f, 2f);
            curWater.transform.localScale = new Vector3(scale, 1, scale);
            curWater.transform.parent = Parents[WhichBox];
        }
    }

    void SpawnBigTrash()
    {
        StartBigTrash = Random.Range(minBigTrash, maxBigTrash);
        //make sure we multiply our bigtrash by the value that it's worth.
        //also divide in the for loop so we still keep the ammount the same
        StartBigTrash = StartBigTrash * Bigtrashvalue;

        for(int i = 0; i < StartBigTrash / Bigtrashvalue; i++)
        {
            currentBigTrashAmount = StartBigTrash;
            int WhichBox = Random.Range(0, boundingbox.Length);
            Vector3 bigTrashPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (boundingbox[WhichBox].position.y + groundOffset), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
            int spawn = Random.Range(0, BigTrash.Length);
            GameObject instance = Instantiate(BigTrash[spawn], bigTrashPos, Quaternion.identity);
            instance.transform.parent = Parents[WhichBox];
        }
    }

    void SpawnHumans()
    {
        StartHumans = Random.Range(minHuman, maxHuman);

        StartHumans = StartHumans * HumanValue;

        for (int i = 0; i < StartHumans / HumanValue; i++)
        {
            currentHumanAmmount = StartHumans;
            int WhichBox = Random.Range(0, boundingbox.Length);
            Vector3 HumanPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (boundingbox[WhichBox].position.y + groundOffset * 2), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
            GameObject instance = Instantiate(Human, HumanPos, Quaternion.identity);
            instance.transform.parent = Parents[WhichBox];
        }
    }

    private void Update()
    {
        //U.I.
        //convert to int for better results
        int percent = (int)PercentageOfTrash();
        PercentText.text = (100 - percent + "%");

        LoadingBar.fillAmount = (100 - PercentageOfTrash()) / 100;

        int redNumber = percent * 2;
        int GreenNumber = 100 - (percent);
        byte red = (byte)redNumber;
        byte Green = (byte)GreenNumber;
        LoadingBar.color = new Color32(red, Green, 0, 255);

        if(PercentageOfTrash() < WinPercentage)
        {
            AllTrashCleaned();
        } 
    }

    float PercentageOfTrash()
    {
        return ((100 / (StartTrash + StartWater + StartBigTrash + StartHumans)) * (currentSmallTrashAmount + currentWaterAmount + currentBigTrashAmount + CurrentShardsAmmount + currentHumanAmmount));
    }

    void AllTrashCleaned()
    {
        time.WinGame();
    }
}
