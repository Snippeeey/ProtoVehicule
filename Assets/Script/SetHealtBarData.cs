using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealtBar : MonoBehaviour
{
    private RectTransform _canvasGameUI;
    private GameObject Cursor, _floatingText;
    void Start()
    {


        _canvasGameUI = GameObject.Find("Canvas_GameUI").GetComponent<RectTransform>();
        GenerateHealthBar();
    }

    private void GenerateHealthBar()
    {
        //Cursor = Instantiate(_healthBarPrefab) as GameObject;
        Cursor.transform.SetParent(_canvasGameUI, false);
    }
}   