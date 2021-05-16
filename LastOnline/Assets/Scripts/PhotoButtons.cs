using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoButtons : MonoBehaviour
{
    public Sprite img;
    public PhotoFolderController photoController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OpenPic()
    {
        photoController.ShowPhoto(img);
    }
}
