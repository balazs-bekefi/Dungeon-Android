using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTab : MonoBehaviour
{
    public List<InputField> fields;
    public Button startGame;
    int fieldIndexer=-1;

    private void Start()
    {
        fieldIndexer++;
        fields[fieldIndexer].Select();
    }

    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                fieldIndexer++;
                fields[fieldIndexer].Select();
            }
        }
        catch(ArgumentOutOfRangeException)
        {
            fieldIndexer = 0;
            fields[fieldIndexer].Select();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startGame.onClick.Invoke();
        }
    }
}
