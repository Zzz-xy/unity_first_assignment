using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Test : MonoBehaviour
{
    private Button button;
	public GameObject gamePanel;
	private bool paused; 
	EventSystem eventsystem;
	GraphicRaycaster RaycastInCanvas;

	private void Awake()
    {
        button = GetComponent<Button>();
		button.onClick.AddListener(onbuttonClick);
		RaycastInCanvas = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
		paused = false;
	}
	
	public void onbuttonClick()
	{
		paused = paused ? false : true;
		
        if (paused)
        {
			gamePanel.SetActive(true);
        }
        else
        {
			gamePanel.SetActive(false);
		}
		
	}
	private void OnDestroy()
	{
		button.onClick.RemoveListener(onbuttonClick);
	}
	bool CheckGuiRaycastObjects()
	{

		PointerEventData eventData = new PointerEventData(eventsystem);
		eventData.pressPosition = Input.mousePosition;
		eventData.position = Input.mousePosition;
		List<RaycastResult> list = new List<RaycastResult>();
		RaycastInCanvas.Raycast(eventData, list);
		return list.Count > 0;

	}
}
