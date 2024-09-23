using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    public Transform headBone;
    public Transform nickBone;

    LayerMask targetLayer;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Bullet"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Transform hitBone = FindClosestBone(hitPoint);
        if(hitBone == headBone || hitBone == nickBone)
        {
            print("��弦");
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