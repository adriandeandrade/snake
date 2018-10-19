using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    [SerializeField] private GameObject tryAgainPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject startRoundPanel;

    private void Awake()
    {
        panels.Add("tryAgainPanel", tryAgainPanel);
        panels.Add("scorePanel", scorePanel);
        panels.Add("startRoundPanel", startRoundPanel);
    }

    public static void ActivatePanel(string panelName)
    {
        GameObject panel;
        if (panels.TryGetValue(panelName, out panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogError("Panel with name: " + panelName + " cannot be found.");
        }
    }

    public static void DeactivatePanel(string panelName)
    {
        GameObject panel;
        if (panels.TryGetValue(panelName, out panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel with name: " + panelName + " cannot be found.");
        }
    }
}
