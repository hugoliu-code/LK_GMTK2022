using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LivesIndicatorText : MonoBehaviour
{
    private TextMeshPro text;
    public bool isFirst;
    private Animator anim;
    private GameController gc;
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        text = GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();
        anim.SetBool("Start", true);
        if (isFirst)
        {
            text.text += gc.health.ToString();
        }
        Invoke("End", 4f);
    }

    // Update is called once per frame
    void End()
    {
        anim.SetBool("Start", false);
    }
}
