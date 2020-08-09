using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float magnusMag = 0.05f;
    [SerializeField] float spinAdd = 0.5f;
    [SerializeField] float speedIncrease = 0.5f;
    [SerializeField] float boundaryBounce = 0.2f;
    [SerializeField] float Zbound = 4.5f;
    Rigidbody BallRB;
    // Start is called before the first frame update
    void Start()
    {
        BallRB = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float YRotVel = BallRB.angularVelocity.y;
        float SignXVelocity = Mathf.Sign(BallRB.velocity.x) == -1 ? -1 : 1;
        BallRB.AddForce(Vector3.forward * magnusMag * -1 * SignXVelocity * YRotVel, ForceMode.Impulse);
        if (Mathf.Abs(transform.position.z)>= Zbound)
        {
            int ZbounceAdd = Mathf.Sign(transform.position.z) == -1 ? 1 : -1;
            BallRB.AddForce(Vector3.forward * boundaryBounce * ZbounceAdd, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float ZOffset = gameObject.transform.position.z - collision.gameObject.transform.position.z;
            int AwayFromPaddle = Mathf.Sign(collision.gameObject.transform.position.x - gameObject.transform.position.x) == -1 ? -1 : 1;
            float XVelocity = BallRB.velocity.x;
            float NewSpeed = ((XVelocity + Mathf.Sign(XVelocity)*speedIncrease) * -AwayFromPaddle);
            BallRB.velocity = (new Vector3(NewSpeed, 0, BallRB.velocity.z));
            BallRB.angularVelocity = Vector3.zero;
            BallRB.AddTorque(0, AwayFromPaddle * spinAdd * ZOffset, 0, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Arena"))
        {
            int ZbounceAdd = Mathf.Sign(collision.gameObject.transform.position.x - gameObject.transform.position.x) == -1 ? 1 : -1;
            BallRB.AddForce(Vector3.forward* boundaryBounce * ZbounceAdd, ForceMode.Impulse);
        }
    }
}
