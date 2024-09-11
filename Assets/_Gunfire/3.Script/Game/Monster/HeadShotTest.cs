using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShotTest : MonoBehaviour
{
    public Transform headBone;
    public Transform nickBone;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Transform hitBone = FindClosestBone(hitPoint);
        if(hitBone == headBone || hitBone == nickBone)
        {
            print("Çìµå¼¦");
        }
    }

    Transform FindClosestBone(Vector3 hitPoint)
    {
        Transform closesBone = null;
        float closestDistance = float.MaxValue;

        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if(skinnedMeshRenderer != null)
        {
            foreach(Transform bone in skinnedMeshRenderer.bones)
            {
                float distance = Vector3.Distance(bone.position, hitPoint);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closesBone = bone;
                }
            }
        }
        print($"closesBone => {closesBone}");
        return closesBone;
    }
}
