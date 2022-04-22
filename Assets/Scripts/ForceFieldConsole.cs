using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class for a status console; analogous to Debug console but not via Debug.log
public class ForceFieldConsole : MonoBehaviour
{

    Dictionary<string, string> statusLogs = new Dictionary<string, string>();

    public Text statusDisplay;

    private static ForceFieldConsole instance;
    public static ForceFieldConsole Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ForceFieldConsole>();
            }
            return instance;
        }
    }

    void Start()
    {
        statusOut("Force-field info");
    }

    void OnEnable()
    {
        //Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        //Application.logMessageReceived -= HandleLog;
    }

    public void statusOut(string message)
    // message should have syntax:  key :  message
    {
        string[] splitString = message.Split(char.Parse(":"));
        string msgKey = splitString[0];
        string msgValue = splitString.Length > 1 ? splitString[1] : "";

        if (statusLogs.ContainsKey(msgKey))
            statusLogs[msgKey] = msgValue;
        else
            statusLogs.Add(msgKey, msgValue);

        string displayText = "";
        foreach (KeyValuePair<string, string> log in statusLogs)
        {
            if (log.Value == "")
                displayText += log.Key + "\n";
            else
                displayText += log.Key + ": " + log.Value + "\n";
        }
        statusDisplay.text = displayText;
    }

}
