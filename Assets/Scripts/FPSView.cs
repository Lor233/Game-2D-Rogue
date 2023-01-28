using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSView : MonoBehaviour
{
    public bool on = false;
    public float updateInterval = 0.5F;
    private float accum = 0;
    private int frames = 0;
    private float timeleft;
    private string stringFps;

    void Start()
    {
        Application.targetFrameRate = 60; // Set FPS maximum to 60
        timeleft = updateInterval;
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            on = !on;
        }
        if (on) 
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;
            if(timeleft <= 0.0) 
            {
                float fps = accum / frames;
                string format = System.String.Format("{0:F2} FPS", fps);
                stringFps = format;
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
        }

    }

    void OnGUI()
    {
        if (on) 
        {
            GUIStyle guiStyle = GUIStyle.none;
            guiStyle.fontSize = 30;
            guiStyle.normal.textColor = Color.white;
            guiStyle.alignment = TextAnchor.UpperLeft;
            Rect rt = new Rect(50, 30, 100, 100);
            GUI.Label(rt, stringFps, guiStyle);
        }
    }
}