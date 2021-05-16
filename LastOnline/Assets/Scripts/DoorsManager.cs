using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum IngameEvent
{
    RpEvent,
    RpInterract,
    FriendLogOff,
    RosieEnter,
    BlazeOff,
    RosieOff,
    Credits,
}
public class DoorsManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private bool holdToType;
    public ChatBoxManager chatManager;
    public MsnManager msnManager;
    public ChatConv rpConvo;
    public ChatConv friendMSNConvo;
    public ChatConv girlMSNConvo;

    public UnityEvent rpStart;
    public UnityEvent rpInterract;
    public UnityEvent rosieEnter;
    public UnityEvent blazeLogsOff;
    public UnityEvent rosieLogsOff;

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

    public void StartEvent(IngameEvent _event)
    {
        switch(_event)
        {
            case IngameEvent.RpEvent:
                Debug.Log("Rp event started");
                rpStart.Invoke();
                chatManager.AddChat(rpConvo);
                break;
            case IngameEvent.RpInterract:
                Debug.Log("Rp interact started");
                rpInterract.Invoke();
                break;
            case IngameEvent.RosieEnter:
                Debug.Log("Rosie enters the scene");
                rosieEnter.Invoke();
                break;
            case IngameEvent.BlazeOff:
                Debug.Log("Blaze logs off");
                StartCoroutine(WaitForLogOff(5, friendMSNConvo));
                blazeLogsOff.Invoke();
                break;
            case IngameEvent.RosieOff:
                Debug.Log("Rosie logs off");
                StartCoroutine(WaitForLogOff(5, girlMSNConvo));
                rosieLogsOff.Invoke();
                break;
            case IngameEvent.FriendLogOff:
                Debug.Log("Friend log off event started");
                break;
            case IngameEvent.Credits:
                Debug.Log("Game end");
                Credits();
                break;

        }
    }

    public bool HoldToType()
    {
        return holdToType;
    }


    IEnumerator WaitForLogOff(float _time,ChatConv _newConvo)
    {
        yield return new WaitForSeconds(_time);
        msnManager.AddChat(_newConvo);


    }


    public void Credits()
    {
        StartCoroutine(CreditsIE());
    }

    IEnumerator CreditsIE()
    {

        fade.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextScene);
    }

}
