using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TramNav : MonoBehaviour
{
    private NavMeshAgent NavAgent;
    public Transform[] CheckPoints;

    int index = 0;
    private void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        NavAgent.destination = CheckPoints[index].position;
        Debug.Log(NavAgent.destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint") {

            Debug.Log("Station reached!");
            StartCoroutine(Wait());
            
           
           // Destroy(other.gameObject);
        }
    }

    IEnumerator Wait() {

        yield return new WaitForSeconds(2);
        if (index == CheckPoints.Length - 1)
        {
            System.Array.Reverse(CheckPoints);
            index = 1;
        }
        else
        index++;
    }

    /*IEnumerator MoveObject() 
    {
        int i = 0;
        navAg.destination = destinationMove[i].position;
        while (i < destinationMove.Length) 
        {
            navAg.destination = destinationMove[i].position;
            i++;
            yield return new WaitForSeconds(1f);
            Debug.Log(i);
            Debug.Log(destinationMove[i].position);
        }
    }*/
}
