﻿using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkTestingScriptExperiment : MonoBehaviour
{

    public static event Action<Story> OnCreateStory;

    public TextAsset inkJSONAsset;
    private Story story;
    public Text textPrefab;
    public Button buttonPrefab;


    // Start is called before the first frame update
    public void Awake()
    {
       story = new Story(inkJSONAsset.text);
        // refreshUI();
    }


    public void refreshStory()
    {
        story = new Story(inkJSONAsset.text);
    }

    public void refreshUI()
    {
        eraseUI();


        Text storyText = Instantiate(textPrefab) as Text;

        string text = loadStoryChunk();

        List<string> tags = story.currentTags;


        //to add bold font to tags as per the tutorial
        /*
        if(tags.Count > 0)
        {
            text = "<b>" + tags[0] + "</b>" + " - " + text;

        }
        */

        
        storyText.text = text;
        storyText.transform.SetParent(this.transform, true);


        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });
        }
    }

   
    public void eraseUI()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject); 
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    
    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            //  FindObjectOfType<PlayerMovement>().Freeze();
            text = story.Continue();  
        }
        return text;
    }



    /*
     *
     * trying to move text prefab with the tags in ink.....not working well.
         List<string> tags = story.currentTags;


            if (tags.Contains("rock"))
            {
                textPrefab.alignment = TextAnchor.LowerRight;

            }
            else if (tags.Contains("me"))
            {
                textPrefab.alignment = TextAnchor.LowerCenter;
            }
            else
            {
                textPrefab.alignment = TextAnchor.LowerCenter;
            }

     */

    
}