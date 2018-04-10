using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitTrigger : MonoBehaviour {

    string triggerName;
    public Animator animator;
    public void ActivateWait(string trigName, float waitTime)
    {
        triggerName = trigName;
        Invoke("TheThing", waitTime);
    }
    void TheThing()
    {
        animator.SetTrigger(triggerName);
    }
}
