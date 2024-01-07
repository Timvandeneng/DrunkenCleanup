using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Clothing_Script : MonoBehaviour
{
    public GameObject Sweater, tShirt, longPants, ShortPants;
    public GameObject PenisHat, Sunglasses, HiHat;
    public SkinnedMeshRenderer Skin;
    public Material Darkskin, latteSkin, LightSkin;

    // Start is called before the first frame update
    void Start()
    {
        int Combination = Random.Range(-1, 5);
        switch (Combination)
        {
            case 0:
                Sweater.SetActive(true);
                longPants.SetActive(true);
                break;
            case 1:
                tShirt.SetActive(true);
                longPants.SetActive(true);
                break;
            case 2:
                Sweater.SetActive(true);
                ShortPants.SetActive(true);
                break;
            case 3:
                tShirt.SetActive(true);
                ShortPants.SetActive(true);
                break;
            case 4:
                ShortPants.SetActive(true);
                break;
            default:
                longPants.SetActive(true);
                break;
        }

        int RandomSkinColor = Random.Range(-1, 3);
        switch (RandomSkinColor)
        {
            case 0:
                Skin.material = Darkskin;
                break;
            case 1:
                Skin.material = latteSkin;
                break;
            case 2:
                Skin.material = LightSkin;
                break;
            default:
                //
                break;
        }

        int RandomCosmeticItem = Random.Range(0, 3);
        switch(RandomCosmeticItem)
        {
            case 0:
                PenisHat.SetActive(true);
            break;
            case 1:
                Sunglasses.SetActive(true);
            break;
            case 2:
                HiHat.SetActive(true);
            break;
            default:
                //
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
