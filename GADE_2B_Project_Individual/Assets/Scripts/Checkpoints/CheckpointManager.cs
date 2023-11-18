using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CustomStack<T>
{
    private List<T> elements = new List<T>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Push(T item)
    {
        elements.Add(item);
    }

    public T Pop()
    {
        if (elements.Count == 0)
        {
            throw new System.InvalidOperationException("Empty stack");
        }

        T item = elements[elements.Count - 1];
        elements.RemoveAt(elements.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (elements.Count == 0)
        {
            throw new System.InvalidOperationException("Empty stack");
        }

        return elements[elements.Count - 1];
    }
}

public class CheckpointManager : MonoBehaviour
{
    public CustomStack<Transform> checkpoints = new CustomStack<Transform>();

    private bool raceCompleted = false;

    void Start()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");

        System.Array.Sort(checkpointObjects, (x, y) => x.name.CompareTo(y.name));

        for (int i = checkpointObjects.Length - 1; i >= 0; i--)
        {
            checkpoints.Push(checkpointObjects[i].transform);
            checkpointObjects[i].SetActive(false);
        }

        if (checkpoints.Count > 0)
        {
            checkpoints.Peek().gameObject.SetActive(true);
        }
    }

    public void CheckpointReached(Checkpoint checkpoint)
    {
        Transform previousCheckpoint = checkpoints.Pop();

        Destroy(previousCheckpoint.gameObject);

        if (checkpoints.Count > 0)
        {
            checkpoints.Peek().gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (checkpoints.Count == 0 && !raceCompleted)
        {
            Debug.Log("Finished!");
            raceCompleted = true;
        }
    }
}
