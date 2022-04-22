using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIMainMenu : MonoBehaviour
{

    public Button btn_save;


    /// <summary>
    /// initialises the main menu UI
    /// </summary>
    public void f_Init()
    {
        btn_save.GetComponentInChildren<Text>().text = TextLayout.Instance.DicItem[0001];
    }

    // Start is called before the first frame update
    void Start()
    {
        // Speichern

        // Laden

        // Saturate Taste, alle Dummys zu H Atomen machen

        // zuletzt verwendetes Atom Taste

        // Laserpointer auf Dummy, Handmenü für Optionen


        // um button durch touch zu aktivieren
        // position auf touchpad berechnen
        // button.onClick.Invoke();



        // Button material für bspw. Favoriten oder zuletzt genutzte
        // im UIMainMenu sprite von zuletzt besuchtem abspeichern
        // bei öffnen von Handmenü sprite auf Favoritenposition laden
    }

    // Update is called once per frame
    void Update()
    {

    }




}
