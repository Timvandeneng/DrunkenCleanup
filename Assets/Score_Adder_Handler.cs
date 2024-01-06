using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Adder_Handler : MonoBehaviour
{
    public int Ammount;
    public TextMeshProUGUI ScoreText;
    public float maxrotation;
    public float Speed;
    public float ShrinkSpeed;

    public AudioSource audioM;

    // Start is called before the first frame update
    void Start()
    {
        float randomRot = Random.Range(-maxrotation, maxrotation);
        ScoreText.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, randomRot));
        audioM.pitch = Ammount > 0 ? Random.Range(0.2f, 1f) : 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScoreText.text = Ammount > 0 ? "+" + Ammount : "" + Ammount;
        ScoreText.rectTransform.position += new Vector3(0, Speed * Time.deltaTime, 0);
        ScoreText.rectTransform.localScale -= new Vector3(ShrinkSpeed * Time.deltaTime, ShrinkSpeed * Time.deltaTime, 0);

        if(ScoreText.rectTransform.localScale.x < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
