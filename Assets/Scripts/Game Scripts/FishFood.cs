using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject ignoreObject;
    private bool isFollowing = false;
    private bool hasFollowed = false;

    void Start()
    {
        ignoreObject = GameObject.Find("FishTank Top");
        Collider2D thisCollider = GetComponent<Collider2D>();
        Collider2D otherCollider = ignoreObject.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(thisCollider, otherCollider);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFollowing && !hasFollowed)
        {
            isFollowing = true;
        }
        if (Input.GetMouseButton(0) && isFollowing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0f;
            transform.position = worldPosition;
        }
        if (Input.GetMouseButtonUp(0) && isFollowing)
        {
            isFollowing = false;
            hasFollowed = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("fish") && !isFollowing)
        {
            Destroy(gameObject);
        }
    }
}
