using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private const string configurationPath = @"Configuration.json";
    private JsonConfiguration configuration;

    public TextMeshProUGUI title;
    public TextMeshProUGUI bottomHeader;
    public PanelController[] panels;

    // Start is called before the first frame update
    void Start()
    {
        WriteExampleConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        ReadJson();
        UpdateConfiguration();
    }

    public void UpdateConfiguration()
    {
        title.text = configuration.Title;
        bottomHeader.text = configuration.BottomHeader;
        UpdatePanels();
    }

    public void UpdatePanels()
    {
        var validPanels = configuration.InformationPanels
            .Where(x => x.StartTime > DateTime.Now)
            .OrderBy(x => x.StartTime)
            .ToList();

        for (int i = 0; i < panels.Length; i++)
        {
            if (i >= validPanels.Count)
            {
                panels[i].gameObject.SetActive(false);
                continue;
            }

            panels[i].UpdatePanel(validPanels[i]);
            panels[i].gameObject.SetActive(true);
        }
    }

    public void ReadJson()
    {
        try
        {
            string text = File.ReadAllText(configurationPath);
            var config = JsonConvert.DeserializeObject<JsonConfiguration>(text);
            configuration = config;
        }
        catch
        {
            Debug.LogError("Error Reading Json");
        }
    }

    public void WriteExampleConfiguration()
    {
        PanelInfo[] panelInfoExample = new PanelInfo[]
        {
            new()
            {
                Header = "ExampleHeader",
                StartTime = DateTime.Now,
                Information = "ExampleInfo"
            },
            new()
            {
                Header = "ExampleHeader2",
                StartTime = DateTime.Now + TimeSpan.FromHours(1),
                Information = "ExampleInfo2"
            }
        };
        var text = JsonConvert.SerializeObject(new JsonConfiguration 
        {
            Title = "ExampleTitle", BottomHeader = "ExampleBottomHeader", InformationPanels = panelInfoExample 
        });
        File.WriteAllText("ExampleConfiguration.json", text);
    }
}

[Serializable]
public class JsonConfiguration
{
    public string Title;

    public string BottomHeader;

    public PanelInfo[] InformationPanels;
}
