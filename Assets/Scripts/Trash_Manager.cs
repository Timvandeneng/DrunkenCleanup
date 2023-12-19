using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trash_Manager : MonoBehaviour
{
    [Header("SpawnPhysics")]
    public Transform[] boundingbox;
    public GameObject trashpile;
    public GameObject waterPile;
    public GameObject BigTrash;
    public bool willspawnTrash, willspawnPuddles, willspawnBigTrash;
    public int minTrash, maxTrash, minpuddle, maxpuddle, minBigTrash, maxBigTrash;

    [Header("Ground Physisc")]
    public Transform ground;
    public float groundOffset;

    [Header("Trash Physics")]
    public float SuctionSpeed;
    public float Maxpuddlesize;
    public float MinPuddleSize;
    GameObject curWater;

    [Header("Game Physics")]
    public float WinPercentage;
    public GameObject WinScreen;
    public GameObject NormalUI;

    [HideInInspector]
    public float currentSmallTrashAmount;
    float StartTrash = 0;
    float StartWater = 0;
    float StartBigTrash = 0;
    [HideInInspector]
    public float currentWaterAmount;
    public float currentBigTrashAmount;
    public float CurrentShardsAmmount;
    public float MaxShards;

    [Header("U.I. Elements")]
    public TextMeshProUGUI PercentText;
    public Image LoadingBar;

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
    }

    void SpawnTrash()
    {
            StartTrash = Random.Range(minTrash, maxTrash);

            for (int i = 0; i < StartTrash; i++)
            {
                currentSmallTrashAmount = StartTrash;
                int WhichBox = Random.Range(0, boundingbox.Length);
                Vector3 trashPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (ground.transform.position.y + groundOffset), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
                Instantiate(trashpile, trashPos, Quaternion.identity);
            }
    }

    void SpawnPuddles()
    {
        StartWater = Random.Range(minpuddle, maxpuddle);

        for (int i = 0; i < StartWater; i++)
        {
            currentWaterAmount = StartWater;
            int WhichBox = Random.Range(0, boundingbox.Length);
            Vector3 waterPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (ground.transform.position.y + groundOffset), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
            curWater = Instantiate(waterPile, waterPos, Quaternion.identity);
            float scale = Random.Range(1f, 2f);
            curWater.transform.localScale = new Vector3(scale, 1, scale);
        }
    }

    void SpawnBigTrash()
    {
        StartBigTrash = Random.Range(minBigTrash, maxBigTrash);

        for(int i = 0; i < StartBigTrash; i++)
        {
            currentBigTrashAmount = StartBigTrash;
            int WhichBox = Random.Range(0, boundingbox.Length);
            Vector3 bigTrashPos = new Vector3(Random.Range(boundingbox[WhichBox].position.x - (boundingbox[WhichBox].localScale.x / 2), boundingbox[WhichBox].position.x + (boundingbox[WhichBox].localScale.x / 2)), (ground.transform.position.y + groundOffset), Random.Range(boundingbox[WhichBox].position.z - (boundingbox[WhichBox].localScale.z / 2), boundingbox[WhichBox].position.z + (boundingbox[WhichBox].localScale.z / 2)));
            Instantiate(BigTrash, bigTrashPos, Quaternion.identity);
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
        return ((100 / (StartTrash + StartWater + StartBigTrash)) * (currentSmallTrashAmount + currentWaterAmount + currentBigTrashAmount + CurrentShardsAmmount));
    }

    void AllTrashCleaned()
    {
        NormalUI.SetActive(false);
        WinScreen.SetActive(true);
    }
}
