using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPointOnNavMesh
{
    public static bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 1; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                if (!Physics.CheckSphere(hit.position, 10f, 1 << LayerMask.NameToLayer("Player")))
                {
                    result = hit.position;
                    return true;
                }
            }
            else
            {
                i--;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
