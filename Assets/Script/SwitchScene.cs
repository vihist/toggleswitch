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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValueChanged1(bool bCheck)
    {
        Debug.Log("toggle1" + bCheck);

        if (bCheck)
        {
            SceneManager.UnloadSceneAsync("CtScene");
            SceneManager.LoadSceneAsync("TxScene", LoadSceneMode.Additive);
        }
    }

    public void OnValueChanged2(bool bCheck)
    {
        Debug.Log("toggle2" + bCheck);

        if(bCheck)
        {
            SceneManager.UnloadSceneAsync("TxScene");
            SceneManager.LoadSceneAsync("CtScene", LoadSceneMode.Additive);
        }
    }
}
