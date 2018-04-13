using UnityEngine;

public class Item : MonoBehaviour
{
    public string Description;
    public Goblin.GoblinType GoblinType;
    public string Name;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goblin"))
        {
            var temp = other.gameObject.GetComponent<Goblin>();
            temp.Items.AddItem(this);
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}