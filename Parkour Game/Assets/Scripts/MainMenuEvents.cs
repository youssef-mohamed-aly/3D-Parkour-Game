using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument document;
    private Button button;
    private VisualElement container;

    private void Awake(){
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("startbutton") as Button;
        container = document.rootVisualElement.Q("Container");
        button.RegisterCallback<ClickEvent>(OnPlayClick);
        Time.timeScale = 0;
    }

    private void OnDisable(){
        button.UnregisterCallback<ClickEvent>(OnPlayClick);
    }


    private void OnPlayClick(ClickEvent evt){
        Debug.Log("You Pressed Start");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        this.gameObject.SetActive(false);
    }
}
