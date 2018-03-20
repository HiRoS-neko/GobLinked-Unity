using UnityEngine;

public class Item : MonoBehaviour
{
    public Goblin.GoblinType GoblinType;
    public string Name;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goblin"))
        {
            other.gameObject.GetComponent<Goblin>().Items.AddItem(this);
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}