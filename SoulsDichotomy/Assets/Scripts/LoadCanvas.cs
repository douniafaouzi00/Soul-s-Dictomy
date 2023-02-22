using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCanvas : MonoBehaviour
{

    public GameObject panel;
    private bool amIOpen;
    private int amoutOpen;

    [SerializeField] private float secondsMinOpen;
    [SerializeField] private Slider slider;
    private bool canClose;
    private int numberOfIterationNeeded;
    private int number;
    private bool shoulICount;
    public static LoadCanvas instance;

    private float currValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        amIOpen = false;
        amoutOpen = 0;
        canClose = false;
        shoulICount = false;
        numberOfIterationNeeded = (int)Mathf.Ceil(secondsMinOpen * (1 / Time.fixedDeltaTime));
        slider.value = 0;
        panel.SetActive(false);
    }

    //50 call per secondo
    public void FixedUpdate()
    {
        if (shoulICount)
        {
            number++;
            currValue = Mathf.MoveTowards(currValue, 1f, 1f / (numberOfIterationNeeded - 3));
            slider.value = currValue;
                

            if (number == numberOfIterationNeeded)
            {
                shoulICount = false;
                SetCanClose();
            }
        }
    }


    private void SetCanClose()
    {
        canClose = true;
        CloseAll();
    }

    internal void Open()
    {
        // da riattivare
        ++amoutOpen;
        if (!amIOpen)
        {
            currValue = 0;
            slider.value = 0;
            shoulICount = true;
            number = 0;

            //Time.timeScale = 0;
            amIOpen = true;
            panel.SetActive(true);
        }
        
    }

    internal void Close()
    {
        // da riattivare
        --amoutOpen;
        CloseAll();
        
    }

    private void CloseAll()
    {
        if (amoutOpen == 0 && canClose)
        {
            amIOpen = false;
            MenuManager.instance.CloseAllMenus();
            //Time.timeScale = 1;
            panel.SetActive(false);

            canClose = false;
        }
    }

}
