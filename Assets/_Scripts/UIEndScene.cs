using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScene : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text titleText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Your score: " + gameManager.GetScore().ToString();

        if (gameManager.isEscaped)
        {
            titleText.text = "You Escaped";
            titleText.color = Color.green;
        }
    }
}
