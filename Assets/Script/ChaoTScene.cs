using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaoTScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject sanGong= GameObject.Find("SanG");
        sanGong.transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("SanG");

        sanGong.transform.Find("ChengX").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("ChengX");
        sanGong.transform.Find("TaiW").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TaiW");
        sanGong.transform.Find("YuSDF").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("YuSDF");

        GameObject jiuQing = GameObject.Find("JiuQ");
        jiuQing.transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("JiuQ");
        jiuQing.transform.Find("TaiC").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TaiC");
        jiuQing.transform.Find("TaiP").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TaiP");
        jiuQing.transform.Find("WeiW").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("WeiW");
        jiuQing.transform.Find("GuangL").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("GuangL");
        jiuQing.transform.Find("TingW").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TingW");
        jiuQing.transform.Find("DaH").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("DaH");
        jiuQing.transform.Find("ZhongZ").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("ZhongZ");
        jiuQing.transform.Find("DaS").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("DaS");
        jiuQing.transform.Find("ShaoF").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("ShaoF");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}