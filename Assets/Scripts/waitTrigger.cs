using UnityEngine;

public class waitTrigger : MonoBehaviour
{
    public Animator animator;

    private string triggerName;

    public void ActivateWait(string trigName, float waitTime)
    {
        triggerName = trigName;
        Invoke("TheThing", waitTime);
    }

    private void TheThing()
    {
        animator.SetTrigger(triggerName);
    }
}