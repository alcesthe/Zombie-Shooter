using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
    [SerializeField] Slider healthDisplay;
    [SerializeField] Text scoreDisplay;
    [SerializeField] Text timerDisplay;
    [SerializeField] Player player;
    [SerializeField] float minTimeToCallHelicopter = 180f;

    [Header("Message")]
    [SerializeField] GameObject messagePanel;
    [SerializeField] Text messageText;
    

    private int score = 0;
    private bool isHelicopterAvailable = false;
    private bool isDisplayingMessage = false;

    /*    private void Awake()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }*/

    // Update is called once per frame
    void Update()
    {
        float health = player.GetHealth();
        healthDisplay.value = health;
        scoreDisplay.text = "Score " + score;

        UpdateTimerToCallHelicopter();
    }

    private void UpdateTimerToCallHelicopter()
    {
        if (isHelicopterAvailable == false)
        {
            minTimeToCallHelicopter -= Time.deltaTime;
            timerDisplay.text = "Timer " + (int)minTimeToCallHelicopter;
            if (minTimeToCallHelicopter <= 0)
            {
                isHelicopterAvailable = true;
                timerDisplay.color = Color.red;
                timerDisplay.text = "Escape NOW";
            }
        }
    }

    public void AddPoint(int amount)
    {
        score += amount;
    }

    public bool GetIsHelicopterAvailable()
    {
        return isHelicopterAvailable;
    }

    public void SetMessage(string text)
    {
        messageText.text = text;
        if (!isDisplayingMessage)
        {
            isDisplayingMessage = true;
            StartCoroutine(ToggleMessagePanel());
        }
    }

    IEnumerator ToggleMessagePanel()
    {
        messagePanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        messagePanel.gameObject.SetActive(false);
        isDisplayingMessage = false;
    }
}
