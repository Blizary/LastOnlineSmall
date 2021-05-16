using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RPMove()
    {
        transform.GetChild(0).GetComponent<NPCController>().RPMove();

    }

    public void RosieCome()
    {
        transform.GetChild(0).GetComponent<NPCController>().RosieEnter();

    }

    public void LogOff()
    {
        StartCoroutine(WaitLogOff());
    }
    
    IEnumerator WaitLogOff()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

}
