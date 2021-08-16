using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenemanager : MonoBehaviour
{
    public static Scenemanager instance;
    AudioSource[] audioSource;
    public Animator anim;
    public float transitionTime = 1f;
    private int currentSceneIndex;
    public int sceneToContinue;
    public  GameObject continueButton;
    
    private void Awake()
    {
        audioSource = GetComponents<AudioSource>();
       
        if(instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Save Deleted");
        }
    */     
    }

    public void NewGame()
    {
        audioSource[0].Play();

        Invoke("LoadNewGame", 0.85f);
        
    }

    public void QuitGame()
    {
        audioSource[0].Play();
        Invoke("LoadQuitGame", 0.85f);

       
    }

    public void RetryGame()
    {
        audioSource[0].Play();
        Invoke("LoadRetryGame", 0.5f);

    }

    public void MainMenu()
    {
        audioSource[0].Play();
        Invoke("LoadMainMenu", 0.5f);

    }

    public void ContinueButton()
    {
        audioSource[0].Play();
        Invoke("LoadContinueButton", 0.5f);
    }

    void LoadNewGame()
    {
        // SceneManager.LoadScene("Level");

        StartCoroutine(LoadLevel("Level"));
    }
    void LoadQuitGame()
    {
        Application.Quit();
       // EditorApplication.isPlaying = false;

    }
    void LoadRetryGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        SceneManager.LoadScene(sceneToContinue);
    }
   public void LoadMainMenu()
   {
        
        SceneManager.LoadScene("Main Menu");

   }
    void LoadContinueButton()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if(sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);

        }
        else
        {
           
            return;
        }
    }

    public void SaveGameData()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
    }

    IEnumerator LoadLevel(string sceneToLoad)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToLoad);

    }




}
