using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLegController : MonoBehaviour
{
    public GameObject leftToe;
    public GameObject rightToe;
    public GameObject leftFist;
    public GameObject rightFist;

    public string enemyTag = "Enemy";

    public float speed = 5f;
    public float timeAtTarget = 0.6f;
    public Vector3 minRange;
    public Vector3 maxRange;
    public float maxMovementTime = 2f;
    public int limbIndex = 0;

    public List<GameObject> defaultKickTargets;
    public bool debugMode = false;
    public List<GameObject> debugTargets;

    private GameObject[] limbs;
    private Vector3[] initialPositions;
    private GameObject currentTarget;
    private bool isMoving = false;
    private float movementStartTime = 0;
    public float defaultMoveInterval = 5f;

    public Vector3 boxSize = new Vector3(2.3f, 2.63f, 2.55f);
    public Vector3 boxOffset = new Vector3(-8.42f, 2.26f, 1.58f);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxOffset, boxSize);
    }
    void Start()
    {
        limbs = new GameObject[] { leftToe, rightToe, leftFist, rightFist };
        initialPositions = new Vector3[limbs.Length];
        for (int i = 0; i < limbs.Length; i++)
        {
            initialPositions[i] = limbs[i].transform.position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            limbIndex = Random.Range(0, limbs.Length);
            currentTarget = debugMode ? FindNearestDebugTarget() : FindNearestEnemy();

            if (currentTarget == null && defaultKickTargets.Count > 0)
            {
                // No enemy found, select a default target
                currentTarget = defaultKickTargets[Random.Range(0, defaultKickTargets.Count)];
            }

            if (currentTarget != null && currentTarget.activeSelf)
            {
                isMoving = true;
                movementStartTime = Time.time;
            }
        }

        if (isMoving)
        {
            // Check if the target is destroyed or disabled
            if (currentTarget == null || !currentTarget.activeSelf)
            {
                MoveLimbToInitialPosition(limbIndex);
            }
            else
            {
                MoveLimb(limbIndex);
            }
        }
    }

    void MoveLimbToInitialPosition(int limbIndex)
    {
        GameObject selectedLimb = limbs[limbIndex];
        Vector3 initialPosition = initialPositions[limbIndex];

        selectedLimb.transform.position = Vector3.MoveTowards(selectedLimb.transform.position, initialPosition, speed * Time.deltaTime);

        if (Vector3.Distance(selectedLimb.transform.position, initialPosition) < 0.02f)
        {
            isMoving = false;
            currentTarget = null;
        }
    }



    GameObject FindNearestDebugTarget()
    {
        return FindNearestTarget(debugTargets.ToArray());
    }

    GameObject FindNearestTarget(GameObject[] targets)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (var target in targets)
        {
            if (target.transform.position.x >= minRange.x && target.transform.position.x <= Mathf.Abs(maxRange.x) &&
                target.transform.position.y >= minRange.y && target.transform.position.y <= maxRange.y &&
                target.transform.position.z >= minRange.z && target.transform.position.z <= maxRange.z)
            {
                float distance = Vector3.Distance(target.transform.position, position);

                if (distance < minDistance)
                {
                    nearestTarget = target;
                    minDistance = distance;
                }
            }
        }

        return nearestTarget;
    }

    void MoveLimb(int limbIndex)
    {
        GameObject selectedLimb = limbs[limbIndex];
        Vector3 targetPosition = currentTarget.transform.position;
        Vector3 initialPosition = initialPositions[limbIndex];

        float elapsedTime = Time.time - movementStartTime;
        if (elapsedTime < maxMovementTime)
        {
            selectedLimb.transform.position = Vector3.MoveTowards(selectedLimb.transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            selectedLimb.transform.position = Vector3.MoveTowards(selectedLimb.transform.position, initialPosition, speed * Time.deltaTime);

            if (Vector3.Distance(selectedLimb.transform.position, initialPosition) < 0.01f)
            {
                isMoving = false;
                currentTarget = null;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + boxOffset, boxSize / 2, Quaternion.identity);
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < minDistance)
                {
                    nearestEnemy = hitCollider.gameObject;
                    minDistance = distance;
                }
            }
        }

        return nearestEnemy;
    }
}
