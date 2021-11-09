using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Jobs;

public struct MyJobForTransform : IJobParallelForTransform
{
    public NativeArray<Vector3> dir;


    //Job的execute將帶入的陣列參數的每一個元素分別執行動作
    public void Execute(int index, TransformAccess transform)
    {
        transform.position += dir[index];
    }
}

public class IJobParallelForTransformExample : MonoBehaviour
{
    //TransformAcessArray 作為Schedule()的參數
    TransformAccessArray tarray;
    public GameObject cube;

    private void Start()
    {
        int l = 1000;
        NativeArray<Vector3> _dir = new NativeArray<Vector3>(l, Allocator.Persistent);

        tarray = new TransformAccessArray(l);

        for (int i = 0; i < l; i++)
        {
            _dir[i] = Vector3.one * Random.Range(-10f, 10f);
            tarray.Add(Instantiate(cube).transform);
        }

        MyJobForTransform myJob = new MyJobForTransform();
        myJob.dir = _dir;
        myJob.Schedule(tarray).Complete();

        tarray.Dispose();
        _dir.Dispose();
    }
}
