using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trash_Manager : MonoBehaviour
{
    [Header("SpawnPhysics")]
    public Transform boundingbox;
    public GameObject trashpile;
    public GameObject waterPile;
    public bool willspawnTrash, willspawnPuddles;
    public int minTrash, maxTrash;

    [Header("Ground Physisc")]
    public Transform ground;
    public float groundOffset;

    [Header("Trash Physics")]
    public float SuctionSpeed;

    [HideInInspector]
    public float currentSmallTrashAmount;
    float StartTrash = 0;
    [HideInInspector]
    public float currentWaterAmount;

    [Header("U.I. Elements")]
    public TextMeshProUGUI PercentText;
    public RawImage LoadingBar;
    public GameObject PercentageBar;

    void Start()
    {
        if (willspawnTrash)
        {
            SpawnTrash();
        }
    }

    void SpawnTrash()
    {
            StartTrash = Random.Range(minTrash, maxTrash);

            for (int i = 0; i < StartTrash; i++)
            {
                currentSmallTrashAmount = StartTrash;
                Vector3 trashPos = new Vector3(Random.Range(boundingbox.position.x - (boundingbox.localScale.x / 2), boundingbox.position.x + (boundingbox.localScale.x / 2)), (ground.transform.position.y + groundOffset), Random.Range(boundingbox.position.z - (boundingbox.localScale.z / 2), boundingbox.position.z + (boundingbox.localScale.z / 2)));
                Instantiate(trashpile, trashPos, Quaternion.identity);
            }
    }

    void SpawnPuddles()
    {
        int maxWater = Random.Range(30, 60);

        for (int i = 0; i < maxWater; i++)
        {
            currentWaterAmount = maxWater;
            Vector3 waterPos = new Vector3(Random.Range(boundingbox.position.x - (boundingbox.localScale.x / 2), boundingbox.position.x + (boundingbox.localScale.x / 2)), (ground.transform.position.y), Random.Range(boundingbox.position.z - (boundingbox.localScale.z / 2), boundingbox.position.z + (boundingbox.localScale.z / 2)));
            Instantiate(waterPile, waterPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        //U.I.
        //convert to int for better results
        int percent = (int)PercentageOfTrash();
        PercentText.text = (100 - percent + "%");

        PercentageBar.transform.localScale = new Vector3((100 - PercentageOfTrash()) / 100, PercentageBar.transform.localScale.y, PercentageBar.transform.localScale.z);

        int redNumber = percent * 2;
        int GreenNumber = 100 - (percent);
        byte red = (byte)redNumber;
        byte Green = (byte)GreenNumber;
        LoadingBar.color = new Color32(red, Green, 0, 255);
    }

    float PercentageOfTrash()
    {
        return ((100 / StartTrash) * currentSmallTrashAmount);
    }
}
