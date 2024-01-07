using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDie : MonoBehaviour
{
    SpriteRenderer sprite;
    public float fadespeed;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color desiredcolor = new Color(0, 0, 0, 0);
        sprite.color = Color.Lerp(sprite.color, desiredcolor, fadespeed * Time.deltaTime);

        if (sprite.color.a < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
