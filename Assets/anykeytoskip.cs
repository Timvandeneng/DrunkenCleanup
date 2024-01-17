using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anykeytoskip : MonoBehaviour
{
    Level_loader loader;
    [SerializeField] private Image fillimg;
    [SerializeField] private float fillspeed;
    private float filler;

    // Start is called before the first frame update
    void Start()
    {
        loader = GetComponent<Level_loader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            filler += Time.deltaTime * fillspeed;
        }
        else
        {
            filler = 0;
        }

        fillimg.fillAmount = filler;

        loader.Loadlevel = filler > 1;
        
        

        Debug.Log(filler > 1);
    }
}
