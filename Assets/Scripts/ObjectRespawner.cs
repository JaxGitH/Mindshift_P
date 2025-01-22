using UnityEngine;
using System.Collections.Generic;

public class ObjectRespawner : MonoBehaviour
{
    static public void RespawnObject(GameObject obj, Vector3 initPos, Quaternion initRot)
    {
        Instantiate(obj, initPos, initRot);
    }
}
