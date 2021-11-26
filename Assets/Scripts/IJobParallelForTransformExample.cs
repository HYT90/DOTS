using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompatible]
public struct MyJobForTransform : IJobParallelForTransform
{
    public NativeArray<float> zSpeed;
    public float deltaTime;


    //Jobexecute盢盿皚把计–じだ磅︽笆
    public void Execute(int index, TransformAccess transform)
    {
        transform.position += Vector3.forward * zSpeed[index] * deltaTime;
        if(transform.position.z > 100)
        {
            zSpeed[index] = -math.abs(zSpeed[index]);
        }else if(transform.position.z < -100)
        {
            zSpeed[index] = +math.abs(zSpeed[index]);
        }

        for (int i = 0; i < 1000; i++)
        {
            float j = math.exp((float)i);
            j *= j;
        }
    }
}

public class IJobParallelForTransformExample : MonoBehaviour
{
    public GameObject cube;
    public int l = 10;
    List<GameObject> cubeList;
    float[] zSpeed;
    Transform[] cubeTrans;

    private void Start()
    {
        cubeList = new List<GameObject>();
        zSpeed = new float[l];
        cubeTrans = new Transform[l];
        for (int i = 0; i < l; i++)
        {
            cubeList.Add(Instantiate(cube));
            cubeList[i].transform.position = Vector3.right * UnityEngine.Random.Range(-50f, 50f);
            cubeTrans[i] = cubeList[i].transform;
            zSpeed[i] = UnityEngine.Random.Range(-20f, 20f);
        }
    }

    public bool jobOn;
    public void Update()
    {
        if (jobOn)
        {
            //TransformAcessArray Schedule()把计
            TransformAccessArray tarray = new TransformAccessArray(cubeTrans);
            NativeList<float> _zSpeed = new NativeList<float>(l, Allocator.TempJob);

            _zSpeed.CopyFrom(zSpeed);

            MyJobForTransform myJob = new MyJobForTransform();
            myJob.zSpeed = _zSpeed;
            myJob.deltaTime = Time.deltaTime;
            myJob.Schedule(tarray).Complete();

            for (int i = 0; i < l; i++)
            {
                zSpeed[i] = _zSpeed[i];
            }

            tarray.Dispose();
            _zSpeed.Dispose();
        }
        else
        {
            float deltaTime = Time.deltaTime;

            for(int i = 0; i < l; ++i)
            {
                cubeList[i].transform.position += Vector3.forward * zSpeed[i] * deltaTime;
                if (cubeList[i].transform.position.z > 100)
                {
                    zSpeed[i] = -math.abs(zSpeed[i]);
                }
                else if (cubeList[i].transform.position.z < -100)
                {
                    zSpeed[i] = +math.abs(zSpeed[i]);
                }

                REALHARDMATH();
            }
        }
    }


    void REALHARDMATH()
    {
        for(int i = 0; i<1000; i++)
        {
            float j = math.exp((float)i);
            j *= j;
        }
    }
}
