using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Google.XR.ARCoreExtensions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordARSession : MonoBehaviour
{
    private bool isRecording = false;
    public GameObject StartRecordIcon;
    public GameObject StopRecordIcon;
    public GameObject PlaybackPanel;
    public GameObject RecordPanel;
    private ARCoreRecordingConfig _recordingConfig;
    public ARRecordingManager ArRecordingManager;
    
    public GameObject ToastObject;
    public Text toastText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _recordingConfig = ScriptableObject.CreateInstance<ARCoreRecordingConfig>();
        StartRecordIcon.SetActive(true);
        StopRecordIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int count = 0;

    public void RecordAndStopButton()
    {



        count = PlayerPrefs.GetInt("count", 0);
        
        string ha = RandomNumberGenerator.Create().ToString();
        string filepath = $"{Application.persistentDataPath}/record{count}{Time.time}.mp4";
        _recordingConfig.AutoStopOnPause = true;
        
        if (!isRecording)
        {

            _recordingConfig.Mp4DatasetFilepath = filepath;
            ArRecordingManager.StartRecording(_recordingConfig);

            PlayerPrefs.SetString(""+count,filepath);
            StopRecordIcon.SetActive(true);
            StartRecordIcon.SetActive(false);
            isRecording = true;

            count++;
            
            PlayerPrefs.SetInt("count",count);


        }
        else
        {
            ArRecordingManager.StopRecording();
            StartRecordIcon.SetActive(true);
            StopRecordIcon.SetActive(false);
            isRecording = false;
        }


    }

    public void BackButton()
    {

        SceneManager.LoadScene(ConstantList.SCENE_INTROSCENE);
        
        /*PlaybackPanel.SetActive(true);
        RecordPanel.SetActive(false);*/
    }
    
    public void ShowMessage(string messgae)
    {
        toastText.text = messgae;
        ToastObject.SetActive(true);

        StartCoroutine(ShowToastObject());
    }

    IEnumerator ShowToastObject()
    {
        yield return new WaitForSeconds(3f);
        ToastObject.SetActive(false);
    }
}
