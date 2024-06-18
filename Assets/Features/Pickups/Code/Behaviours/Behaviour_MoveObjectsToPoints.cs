using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Behaviour_MoveObjectsToPoints : MonoBehaviour
{
    public List<GameObject> objectsToMove = new();
    public Transform[] movePoints;
    public bool isRandomMove;
    public float delayBetweenMoves = 2f;

    public void StartMovingObjects() { StartCoroutine(MoveObjectsRoutine()); }

    public IEnumerator MoveObjectsRoutine()
    {
        if (isRandomMove)
        {
            List<int> availableIndices = Enumerable.Range(0, movePoints.Length).ToList();
            foreach (GameObject obj in objectsToMove)
            {
                int randomIndex = Random.Range(0, availableIndices.Count);
                int movePointIndex = availableIndices[randomIndex];
                obj.transform.position = movePoints[movePointIndex].position;
                availableIndices.RemoveAt(randomIndex);
                yield return new WaitForSeconds(delayBetweenMoves);
            }
        }
        else
        {
            for (int i = 0; i < objectsToMove.Count; i++)
            {
                int pointIndex = i % movePoints.Length;
                objectsToMove[i].transform.position = movePoints[pointIndex].position;
                yield return new WaitForSeconds(delayBetweenMoves);
            }
        }
    }

    public void AcceptNewObject(GameObject obj) { objectsToMove.Add(obj); }
}
