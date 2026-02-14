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

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        if (collector == null)
        {
            Debug.LogWarning("This script will not function properly if you do not fill the variable collector");
            return;
        }

        if (image != null && collector.itemType.sprite != null)
        {
            image.sprite = collector.itemType.sprite;
        }
        if (nameText.text != null && collector.itemType.name != null)
        {
            nameText.text = collector.itemType.title;
        }
        if (countText.text != null)
        {
            countText.text = collector.collectedItems.ToString();
        }
    }
}
