﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceName : MonoBehaviour
{
    public bool needTexts;
    public GameObject text;
    public Text placeText;
    public string placeName;

    public virtual void OnEnable()
    {
        text = GameObject.FindGameObjectWithTag("Player UI").transform.Find("PlaceText").gameObject;
        placeText = text.GetComponent<Text>();
    }
    protected IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

    public void showText()
    {
        if (needTexts)
        {
            StartCoroutine(placeNameCo());
        }
    }
}