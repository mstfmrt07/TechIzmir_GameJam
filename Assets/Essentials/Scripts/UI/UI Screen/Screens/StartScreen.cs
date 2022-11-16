using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : UIScreen
{
    public Text levelText;
        
    public override void Load()
    {
        base.Load();
        levelText.text = "BÖLÜM " + (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Close()
    {
        base.Close();
    }
}
