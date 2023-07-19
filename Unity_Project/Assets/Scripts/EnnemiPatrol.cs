using UnityEngine;

public class EnnemiPatrol : MonoBehaviour
{
    public float speed;
    /*Points between which the ennemi will move :*/
    public Transform[] waypoints;

    public int damageOnCollison = 20;

    public SpriteRenderer graphics;

    /*to the point toward the ennemi goes */
    private Transform target;
    private int destPoint;

    // Start is called before the first frame update
    void Start()
    {
        /*the ennemi target the first waypoint : */
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //to have the direction we take the target position minus the actual position of the ennemi
        Vector3 dir = target.position - this.transform.position;
        /*we move the position according to a noramlized vector : (1,1,1)  to adapt the dir vector3 calculated multiply to the speed and the deltatime(to have a continued movement in time)
         * we set as reference for the movement the world coordinate system (could use Self which is a movement according to the parents */
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        /*to switch the dir once a movment to a waypoint is done :*/
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            /*we use the modulo to come back at the first waypoint af the tab if we are at the end (ex : we are at C on {A,B,C} so we make (2+1)%3 = 0 -> come back to A index 0)*/
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollison);
        }
    }
}
