using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour {

    static bool bFlag = false;

    //public Toggle toggle1;
    //public Toggle toggle2;

    // Use this for initialization
    void Start()
    {
        //toggle1 = GameObject.Find("Toggle1").GetComponent<Toggle>();
        //toggle2 = GameObject.Find("Toggle2").GetComponent<Toggle>();

        //toggle1.onValueChanged.AddListener(OnValueChanged1);
        //toggle2.onValueChanged.AddListener(OnValueChanged2);
        //if (!bFlag)
        //{
        //    DontDestroyOnLoad(GameObject.Find("Canvas"));
        //    DontDestroyOnLoad(GameObject.Find("toggleSwitchScript"));
        //    bFlag = true;
        //}

        //DontDestroyOnLoad(this);

        GameObject UIRoot = GameObject.Find("Canvas");
        Object dialogPrefab = Resources.Load("EasyMenu/_Prefabs/Dialog") as Object;
        GameObject dialog = Instantiate(dialogPrefab) as GameObject;
        dialog.transform.SetParent(UIRoot.transform, false);
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

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void StartGame(string strName)
    {
        SceneManager.LoadScene(strName, LoadSceneMode.Single);
    }
}
