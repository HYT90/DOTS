using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public struct MyJobParallelFor : IJobParallelFor
{
    //[ReadOnly]//�׹��}�C�̪����� ��Ū
    public NativeArray<float> a;

    //[WriteOnly]//�߼g
    public NativeArray<float> array;

    //Job��@NativeContainer���C�Ӥ�������ʧ@, index����������m
    public void Execute(int index)
    {
        a[index] = .5f * index;
        for (int j = 0; j < 100; ++j)
        {
            a[index] *= j;
        }
        array[index] = a[index];
        Debug.Log(array[index]);
    }
}

public class IJobParallelForExample : MonoBehaviour
{
    public bool isJob;

    void Update()
    {
        if (!isJob)
        {
            float[] a = new float[100];
            float[] b = new float[100];

            for(int i = 0; i < a.Length; ++i)
            {
                a[i] = .5f * i; 
                for(int j = 0; j< 100; ++j)
                {
                    a[i] *= j;
                }
                b[i] = a[i];
                Debug.Log(b[i]);
            }

            return;
        }


        NativeArray<float> _a = new NativeArray<float>(100, Allocator.TempJob);
        var _array = new NativeArray<float>(100, Allocator.TempJob);

        MyJobParallelFor jobParallel = new MyJobParallelFor();
        jobParallel.a = _a;
        jobParallel.array = _array;

        // Schedule the job with one Execute per index in the results array and only 1 item per processing batch
        //Schedule(int length, int batches), �a�JNativeContainer�����ץH�έn���t���妸��
        JobHandle jobHandle = jobParallel.Schedule(_array.Length, 10);

        jobHandle.Complete();

        _a.Dispose();
        _array.Dispose();
    }
}
