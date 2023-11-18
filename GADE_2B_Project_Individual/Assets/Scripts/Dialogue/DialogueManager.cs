using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public MyQueue<Dialogue> dialogues;
    public Image cocoImage;
    public TextMeshProUGUI nameText, dialogueText;
    public string sceneName;

    [System.Serializable]
    public class DialogueData
    {
        public List<Dialogue> dialogueList;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string name;
        public string text;
    }

    void Start()
    {
        dialogues = new MyQueue<Dialogue>();
        LoadDialogue();
        cocoImage.enabled = false;
        DisplayNextDialogue();
    }

    void LoadDialogue()
    {
        string fileName = "";
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "BeginnerIntroduction")
        {
            fileName = "DialogueForBeginner.json";
        }
        else if (sceneName == "CheckPointIntroduction")
        {
            fileName = "DialogueForCheckpoint.json";
        }
        else if (sceneName == "AdvancedIntroduction")
        {
            fileName = "DialogueForAdvanced.json";
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            DialogueData loadedData = JsonUtility.FromJson<DialogueData>(dataAsJson);

            foreach (Dialogue dialogue in loadedData.dialogueList)
            {
                dialogues.Enqueue(dialogue);
            }
        }
        else
        {
            Debug.LogError("File not found");
        }
    
    }


    public void DisplayNextDialogue()
    {
        if (dialogues.IsEmpty())
        {
            Debug.Log("Finished with dialogues.");

            if (sceneName == "BeginnerIntroduction")
            {
                SceneManager.LoadScene("BeginnerRace");
            }
            else if (sceneName == "CheckPointIntroduction")
            {
                SceneManager.LoadScene("CheckpointRace");
            }
            else if (sceneName == "AdvancedIntroduction")
            {
                SceneManager.LoadScene("AdvancedRace");
            }

            return;
        }
        Dialogue dialogue = dialogues.Dequeue();

        


        nameText.text = dialogue.name;
        dialogueText.text = dialogue.text;

        if (dialogue.name == "Coco")
        {
            cocoImage.enabled = true;
        }
        else
        {
            cocoImage.enabled = false;
        }
    }
    public class MyQueue<T>
    {
        private List<T> elements;
        private int front;
        private int rear;

        public MyQueue()
        {
            elements = new List<T>();
            front = 0;
            rear = -1;
        }

        public void Enqueue(T item)
        {
            elements.Add(item);
            rear++;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T item = elements[front];
            front++;

            
            if (front > 1000)
            {
                elements.RemoveRange(0, front);
                rear -= front;
                front = 0;
            }

            return item;
        }

        public bool IsEmpty()
        {
            return front > rear;
        }
    }
}