using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputDisplay : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Display;

    public Queue<string> LatestInput;

    private float _clearTimer;
    public float ClearTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        LatestInput = new Queue<string>();

        _clearTimer = ClearTime;
    }

    // Update is called once per frame
    void Update()
    {
        Display.text = string.Empty;

        ReadInput();

        UpdateDisplay();

        _clearTimer -= Time.deltaTime;

        if (_clearTimer <= 0 && LatestInput.Count > 0)
        {
            LatestInput.Dequeue();
            ResetTimer();
        }
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddInput("Left");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddInput("Forward");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddInput("Slow");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddInput("Right");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AddInput("Reset");
        }
    }

    private void AddInput(string text)
    {
        ResetTimer();

        if (LatestInput.Count >= 5)
        {
            LatestInput.Dequeue();
        }

        LatestInput.Enqueue(text);
    }

    private void UpdateDisplay()
    {
        var text = string.Empty;

        foreach (var input in LatestInput)
        {
            text += input + Environment.NewLine;
        }

        Display.text = text;
    }

    private void ResetTimer()
    {
        _clearTimer = ClearTime;
    }
}
