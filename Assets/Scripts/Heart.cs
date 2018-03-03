using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject _heart;

    public bool Filled = true;

    public void Active(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Fill(bool fillState)
    {
        _heart.SetActive(fillState);
        Filled = fillState;
    }
}