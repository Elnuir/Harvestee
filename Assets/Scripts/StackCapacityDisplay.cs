using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackCapacityDisplay : MonoBehaviour
{
    Stack stack;
    TextMeshProUGUI textMeshPro;
    
    void Start()
    {
        stack = FindObjectOfType<Stack>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = stack.currentCapacity.ToString() + "/" + stack.maxCapacity.ToString(); //SetText(stack.currentCapacity.ToString() + "/" + stack.maxCapacity.ToString());
    }
}
