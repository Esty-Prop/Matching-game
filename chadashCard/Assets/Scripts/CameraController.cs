using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerOne;
    public Vector3 offset;

    Vector3 gizmoPos;
    public float smoothspeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraStartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerOne != null && GameManager.instance.playerList.Count< 2)
        {
            Vector3 desiredPosition = playerOne.transform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed * Time.deltaTime);
            transform.position = smoothPosition;
        }
        else if(GameManager.instance.playerList.Count >= 2)
        {
            Vector3 desiredPosition = findControid() + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed * Time.deltaTime);
            transform.position = smoothPosition;
            gizmoPos = findControid();
        }
    }

    IEnumerator CameraStartDelay()
    {
        yield return new WaitForSeconds(0.2f);
        playerOne = GameManager.instance.playerList[0].gameObject.transform;
        transform.position = playerOne.transform.position + offset;
    }

    Vector3 findControid()
    {
        var totalx = 0f;
        var totaly = 0f;
        var totalz = 0f;

        foreach(var player in GameManager.instance.playerList)
        {
            totalx += player.transform.parent.transform.position.x;
            totaly += player.transform.parent.transform.position.x;
            totalz += player.transform.parent.transform.position.x;
        }
        var centerx = totalx / GameManager.instance.playerList.Count;
        var centery = totaly / GameManager.instance.playerList.Count;
        var centerz = totalz / GameManager.instance.playerList.Count;

        return new Vector3(centerx, centery, centerz);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gizmoPos, new Vector3(1, 1, 0));
    }
}
