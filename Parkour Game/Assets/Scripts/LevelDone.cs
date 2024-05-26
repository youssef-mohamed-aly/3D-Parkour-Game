using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class LevelDone : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument document;
    private Button button;

    private void Awake(){
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("nextbutton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayClick);
    }

    private void OnDisable(){
        button.UnregisterCallback<ClickEvent>(OnPlayClick);
    }

    private void OnPlayClick(ClickEvent evt){
        Debug.Log("You Pressed NEXT");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        this.gameObject.SetActive(false);
    }
}
