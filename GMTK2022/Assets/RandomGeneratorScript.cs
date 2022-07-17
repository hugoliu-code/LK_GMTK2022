using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneratorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] levelone = new int[] {1,2,3,4,5,6};
    private int[] levelTwo = new int[] { 1, 1, 2, 3 };
    private int generateNum;
    private GameController gc;
    private GunManager gm;
    [SerializeField] GameObject dice;
    [SerializeField] GameObject card;
    [SerializeField] float diceOffset;
    [SerializeField] float cardOffset;
    [SerializeField] float waitForDiceGeneration;
    [SerializeField] float waitForCardGeneration;
    private int[] rollsGenerated;
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gm = FindObjectOfType<GunManager>();
        generateNum = gc.health;
        Invoke("GenerateDice", waitForDiceGeneration);
    }
    void GenerateDice()
    {
        rollsGenerated = new int[gc.health];
        for(int a = 0; a < gc.health; a++)
        {
            rollsGenerated[a] = levelone[Random.Range(0, levelone.Length)];
            GameObject current = Instantiate(dice);
            current.GetComponentInChildren<diceRollAnimation>().result = rollsGenerated[a];
            float total = (diceOffset * (gc.health - 1));
            current.transform.position = new Vector2((diceOffset * a)-total/2, 0f);
        }
        Invoke("GenerateCards", waitForCardGeneration);
    }
    void GenerateCards()
    {
        for (int a = 0; a < gc.health; a++)
        {
            GameObject current = Instantiate(card);
            current.GetComponentInChildren<CardController>().roll = rollsGenerated[a];
            float total = (cardOffset * (gc.health - 1));
            current.transform.position = new Vector2((cardOffset * a) - total / 2, 0f);
        }
    }

}
