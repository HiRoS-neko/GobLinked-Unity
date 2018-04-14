using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class CheckDead : MonoBehaviour
    {
        [SerializeField] private List<GameObject> dead;


        private void FixedUpdate()
        {
            foreach (var d in dead)
            {
                if (d != null)
                {
                    return;
                }
            }

            SceneManager.LoadScene("Credits");
        }
    }
}