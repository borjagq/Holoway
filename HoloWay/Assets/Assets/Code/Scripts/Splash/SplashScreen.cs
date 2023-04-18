using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private bool startFade = false; // Whether the animation has started.
    private float CurrentAlpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        // Set the new color in the Image.
        GameObject CoverImage = GameObject.Find("CoverImage");
        Image Cover = CoverImage.GetComponent<Image>();
        Color NewColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Cover.color = NewColor;

        // Get the VidePlayer component.
        GameObject GamePlayer = GameObject.Find("VideoPlayer");

        // Get the actual VideoPlayer instance.
        VideoPlayer VP = GamePlayer.GetComponent<VideoPlayer>();

        // Set the video to the first frame.
        VP.frame = 0;
        VP.Prepare();
        VP.frame = 0;
        
        // Invoke repeating of checkOver method
        InvokeRepeating("FadeTransition", 0.1f, 0.025f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Gets called to animate the color transition.
    void FadeTransition()
    {
        // Get the VidePlayer component.
        GameObject GamePlayer = GameObject.Find("VideoPlayer");

        // Get the actual VideoPlayer instance.
        VideoPlayer VP = GamePlayer.GetComponent<VideoPlayer>();

        // Play the video!
        //Player.Play();

        if (startFade)
        {
            // Get the object that is used to cover the other.
            GameObject CoverImage = GameObject.Find("CoverImage");
            Image Cover = CoverImage.GetComponent<Image>();

            // Operate with it.
            CurrentAlpha -= 0.05f;
            if (CurrentAlpha < 0)
            {
                // Alpha can't be under 0 so we will just use 0 instead.
                CurrentAlpha = 0.0f;

                // Remove this repeating.
                CancelInvoke("FadeTransition");

                // Add the repeating that will chack if the video has finished.
                InvokeRepeating("CheckIfVideoIsOver", 0.1f, 0.1f);

                // Play the video.
                VP.Play();
            }

            // Set the new color in the Image.
            Color NewColor = new Color(1.0f, 1.0f, 1.0f, CurrentAlpha);
            Cover.color = NewColor;
        }
        else
        {
            if (VP.isPrepared)
            {
                VP.Play();
                VP.Pause();
                startFade = true;
            }
        }
    }

    public void CheckIfVideoIsOver()
    {
        // Get the VidePlayer component.
        GameObject GamePlayer = GameObject.Find("VideoPlayer");

        // Get the actual VideoPlayer instance.
        VideoPlayer VP = GamePlayer.GetComponent<VideoPlayer>();

        // Video stats.
        long playerCurrentFrame = VP.frame;
        long playerFrameCount = Convert.ToInt64(VP.frameCount);
        
        if(playerCurrentFrame >= (playerFrameCount - 1))
        {
            SceneManager.LoadScene(1);
        }
    }
}
