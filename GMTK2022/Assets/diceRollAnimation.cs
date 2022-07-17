using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRollAnimation : MonoBehaviour
{
    public int result; //predetermined
    public float rollTime;
    public float stayTime;
    private float finishedRolling;
    public float waitTime;
    [SerializeField] Sprite[] sprites;
    private SpriteRenderer sr;
    private Animator anim;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine("Roll");
    }
    IEnumerator Roll()
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("Start", true);
        finishedRolling = Time.time + rollTime;
        while(Time.time < finishedRolling)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            sr.sprite = sprites[Random.Range(0, 6)];
        }
        sr.sprite = sprites[result - 1];
        yield return new WaitForSeconds(stayTime);
        anim.SetBool("Start", false);
    }
}
