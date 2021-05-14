using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingTextManager : MonoBehaviour
{
    public string nextScene;
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        Debug.Log("play yo");
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        
        fade.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }
}
