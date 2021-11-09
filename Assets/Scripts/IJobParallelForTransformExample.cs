using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;

public struct MyJobForTransform : IJobParallelForTransform
{
    public void Execute(int index, TransformAccess _transform)
    {
        
    }
}

public class IJobParallelForTransformExample : MonoBehaviour
{

    private void Start()
    {
        
    }
}
