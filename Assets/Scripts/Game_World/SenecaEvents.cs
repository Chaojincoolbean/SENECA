using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;

namespace SenecaEvents
{
    public class TopicSelectedEvent : GameEvent
    {
        public readonly HARTOTuningv3Script hartoTopic;
        public readonly string topicName;
        public readonly Player player;

        public TopicSelectedEvent(string hartoTopic, Player player) 
	    {
            topicName = hartoTopic;
            this.player = player;
        }
    }

    public class ToggleHARTOEvent : GameEvent
    {
        public ToggleHARTOEvent ()
        {
            
        }
    }
    public class BeginDialogueEvent : GameEvent
    {
        public BeginDialogueEvent ()
        {
        
        }
    }

     public class EndDialogueEvent : GameEvent
    {
        public EndDialogueEvent ()
        {
            
        }
    }

    public class EmotionSelectedEvent : GameEvent
    {
        public readonly HARTOTuningv3Script hartoEmotion;
        public readonly Emotions emotion;

        public EmotionSelectedEvent(Emotions currentEmotion) 
	    {
            //this.hartoEmotion = hartoEmotion;
            emotion = currentEmotion;
        }
    }

    public class RecordingModeToggledEvent : GameEvent
    {
        public RecordingModeToggledEvent()
        {

        }
    }

    public class RecordingFolderSelectedEvent : GameEvent
    {
        public readonly string folder;
        public RecordingFolderSelectedEvent(string selectedFolder)
        {
            folder = selectedFolder;
        }
    }

    public class RecordingSelectedEvent : GameEvent
    {
        public readonly string recording;
        public RecordingSelectedEvent(string thisRecording)
        {
            recording = thisRecording;
        }
    }
}
