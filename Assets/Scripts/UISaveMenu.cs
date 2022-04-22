using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISaveMenu : MonoBehaviour
{
    public GameObject textfieldGO;
    public GameObject savemenu;

    public GameObject caps;

    public Material grey;

    public string inputfield;

    public bool upperCase = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void inputKey(string key)
    {
        if (upperCase)
            textfieldGO.GetComponent<TMP_InputField>().text += key.ToUpper();
        else
            textfieldGO.GetComponent<TMP_InputField>().text += key.ToLower();

        inputfield = textfieldGO.GetComponent<TMP_InputField>().text;
    }

    public void deleteKey()
    {
        if (textfieldGO.GetComponent<TMP_InputField>().text.Length > 0)
            textfieldGO.GetComponent<TMP_InputField>().text = textfieldGO.GetComponent<TMP_InputField>().text.Remove(textfieldGO.GetComponent<TMP_InputField>().text.Length - 1);

        inputfield = textfieldGO.GetComponent<TMP_InputField>().text;
    }


    public void clearInputField()
    {
        textfieldGO.GetComponent<TMP_InputField>().text = "";
        inputfield = textfieldGO.GetComponent<TMP_InputField>().text;
    }

    public void upperLowerCase()
    {
        Transform keyboard = savemenu.transform.GetChild(0);
        for (int i = 1; i <= 3; i++)
        {
            Transform line = keyboard.GetChild(i);
            foreach (Transform key in line)
            {
                if (!upperCase)
                    key.GetChild(0).GetComponent<Text>().text = key.GetChild(0).GetComponent<Text>().text.ToUpper();
                else
                    key.GetChild(0).GetComponent<Text>().text = key.GetChild(0).GetComponent<Text>().text.ToLower();
            }
        }

        if (!upperCase)
            caps.GetComponent<Image>().material = grey;
        else
            caps.GetComponent<Image>().material = null;

        upperCase = !upperCase;
    }

    /// <summary>
    /// this method controls the main UI
    /// UI can be toggled on and off
    /// </summary>
    public void activateKeyboard()
    {
        savemenu.SetActive(true);

        //Vector3 tempForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        //Ray ray = new Ray(Camera.main.transform.position, tempForward);
        //if (savemenu.gameObject.activeSelf)
        //{
        //    savemenu.transform.position = ray.GetPoint(2);
        //    savemenu.transform.LookAt(ray.GetPoint(7));
        //}
    }

    public void deactivateKeyboard()
    {
        savemenu.SetActive(false);
        //savemenu.transform.position = new Vector3(-100, -100, -100);
    }
}
