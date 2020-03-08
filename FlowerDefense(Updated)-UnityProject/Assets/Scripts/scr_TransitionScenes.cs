using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class scr_TransitionScenes : MonoBehaviour
{
    [SerializeField] private bool onAnyKey;
    [SerializeField] private int newScene;
    private Animator anim;
    private bool transitioned;
    private float birthTime;
    [SerializeField] private float minWaitInSeconds;
    private Player systemInput;

    private void Start()
    {
        systemInput = ReInput.players.GetSystemPlayer();
        anim = GetComponent<Animator>();
        birthTime = Time.time;
    }
    void Update()
    {
        if (onAnyKey)
        {
            if (systemInput.GetAnyButtonDown() )
            {
                if (Time.time > birthTime + minWaitInSeconds)
                {
                    anim.SetTrigger("Transition");
                    transitioned = true;
                }
            }
        }
    }
    public void TransitionToSpecificScene(int tempNewScene)//called from another script
    {
        newScene = tempNewScene;
        anim.SetTrigger("Transition");
        transitioned = true;
    }
    private void LoadNewScene()//called from anim
    { SceneManager.LoadScene(newScene); }
}
