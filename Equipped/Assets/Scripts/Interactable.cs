using UnityEngine;

public class Interactable : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;

    public float radius = 1f;
    public string interactionVerb = "Interact";

    bool isFocused = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten.
        Debug.Log("Interacting with: " + transform.name);
    }

    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    Debug.Log("Interact");
                    Interact();
                    hasInteracted = true;
                }
            }
        }
    }

    public void OnFocused (Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
    }
    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
