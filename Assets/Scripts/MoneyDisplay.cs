using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    MoneyDisplay score;
    TextMeshProUGUI textMeshPro;
    int money;
    Animator animator;
    string VIBRATE = "Vibrate";
    public int Money
    {
        get => money;
        set
        {
            money = value;
            animator.SetTrigger(VIBRATE);;
            textMeshPro.text = score.Money.ToString();
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        score = FindObjectOfType<MoneyDisplay>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

}
