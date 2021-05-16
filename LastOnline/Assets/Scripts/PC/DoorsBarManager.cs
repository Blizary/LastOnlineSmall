using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsBarManager : MonoBehaviour
{
    [SerializeField] private GameObject gameView;
    [SerializeField] private GameObject windowView;
    [SerializeField] private GameObject messengerView;
    [SerializeField] private GameObject musicFolderView;
    [SerializeField] private GameObject photoFolderView;
    [SerializeField] private GameObject photoShow;


    public AudioSource tavernMusic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameBarButton()
    {
        if(gameView.activeInHierarchy)
        {
            windowView.SetActive(true);
            gameView.SetActive(false);
            tavernMusic.Pause();
        }
        else
        {
            windowView.SetActive(false);
            gameView.SetActive(true);
            tavernMusic.UnPause();
        }
        
    }

    public void DesktopButton()
    {
        windowView.SetActive(true);
        gameView.SetActive(false);
    }

    public void MusicFolderBarButton()
    {
        windowView.SetActive(true);
        gameView.SetActive(false);

        if (musicFolderView.activeInHierarchy)
        {
            musicFolderView.SetActive(false);
        }
        else
        {
            musicFolderView.SetActive(true);
        }
    }

    public void PhotoFolderBarButton()
    {
        windowView.SetActive(true);
        gameView.SetActive(false);

        if (photoFolderView.activeInHierarchy)
        {
            photoFolderView.SetActive(false);
        }
        else
        {
            photoFolderView.SetActive(true);
        }
    }

    public void ChatBarButton()
    {
        if (windowView.activeInHierarchy)
        {
            if (messengerView.activeInHierarchy)
            {
                messengerView.SetActive(false);
            }
            else
            {
                messengerView.SetActive(true);
            }
        }

        windowView.SetActive(true);
        gameView.SetActive(false);

       
       

    }

    public void CloseChat()
    {
        messengerView.SetActive(false);
    }

    public void CloseMusicFolder()
    {
        musicFolderView.SetActive(false);
    }

    public void ClosePhotoFolder()
    {
        photoFolderView.SetActive(false);
        ClosePhotoShow();
    }

    public void OpenPhotoShow()
    {
        photoShow.SetActive(true);
    }

    public void ClosePhotoShow()
    {
        photoShow.SetActive(false);
    }
}
