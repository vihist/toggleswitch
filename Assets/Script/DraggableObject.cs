using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IPointerDownHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        panelRectTransform = transform as RectTransform;
        parentRectTransform = panelRectTransform.parent as RectTransform;
    }

    public void OnPointerDown(PointerEventData data)
    {
        // 记录当前面板起点  
        originalPanelLocalPosition = panelRectTransform.localPosition;
        // 通过屏幕中的鼠标点，获取在父节点中的鼠标点  
        // parentRectTransform:父节点  
        // data.position:当前鼠标位置  
        // data.pressEventCamera:当前事件的摄像机  
        // originalLocalPointerPosition:获取当前鼠标起点  
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null || parentRectTransform == null)
            return;
        Vector2 localPointerPosition;
        // 获取本地鼠标位置  
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition))
        {
            // 移动位置 = 本地鼠标当前位置 - 本地鼠标起点位置  
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            // 当前面板位置 = 面板起点 + 移动位置  
            panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }
    }

    // 鼠标起点  
    private Vector2 originalLocalPointerPosition;
    // 面板起点  
    private Vector3 originalPanelLocalPosition;
    // 当前面板  
    private RectTransform panelRectTransform;
    // 父节点,这个最好是UI父节点，因为它的矩形大小刚好是屏幕大小  
    private RectTransform parentRectTransform;
}
