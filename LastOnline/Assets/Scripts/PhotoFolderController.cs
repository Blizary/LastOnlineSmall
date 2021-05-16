using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoFolderController : MonoBehaviour
{
    public GameObject currentPhoto;
    public List<Sprite> availablePhotos;
    public GameObject nextButton;
    public GameObject previousButton;

    private int currentPhotoInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhotoInt >= 0 && currentPhotoInt<availablePhotos.Count-1)
        {
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }

        if (currentPhotoInt > 1 && currentPhotoInt <= availablePhotos.Count)
        {
            previousButton.SetActive(true);
        }
        else
        {
            previousButton.SetActive(false);
        }
    }

    public void ShowPhoto(Sprite _newPhoto)
    {
        for(int i=0;i<availablePhotos.Count;i++)
        {
            if(_newPhoto == availablePhotos[i])
            {
                currentPhotoInt = i;
            }
        }
        currentPhoto.GetComponent<Image>().sprite = _newPhoto;
    }

    public void NextPhoto()
    {
        currentPhotoInt +=1;
        currentPhoto.GetComponent<Image>().sprite = availablePhotos[currentPhotoInt];
    }


    public void PreviousPhoto()
    {
        currentPhotoInt -= 1;
        currentPhoto.GetComponent<Image>().sprite = availablePhotos[currentPhotoInt];
    }    
}
