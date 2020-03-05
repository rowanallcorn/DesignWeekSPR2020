using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_TransitionScenes : MonoBehaviour
{
    [SerializeField] private bool onAnyKey;
    [SerializeField] private int newScene;
    private Animator anim;
    private bool transitioned;

    private void Start()
    {  anim = GetComponent<Animator>();  }
    void Update()
    {
        if (onAnyKey)
        {
            if (Input.anyKey&& !transitioned)
            { 
                anim.SetTrigger("Transition");
                transitioned = true;
            }
        }
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene(newScene);
    }
}
