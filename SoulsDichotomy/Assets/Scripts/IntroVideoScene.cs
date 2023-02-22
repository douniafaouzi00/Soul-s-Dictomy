using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        AudioManager.instance.StopMusic();
        videoPlayer.loopPointReached += LoadScene;
    } 
    void LoadScene(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= LoadScene;
        LevelManager.instance.LoadLevel(0);
    }
    public void Skip()
    {
        videoPlayer.loopPointReached -= LoadScene;
        LevelManager.instance.LoadLevel(0);
    }
}
