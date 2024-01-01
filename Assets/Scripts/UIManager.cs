using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;


    private void Update()
    {
        coinText.text = "Coin \n" + GameManager.Instance.CoinInt.ToString();
    }


}
