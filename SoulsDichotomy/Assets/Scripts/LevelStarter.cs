using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelStarter : MonoBehaviour
{

    [Header("ToTest")]
    public GameObject gameManager;
    public GameObject skillManager;
    public GameObject player;
    public GameObject soul;
    public GameObject cameraFollow;
    public GameObject eventSystem;

    [Header("Menu only available in game")]
    public GameObject skillMenu;
    public GameObject gameOverMenu;
    public GameObject UIPanel;
    public GameObject InGameMenu;
    public GameObject loadCanvas;

    [Header("Menu from mainMenu")]
    public GameObject settingMenu;
    private void Awake()
    {
        Transform enterLevel = GameObject.FindGameObjectWithTag("EnterLevel").transform;
        Instantiate(loadCanvas, Vector3.zero, Quaternion.identity);

        Instantiate(skillManager, Vector3.zero, Quaternion.identity);
        Instantiate(gameOverMenu, Vector3.zero, Quaternion.identity);
        Instantiate(UIPanel, Vector3.zero, Quaternion.identity);
        Instantiate(InGameMenu, Vector3.zero, Quaternion.identity);
        Instantiate(settingMenu, Vector3.zero, Quaternion.identity);
        if (GameManager.instance == null)
        {
            Instantiate(skillMenu, Vector3.zero, Quaternion.identity);
            Instantiate(player, enterLevel.position, Quaternion.identity);
            Instantiate(soul, enterLevel.position, Quaternion.identity);
            if (CinemachineCore.Instance.BrainCount == 0) 
            {
               // CinemachineVirtualCamera vc = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
                //if (vc == null)
                    DontDestroyOnLoad(Instantiate(cameraFollow, Vector3.zero, Quaternion.identity));
            }        
        }
        else
        {
            GameManager.instance.SetTransforOfCharacter(enterLevel.position);
        }
        Instantiate(eventSystem, Vector3.zero, Quaternion.identity);
        Instantiate(gameManager, Vector3.zero, Quaternion.identity);
        


    }

}
