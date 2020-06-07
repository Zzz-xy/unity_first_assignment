using UnityEngine;
using System.Collections;
using TMPro;

public class Score : MonoBehaviour
{
	public int score = 0;                   


	private PlayerControl playerControl;   
	private int previousScore = 0;         


	void Awake()
	{
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update()
	{
		GetComponent<TextMeshProUGUI>().text = "Score: " + score;

		if (previousScore != score)
			playerControl.StartCoroutine(playerControl.Taunt());

		previousScore = score;
	}

}
