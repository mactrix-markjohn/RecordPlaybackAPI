using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoListScript : MonoBehaviour
{

    public Text text;
    private string name;
    private MainScript mainScriptt;
    public string path;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string videoname, string videodir, MainScript script)
    {
        path = videodir;
        text.text = videoname;
        mainScriptt = script;
        //videoname.Substring(videoname.LastIndexOf("/"),videoname.Length-1);
    }

    public void OnHandleClick()
    {
        mainScriptt.HandleCLick(path);
    }
}
