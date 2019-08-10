using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    [SerializeField]
    string[] list = { "1", "2","3", "4", "5"};
    [SerializeField]
    string[] newArray = { "", "", "", "", "", ""};

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < list.Length; i++)
            newArray[i] = list[i];

        ReverseList();
    }

    // Update is called once per frame
    private void ReverseList()
    {
        int j = list.Length - 1;
        string middleValue = "";
        if ((list.Length - 1) % 2 == 0)
        {
            middleValue = list[(list.Length / 2)];
        }
        else if ((list.Length -1) % 2 == .5)
        {
            middleValue = list[(int)((list.Length / 2) + .5)];
            Debug.Log(middleValue);

        }

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == "" || list[j] == "")
            {
                j--;
                continue;
            }
            if (middleValue == list[i])
            {
                break;
            }
            newArray[i] = list[j];
            newArray[j] = list[i];
            j--;
        }
    }
}
