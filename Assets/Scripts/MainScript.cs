using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{

    public GameObject videoList;
    public GameObject ScrollCOntent;
    public GameObject ToastObject;
    public Text toastText;
    public static string path;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RecordSession()
    {
        SceneManager.LoadScene(ConstantList.SCENE_ANCHORAR);
    }

    public void PopulateList()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.*");

        if (info.Length <= 0)
        {
            ShowMessage("There is no recording yet. Click the record button\nto record a, AR Session");
        }
        else
        {
            
            foreach (FileInfo f in info)
            {

                GameObject listGameObject = Instantiate(videoList);

                VideoListScript videoListScript = listGameObject.GetComponent<VideoListScript>();
            
                videoListScript.Setup(f.Name,f.ToString(),this);
            
                listGameObject.transform.SetParent(ScrollCOntent.transform);
            
            }
            
        }


        
    }

    public void HandleCLick(string pathvid)
    {
        path = pathvid;
        
        SceneManager.LoadScene(ConstantList.SCENE_PLAYBACKCLIP);
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
