using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Test2 : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onbuttonClick); 
    }
    public void onbuttonClick()
    {
        SceneManager.LoadScene(0);
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(onbuttonClick);
    }
}
