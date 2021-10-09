using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text pots;

    // Update is called once per frame
    void Update()
    {
        pots.text = transform.GetComponent<GameInfo>().potions.ToString();
    }
}
