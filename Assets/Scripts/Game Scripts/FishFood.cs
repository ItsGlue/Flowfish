using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject ignoreObject;
    private bool isFollowing = false;
    private bool hasFollowed = false;

    private bool inFishTank = false;

    void Start()
    {
        ignoreObject = GameObject.Find("FishTank Top");
        Collider2D thisCollider = GetComponent<Collider2D>();
        Collider2D otherCollider = ignoreObject.GetComponent<Collider2D>();

        // Ignore collisions between this object and the other object in 2D
        Physics2D.IgnoreCollision(thisCollider, otherCollider);
    }

    void Update()
    {
        if (!isFollowing && !inFishTank && hasFollowed) {
            Destroy(gameObject);
        }
        // Start following the mouse if the left mouse button is pressed, the object hasn't followed before, and is not currently following
        if (Input.GetMouseButtonDown(0) && !isFollowing && !hasFollowed)
        {
            isFollowing = true;
        }

        // Update position while the mouse button is held down and object is following
        if (Input.GetMouseButton(0) && isFollowing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0f;
            transform.position = worldPosition;
        }

        // Stop following the mouse when the button is released
        if (Input.GetMouseButtonUp(0) && isFollowing)
        {
            isFollowing = false;
            hasFollowed = true;  // Mark the object as having followed the mouse
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("fish") && !isFollowing)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("FishTankTrigger")) {
            inFishTank = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("FishTankTrigger")) {
            inFishTank = false;
        }
    }
}
