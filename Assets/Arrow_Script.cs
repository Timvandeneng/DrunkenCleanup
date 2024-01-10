using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Arrow_Script : MonoBehaviour
{
    Transform Target;
    public Transform Model;
    public float FollowSpeed;
    public int WhichTarget; //transforms under here from top to down decide the int

    public List<Transform> Bins;
    public Transform SelectedBin;
    public List<Transform> Toilets;
    public Transform SelectedToilet;
    public Transform Shack;

    TextMeshPro Loctext;

    //all for the glow effect
    trashBinOpener trashcanscrpt;
    Human_Store_place shackScrpt;

    // Start is called before the first frame update
    void Start()
    {
        SelectedBin = Bins[0];
        SelectedToilet = Toilets[0];
        Target = SelectedBin;
        trashcanscrpt = Bins[0].GetComponent<trashBinOpener>();
        Loctext = Model.GetComponentInChildren<TextMeshPro>();
        shackScrpt = FindFirstObjectByType<Human_Store_place>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredLookpos = new Vector3(Target.position.x, Target.position.y, Target.position.z);
        transform.LookAt(desiredLookpos);

        Model.position = Vector3.Lerp(Model.position, transform.position, FollowSpeed * Time.deltaTime);
        Model.rotation = transform.rotation;
        Model.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
       

        switch (WhichTarget)
        {
            case 0:
                transform.localScale = Vector3.zero;
                trashcanscrpt.selected = false;
                shackScrpt.Selected = false;
                Loctext.text = "";
                break;

            case 1:
                transform.localScale = new Vector3(1, 1, 1);
                Target = SelectedBin;
                trashcanscrpt.selected = true;
                Loctext.text = "Bring To: Trash-Can";
                break;

            case 2:
                transform.localScale = new Vector3(1, 1, 1);
                Loctext.text = "BARF IN TOILET!!!";
                Target = SelectedToilet;
                break;

            case 3:
                transform.localScale = new Vector3(1, 1, 1);
                Target = Shack;
                shackScrpt.Selected = true;
                Loctext.text = "Bring To: Shack";
                break;
        }



        for (int i = 0; i < Bins.Count; i++)
        {
            float dist = Vector3.Distance(Bins[i].position, transform.position);
            float CurDist = Vector3.Distance(SelectedBin.position, transform.position);

            if (dist < CurDist && Bins[i].gameObject.activeInHierarchy)
            {
                SelectedBin = Bins[i];
                trashcanscrpt = Bins[i].GetComponent<trashBinOpener>();
            }

            if (i == Bins.Count)
                i = 0;

        }

        for (int i = 0; i < Toilets.Count; i++)
        {
            float dist = Vector3.Distance(Toilets[i].position, transform.position);
            float CurDist = Vector3.Distance(SelectedToilet.position, transform.position);

            if (dist < CurDist && Toilets[i].gameObject.activeInHierarchy)
            {
                SelectedToilet = Toilets[i];
            }

            if (i == Toilets.Count)
                i = 0;

        }
    }
}
