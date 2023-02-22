using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsCanvas : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textInstruction;
    private string originalText;
    // Start is called before the first frame update
    void Start()
    {
        originalText = textInstruction.text;
        SetUp();   
    }

    private string ReplaceIn(string toModify, string nameInMap)
    {
        if (toModify.Contains(nameInMap))
        {
            string newStr=SettingsMenu.instance.GetCustomInput().GetNameOf(nameInMap);
            toModify = toModify.Replace(nameInMap, newStr);
        }
        return toModify;
    }

    public void SetUp()
    {
        string str = originalText;
        str = ReplaceIn(str, "upKey");
        str = ReplaceIn(str, "rightKey");
        str = ReplaceIn(str, "leftKey");

        str = ReplaceIn(str, "interactKey");
        str = ReplaceIn(str, "switchKey");

        str = ReplaceIn(str, "upSoul");
        str = ReplaceIn(str, "downSoul");
        str = ReplaceIn(str, "rightSoul");
        str = ReplaceIn(str, "leftSoul");
        textInstruction.text = str;
    }
}
