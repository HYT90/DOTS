using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;


/***
 *  To create a job, you need to:
 * 
 *  Create a struct that implements IJob.
 *  Add the member variables that the job uses (either blittable types or NativeContainer types).
 *  Create a method in your struct called Execute with the implementation of the job inside it.
 ***/
public struct MyJob : IJob
{
    public float x;
    public double y;

    public double z;

    //public NativeArray<int> array;//NativeContainer 為了讓主執行緒能訪問Job執行緒的Data所使用的Container, 在主執行緒中assign或 Constructor

    public void Execute()
    {
        z = (double)x + y;
        z = math.exp10(z);
        //array[0] = (int)z;
        Debug.Log(x + "Complete" + z);
    }
}

/*
    To schedule a job in the main thread, you must:

    Instantiate the job.
    Populate the job’s data.
    Call the Schedule method.
 */
public class IJobExample : MonoBehaviour
{
    public bool jobEnable;


    private void Update()
    {
        int l = 1000;
        if (jobEnable)
        {
            // Create a native array of a single float to store the result. This example waits for the job to complete for illustration purposes
            //NativeArray<int> _array = new NativeArray<int>(1, Allocator.TempJob);


            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(l, Allocator.TempJob);

            for (int i = 0; i < handles.Length; ++i)
            {
                MyJob myJob = new MyJob();
                myJob.x = i * 0.0005f;
                myJob.y = i * 0.000489f;
                handles[i] = myJob.Schedule();
            }

            JobHandle handleCombine = JobHandle.CombineDependencies(handles);

            handleCombine.Complete();

            //Schedule myJob, Schedule() return JobHandle as a dependency(parameter) for Schedule() of other jobs. mySecJob execute when myJob is completed;
            //JobHandle firstJobHandle = myJob.Schedule();


            //mySecJob.Schedule(firstJobHandle).Complete();
            //Wait for myJob to complete
            //firstJobHandle.Complete();

            //After all JobHandles are completed, the main thread can safely acess the (NativeContainer) _array.
            //int safelyAcess = _array[0];

            // Free the memory allocated by the result array
            handles.Dispose();
            //_array.Dispose();
        }
        else
        {
            float x;
            double y;

            double z;
            for(int i = 0; i< l; ++i)
            {
                x = i * 0.0005f;
                y = i * 0.00089f;
                z = (double)x + y;
                z = math.exp10(z);
                Debug.Log(x + "Complete" + z);
            }
        }

    }
}
