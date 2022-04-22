using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFileListener : MonoBehaviour
{
    public void giveName(GameObject obj)
    {
        string name = obj.GetComponentInChildren<Text>().text;

        GlobalCtrl.Instance.LoadMolecule(name, 0);
    }

    public void ignoreName()
    {
        foreach (Atom a in GlobalCtrl.Instance.List_curAtoms)
        {
            a.isMarked = true;
        }
        GlobalCtrl.Instance.markToDelete();


        GlobalCtrl.Instance.LoadMolecule("", 1);
    }
}
