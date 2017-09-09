using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    public void Awake()
    {
        Invoke("OnTimer", 1.0F);  //2秒后，没0.3f调用一次
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTimer()
    {
        Debug.Log("OnTimer");

        GameObject UIRoot = GameObject.Find("Canvas");
        Object dialogPrefab = Resources.Load("EasyMenu/_Prefabs/Dialog") as Object;
        GameObject dialog = Instantiate(dialogPrefab) as GameObject;
        dialog.transform.SetParent(UIRoot.transform, false);

        Invoke("OnTimer", 1.0F);
    }


    public void OnValueChanged1(bool bCheck)
    {
        Debug.Log("toggle1" + bCheck);

        if (bCheck)
        {
            SceneManager.UnloadSceneAsync("toggle2");
            SceneManager.LoadSceneAsync("toggle1", LoadSceneMode.Additive);
        }
    }

    public void OnValueChanged2(bool bCheck)
    {
        Debug.Log("toggle2" + bCheck);

        if(bCheck)
        {
            SceneManager.UnloadSceneAsync("toggle1");
            SceneManager.LoadSceneAsync("toggle2", LoadSceneMode.Additive);
        }
    }
}
