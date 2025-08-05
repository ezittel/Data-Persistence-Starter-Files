#if UNITY_EDITOR
using System.Threading;
using TMPro;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField playerNameInputField;
    public string pname;



    //private GameObject foundObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (GameManager.instance.playerName.Length >= 3)
        {
            startButton.interactable = true;
            pname = GameManager.instance.playerName;
            playerNameInputField.text = pname;
        }
        else
        {
            startButton.interactable = false;

        }
        
        playerNameInputField.onValueChanged.AddListener(OnNameInputValueChanged);
        //foundObject = GameObject.FindWithTag("Name");
    }
    private void OnNameInputValueChanged(string newName)
    {

        //GameManager.instance.PlayClickSound();
        if (newName.Length >= 3)
        {
            startButton.interactable = true;
            pname = newName;
        }
        else
        {
            startButton.interactable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNew()
    {
        
        //GameManager.instance.PlayButtonSound();
        //foundObject.SetActive(true);
        //get text and handle it
        if (GameManager.instance.playerName.Length == 0)
        {
            GameManager.instance.playerName = pname;
        }
        if (GameManager.instance.level > 0)
        {
            //reset to 0 for init
            GameManager.instance.level = 0;
        }
        //load high scores
        GameManager.instance.LoadScore();
        
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        //GameManager.instance.PlayButtonSound();
        //delay until sound plays.
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
