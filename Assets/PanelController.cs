using System;
using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public TextMeshProUGUI panel;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI infoText;

    public PanelInfo panelInfo;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePanel(panelInfo);
    }

    public void UpdatePanel(PanelInfo panelInfo)
    {
        this.panelInfo = panelInfo;

        headerText.text = panelInfo.Header;
        timeText.text = panelInfo.StartTime.ToString("HH:mm");
        infoText.text = panelInfo.Information;

        //timeText.color = Color.white;
        //headerText.color = Color.white;
        //infoText.color = Color.white;

        //if (panelInfo.Time - DateTime.Now <= TimeSpan.FromMinutes(30))
        //{
        //    timeText.color = Color.red;
        //    headerText.color = Color.red;
        //    infoText.color = Color.red;
        //}

    }
}

[SerializeField]
public class PanelInfo
{
    public string Header { get; set; }

    public DateTime StartTime { get; set; }

    public string Information { get; set; }
}
