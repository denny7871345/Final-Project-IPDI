using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSet : MonoBehaviour
{

    public int nowLevel = 0;

    public Button plus;

    public TextMeshProUGUI text;

    void Start()
    {
        plus.onClick.AddListener(AddLevel);
        plus.enabled = true;
        text.text = nowLevel.ToString();
    }

    private void Update()
    {
        text.text = "+" + nowLevel.ToString();
        plus.enabled = true;
    }

    private void AddLevel()
    {
        nowLevel++;
    }

}
