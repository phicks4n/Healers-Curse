using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Animator transition;
    public Animator battleTransition;
    public float transitionTime;
    public float battleTransitionTime;

    public GameObject levelWindow;
    public KeyCode toggleKey = KeyCode.K;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleWindow();
        }
        
    }

    public void ToggleWindow()
    {
        if(levelWindow != null)
        {
            levelWindow.SetActive(!levelWindow.activeSelf);
        }
        else
        {
            Debug.LogWarning("Test");
        }
    }



    public void NextLevel(Collider2D player, int sceneBuildIndex)
    {
        StartCoroutine(LoadLevel(player, sceneBuildIndex));
    }

    IEnumerator LoadLevel(Collider2D player, int sceneBuildIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Playered entered, so move to other level
        print("Switching Scene to " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        //Play animation
        transition.SetTrigger("End");
    }

    public void battleTransistion(Collider2D player, int sceneBuildIndex)
    {
        StartCoroutine(battleLoadLevel(player, sceneBuildIndex));
    }

    IEnumerator battleLoadLevel(Collider2D player, int sceneBuildIndex)
    {
        //Play animation
        battleTransition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(battleTransitionTime);

        //Playered entered, so move to other level
        print("Switching Scene to " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        //Play animation
        battleTransition.SetTrigger("End");
    }
   
}
