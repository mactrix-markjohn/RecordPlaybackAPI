using System.Collections;
using System.Collections.Generic;
using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaybackARSession : MonoBehaviour
{

    public ARSession session;
    private bool setPlaybackDataset;
    private float timeout;
    public ARPlaybackManager ARPlaybackManager;
    
    public GameObject ToastObject;
    public Text toastText;
    

    public GameObject Instruction;
    public GameObject PlayImage;
    public GameObject PauseImage;
    public GameObject PlayButton;

    public GameObject PlaceObject;
    GameObject SpawnedObject;
    public ARRaycastManager RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private bool restartCheck = false;

    private string path;
    

    // Start is called before the first frame update
    void Start()
    {
        PlaybackDataset();
       
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            
            

            if (SpawnedObject == null)
            {
                SpawnedObject = Instantiate(PlaceObject, hitPose.position, hitPose.rotation);
            }
            else
            {
                SpawnedObject.transform.position = hitPose.position;
            }
        }


        /*if (restartCheck)
        {
            if (ARPlaybackManager.PlaybackStatus == PlaybackStatus.FinishedSuccess)
            {
                // Disable the ARSession to pause the current AR session.
                session.enabled = false;

                // In the next frame, specify the same dataset file path.
                ARPlaybackManager.SetPlaybackDataset(path); // Same path that was previously set.

                // In the frame after that, re-enable the ARSession to resume the session from
                // the beginning of the dataset.
                session.enabled = true;
            }
        }*/

        


        /*if (setPlaybackDataset)
        {
          // StartPlaybackDataset();
        }
        else
        {
           // StopPlaybackDataset();
        }*/

    }
    
    void PlaybackDataset()
    {
       // setPlaybackDataset = true;

        // Pause the current AR session.
        session.enabled = false;
        
        path = MainScript.path;

        ShowMessage(path);

        setPlaybackDataset = false;

        // Set a timeout for retrying playback retrieval.
        timeout = 10f;
    }

    public void PlayBack()
    {
        if (!setPlaybackDataset)
        {
            timeout = 10f;
            Instruction.SetActive(false);
            PlayImage.SetActive(false);
            PauseImage.SetActive(true);
            setPlaybackDataset = true;
            StartPlaybackDataset();

            restartCheck = true;
            
            //PlayButton.SetActive(false);
            
            



        }
        else
        {
            timeout = 10f;
            PlayImage.SetActive(true);
            PauseImage.SetActive(false);
            setPlaybackDataset = false;
            RestartPlaybackDataset();
        }
    }

    void StartPlaybackDataset()
    {
        session.enabled = false;
        
        PlaybackResult result = ARPlaybackManager.SetPlaybackDataset(path);
        

        session.enabled = true;

        /*if (result == PlaybackResult.ErrorPlaybackFailed || result == PlaybackResult.SessionNotReady)
        {
            // Try to set the dataset again in the next frame.
            timeout -= Time.deltaTime;
        }
        else
        {
            // Do not set the timeout if the result is something other than ErrorPlaybackFailed.
            timeout = -1f;
        }

        if (timeout < 0.0f)
        {
            setPlaybackDataset = false;

            session.enabled = true;
                
                
            // If playback is successful, proceed as usual.
            // If playback is not successful, handle the error appropriately.
        }*/
    }

    void RestartPlaybackDataset()
    {
        
        
        
        if (ARPlaybackManager.PlaybackStatus == PlaybackStatus.FinishedSuccess)
        {
            // Disable the ARSession to pause the current AR session.
            session.enabled = false;

            // In the next frame, specify the same dataset file path.
            ARPlaybackManager.SetPlaybackDataset(path); // Same path that was previously set.

            // In the frame after that, re-enable the ARSession to resume the session from
            // the beginning of the dataset.
            session.enabled = true;
        }
        
        
        
        /*session.enabled = false;
        
        
        PlaybackResult result = ARPlaybackManager.SetPlaybackDataset(path);

        session.enabled = true;*/

        /*
        if (result == PlaybackResult.ErrorPlaybackFailed || result == PlaybackResult.SessionNotReady)
        {
            // Try to set the dataset again in the next frame.
            timeout -= Time.deltaTime;
        }
        else
        {
            // Do not set the timeout if the result is something other than ErrorPlaybackFailed.
            timeout = -1f;
        }

        if (timeout < 0.0f)
        {
            setPlaybackDataset = false;

            session.enabled = true;
                
                
            // If playback is successful, proceed as usual.
            // If playback is not successful, handle the error appropriately.
        }
        */


    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
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

    public void Back()
    {
        SceneManager.LoadScene(ConstantList.SCENE_INTROSCENE);
    }


    void OnDestroy()
    {
        DestroyImmediate(session);
    }



    #region PlaceOnPlane

    
    /*[SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]*/
    //GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    /*public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }*/

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
   // public GameObject spawnedObject { get; private set; }

   
    
    



    // ARRaycastManager m_RaycastManager;

    #endregion

}
