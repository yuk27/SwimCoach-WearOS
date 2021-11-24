using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public TextAsset TextInput;
    Dictionary<string, Dictionary<string,Dictionary<string,List<string>>>> workout = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
    string currentTitle;
    string currentSubtitle1;
    string currentSubtitle2;

    string GetClass(string line){
        string auxTitle = line.Split('>')[0];
        return auxTitle.Substring(12, (auxTitle.Length - 13));
    }

    string GetContent(string line){
        string auxLine = line.Split('>')[1];
        return auxLine.Split('<')[0];
    }

    void AddWorkoutInfo(string line){

        var type = GetClass(line);
        var content = GetContent(line);

        if (content == "Created with "){
            return;
        }

        if (type == "title"){
            currentTitle = content;
            currentSubtitle1 = "Workout_Description";
            currentSubtitle2 = "Workout_Description";
            workout[currentTitle] = new Dictionary<string, Dictionary<string,List<string>>>();
            workout[currentTitle][currentSubtitle1] = new Dictionary<string,List<string>>();
            workout[currentTitle][currentSubtitle1][currentSubtitle2] = new List<string>();
            return;
        }
        if (type == "sub-title-1"){
            currentSubtitle1 = content;
            workout[currentTitle][currentSubtitle1] = new Dictionary<string,List<string>>();
            return;
        }
        if (type == "sub-title-2"){
            currentSubtitle2 = content;
            workout[currentTitle][currentSubtitle1][currentSubtitle2] = new List<string>();
            return;
        }
        else {
            workout[currentTitle][currentSubtitle1][currentSubtitle2].Add(content);
        }

    }

    void Start()
    {
        char[] newline = {'\n'};
        string[] parts = (TextInput.ToString().Split(new string[]{ "</style>"}, System.StringSplitOptions.RemoveEmptyEntries)[1]).Split(newline, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            if (part != "<br/>") {
                AddWorkoutInfo(part);
            }
            
        }
    }


    void Update()
    {

    }

}
