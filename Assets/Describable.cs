using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Describable : MonoBehaviour
{
    public string description;
    public List<string> dialogue;

    int nextIndex;

    public string StartDescription()
    {
        nextIndex = 0;
        return description;
    }

    public string NextText()
    {
        if (dialogue == null || nextIndex >= dialogue.Count) return null;

        nextIndex += 1;

        return dialogue[nextIndex - 1];
    }
}
