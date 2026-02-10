using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableUI : MonoBehaviour
{
    // reference to the collector that stores S/O data
    public CollectableCollector collector;

    [SerializeField]
    private Image image; // UI image for Item icon
    [SerializeField]
    private TextMeshProUGUI nameText; // UI text for Item name
    [SerializeField]
    private TextMeshProUGUI countText; // UI text for Item count

    private TextMeshProUGUI randomText;
    private Image randomImage;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        image.sprite = randomImage.sprite;
        nameText.text = randomText.text;
        countText.text = randomText.text;
    }
}
