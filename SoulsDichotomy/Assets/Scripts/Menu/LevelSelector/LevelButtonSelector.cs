using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelButtonSelector : MonoBehaviour
{
    [Header("Level the Door will open")]
    [SerializeField] private int level;
    [Header("Sprites")]
    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite unlocked;
    [Header("UI components")]
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image imageUI;
    [SerializeField] private TextMeshProUGUI textScore;
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void OnEnable()
    {
        SetUp();
    }

    private void SetUp()
    {
        /*
            //TODO: 4 DEBUG remove
            imageUI.sprite = unlocked;
            button.interactable = true;
            textMeshPro.text = level.ToString();
            textScore.text = "-99999";
            return;
            //end todo remove
        */


        //Levelx has 1 if is already be completed 0 otherwise, so you can re-choose it
        //Level1 must have 1 from the beginning
        if (PlayerPrefs.HasKey("Level" + level))
        {
            if (PlayerPrefs.GetInt("Level" + level) == 1)
            {
                imageUI.sprite = unlocked;
                button.interactable = true;
            }
            else
            {
                imageUI.sprite = locked;
                button.interactable = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Level" + level, level==1?1:0);
            SetUp();
            return;
        }
        textMeshPro.text = level.ToString();
        if (PlayerPrefs.HasKey("ScoreLevel" + level))
        {
            textScore.text = PlayerPrefs.GetInt("ScoreLevel" + level) + " s";
        }
        else
        {
            textScore.text = "";
        }
    }
}
