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
    private float birthTime;
    [SerializeField] private float minWaitInSeconds;

    private void Start()
    {
        anim = GetComponent<Animator>();
        birthTime = Time.time;
    }
    void Update()
    {
        if (onAnyKey)
        {
            if (Input.anyKey && !transitioned&&Time.time>birthTime+ minWaitInSeconds)
            {
                anim.SetTrigger("Transition");
                transitioned = true;
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
