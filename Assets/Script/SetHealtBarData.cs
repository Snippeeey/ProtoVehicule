using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    private RectTransform _canvasGameUI;
    private GameObject Cursor, _floatingText;
    private PlanePilot PP;
    void Start()
    {

        PP = FindObjectOfType<PlanePilot>();
        _canvasGameUI = GameObject.Find("Canvas_GameUI").GetComponent<RectTransform>();
        GenerateHealthBar();
    }

    private void GenerateHealthBar()
    {
        //Cursor = Instantiate(_healthBarPrefab) as GameObject;
        //Cursor.GetComponent<PlanePilot>().SetHealthBarData(this.transform, _canvasGameUI);
        //PP.SetHealthBarData()

        //Cursor.transform.SetParent(_canvasGameUI, false);
    }
}