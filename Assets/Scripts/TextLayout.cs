using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TextLayout
{

    public Dictionary<int, string> DicItem { get; private set; }

    private static TextLayout instance;
    public static TextLayout Instance
    {
        get
        {
            if (instance == null)
                instance = new TextLayout();
            return instance;
        }
    }

    /// <summary>
    /// file reader for molecule files
    /// </summary>
    public TextLayout()
    {
        FileStream fs = new FileStream(Application.dataPath + "/" + GlobalCtrl.Instance.dataFolder + "/TextLayout.txt", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        DicItem = new Dictionary<int, string>();
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            DicItem.Add(int.Parse(s.Split(':')[0]), s.Split(':')[1]);
        }
        sr.Dispose();
        fs.Dispose();
    }
}
