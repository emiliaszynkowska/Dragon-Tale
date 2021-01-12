using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    public GameObject item1;
    public RawImage icon1;
    public TextMeshProUGUI text1;

    public GameObject item2;
    public RawImage icon2;
    public TextMeshProUGUI text2;

    public GameObject item3;
    public RawImage icon3;
    public TextMeshProUGUI text3;

    private bool item1set = false;
    private bool item2set = false;
    private bool item3set = false;

    public TextMeshProUGUI empty;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
        empty.gameObject.SetActive(true);

    }

    public void AddItem(Texture icon, string text)
    {
        if (!item1set)
        {
            empty.gameObject.SetActive(false);
            item1set = true;
            item1.SetActive(true);
            icon1.texture = icon;
            text1.text = text;
        } else if (!item2set)
        {
            item2.SetActive(true);
            item2set = true;
            icon2.texture = icon;
            text2.text = text;
        }
        else if (!item3set)
        {
            item3.SetActive(true);
            item3set = true;
            icon3.texture = icon;
            text3.text = text;
        }
    }
}
