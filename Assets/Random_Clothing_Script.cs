using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Clothing_Script : MonoBehaviour
{
    public GameObject Sweater, tShirt, longPants, ShortPants;
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
                //
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
