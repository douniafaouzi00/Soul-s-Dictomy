using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CustomizeInput : MonoBehaviour
{
    public delegate void OnChangeInput(KeyCode up, /*KeyCode down,*/ KeyCode right, KeyCode left, KeyCode interact, KeyCode switchChar, KeyCode upSoul, KeyCode downSoul, KeyCode rightSoul, KeyCode leftSoul);

    public static OnChangeInput changeInput;

    //non credo serva ma per ora lo lascio qua che ci ho sudato il sangue
    public static OnChangeInput ChangeInput { 
        set { changeInput += value;
            changeInput(map["upKey"],/* map["downKey"],*/ map["rightKey"], map["leftKey"], map["interactKey"], map["switchKey"], map["upSoul"], map["downSoul"], map["rightSoul"], map["leftSoul"]);
        }
        get { return changeInput; }
    }

    public TextMeshProUGUI upText;
    public TextMeshProUGUI downText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    public TextMeshProUGUI interactText;
    public TextMeshProUGUI switchText;

    public TextMeshProUGUI upTextSoul;
    public TextMeshProUGUI downTextSoul;
    public TextMeshProUGUI leftTextSoul;
    public TextMeshProUGUI rightTextSoul;

    public GameObject pressAnyKey;
    private TextMeshProUGUI pressAnyKeyMesh;
    private string pressAnyKeyText;

    private KeyCode up;
    //private KeyCode down;
    private KeyCode right;
    private KeyCode left;
    private KeyCode interact;
    private KeyCode switchChar;
    private KeyCode leftSoul;
    private KeyCode rightSoul;
    private KeyCode downSoul;
    private KeyCode upSoul;

    private string selected;
    private bool editMode;

    public static Dictionary<String, KeyCode> map;
    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<string, KeyCode>();
        map.Add("upKey", up);
        //map.Add("downKey", down);
        map.Add("rightKey", right);
        map.Add("leftKey", left);
        map.Add("interactKey", interact);
        map.Add("switchKey", switchChar);
        map.Add("rightSoul", rightSoul);
        map.Add("leftSoul", leftSoul);
        map.Add("downSoul", downSoul);
        map.Add("upSoul", upSoul);
        SetDefault();
        editMode = false;
        FillKeyCode();
        pressAnyKeyMesh = pressAnyKey.GetComponent<TextMeshProUGUI>();
        pressAnyKeyText = pressAnyKeyMesh.text;
    }
    
    private void SetDefault()
    {
        map["upKey"] = KeyCode.UpArrow;
        //map["downKey"] = KeyCode.DownArrow;
        map["rightKey"] = KeyCode.RightArrow;
        map["leftKey"] = KeyCode.LeftArrow;
        
        map["interactKey"] = KeyCode.E;
        map["switchKey"] = KeyCode.Space;

        map["leftSoul"]= KeyCode.LeftArrow;
        map["upSoul"] = KeyCode.UpArrow;
        map["downSoul"] = KeyCode.DownArrow;
        map["rightSoul"] = KeyCode.RightArrow;
    }

    private void FillKeyCode()
    {
        IfExistFillOrInit(ref up, "upKey");
        //IfExistFillOrInit(ref down, "downKey");
        IfExistFillOrInit(ref right, "rightKey");
        IfExistFillOrInit(ref left, "leftKey");

        IfExistFillOrInit(ref interact, "interactKey");
        IfExistFillOrInit(ref switchChar, "switchKey");

        IfExistFillOrInit(ref upSoul, "upSoul");
        IfExistFillOrInit(ref downSoul, "downSoul");
        IfExistFillOrInit(ref rightSoul, "rightSoul");
        IfExistFillOrInit(ref leftSoul, "leftSoul");
    }

    public void SetText()
    {
        upText.text = map["upKey"].ToString();
        //downText.text = map["downKey"].ToString();
        rightText.text = map["rightKey"].ToString();
        leftText.text = map["leftKey"].ToString();

        interactText.text = map["interactKey"].ToString();
        switchText.text = map["switchKey"].ToString();

        upTextSoul.text = map["upSoul"].ToString();
        downTextSoul.text = map["downSoul"].ToString();
        rightTextSoul.text = map["rightSoul"].ToString();
        leftTextSoul.text = map["leftSoul"].ToString();
    }

    public string GetNameOf(string s)
    {
        if (map.ContainsKey(s))
            return map[s].ToString();
        else
            return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse0))
                    {
                        editMode = false;
                        ToggleTextPressAnyKey();
                        break;
                    }
                    if (Input.GetKey(keyCode) && keyCode!=KeyCode.Escape && keyCode!=KeyCode.Mouse0)
                    {
                        if(ChangeKey(keyCode, selected))
                        {
                            editMode = false;
                            break;
                        }
                        
                    }
                }
            }
        }



    }

    public void SwitchEditMod(string name)
    {
        ToggleTextPressAnyKey();
        editMode = true;
        switch (name)
        {
            case "up":
                selected = "upKey";
                break;
            //case "down":
            //    selected = "downKey";
            //    break;
            case "left":
                selected = "leftKey";
                break;
            case "right":
                selected = "rightKey";
                break;
            case "interact":
                selected = "interactKey";
                break;
            case "switch":
                selected = "switchKey";
                break;
            case "upSoul":
                selected = "upSoul";
                break;
            case "downSoul":
                selected = "downSoul";
                break;
            case "leftSoul":
                selected = "leftSoul";
                break;
            case "rightSoul":
                selected = "rightSoul";
                break;
        }
    }

    private void IfExistFillOrInit(ref KeyCode key, string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if((KeyCode)PlayerPrefs.GetInt(name) == KeyCode.None)
            {
                PlayerPrefs.SetInt(name, (int)map[name]);
            }
            else
            {
                map[name] = (KeyCode)PlayerPrefs.GetInt(name);
            }
        }
        else
        {
            PlayerPrefs.SetInt(name, (int)map[name]);
        }
    }

    private bool IsValid(KeyCode k)
    {
        if (map.ContainsValue(k))
        {
            if (selected == "upSoul" && map["upKey"]==k)
            {
                return true;
            }else if (selected == "upKey" && map["upSoul"] == k)
            {
                return true;
            }/*else if (selected == "downKey" && map["downSoul"] == k)
            {
                return true;
            }else if (selected == "downSoul" && map["downKey"] == k)
            {
                return true;
            }*/else if (selected == "leftKey" && map["leftSoul"] == k)
            {
                return true;
            }else if (selected == "leftSoul" && map["leftKey"] == k)
            {
                return true;
            }else if (selected == "rightKey" && map["rightSoul"] == k)
            {
                return true;
            }else if (selected == "rightSoul" && map["rightKey"] == k)
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }  
    }

    private bool ChangeKey(KeyCode newKey, string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if (!IsValid(newKey))
            {
                StartCoroutine("ShowError");
                return false;
            }
            PlayerPrefs.SetInt(name, (int)newKey);
            map[name] = newKey;
            SetText();
            ToggleTextPressAnyKey();
            if (changeInput != null)
            {
                changeInput(map["upKey"],/* map["downKey"],*/ map["rightKey"], map["leftKey"], map["interactKey"], map["switchKey"], map["upSoul"], map["downSoul"], map["rightSoul"], map["leftSoul"]);
            }

            //questo non va, da capire perche
            InstructionsCanvas[] myinstructions = FindObjectsOfType<InstructionsCanvas>();
            foreach(InstructionsCanvas ic in myinstructions)
            {
                ic.SetUp();
            }
                
            return true;
        }
        return false;
    }

    private void ToggleTextPressAnyKey()
    {
        pressAnyKey.SetActive(!pressAnyKey.activeInHierarchy);
    }
    private IEnumerator ShowError()
    {
        pressAnyKeyMesh.text = "Key already in use, please select a valid one!";
        yield return new WaitForSeconds(2);
        pressAnyKeyMesh.text = pressAnyKeyText;
    }
}
