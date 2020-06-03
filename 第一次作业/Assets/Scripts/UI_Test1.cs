using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_Test1 : MonoBehaviour
{
	private Button button;
	private bool paused;
	public AudioMixer mixer;

	private void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(onbuttonClick);
		paused = false;
	}

	public void onbuttonClick()
	{
		Time.timeScale = paused ? 1 : 0;
		if (paused)
		{
			mixer.SetFloat("Master", 0);
		}
		else
		{
			mixer.SetFloat("Master", -80);
		}
		paused = paused ? false : true;
	}
	private void OnDestroy()
	{
		button.onClick.RemoveListener(onbuttonClick);
	}
}
