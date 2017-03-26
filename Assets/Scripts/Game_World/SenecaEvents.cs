using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using UnityStandardAssets.Characters.FirstPerson;

namespace SenecaEvents
{
    public class TopicSelectedEvent : GameEvent
    {
        public readonly HARTOTuningv3Script hartoTopic;
        public readonly Player player;

        public TopicSelectedEvent(HARTOTuningv3Script hartoTopic, Player player) 
	    {
            this.hartoTopic = hartoTopic;
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

        public EmotionSelectedEvent(HARTOTuningv3Script hartoEmotion) 
	    {
            this.hartoEmotion = hartoEmotion;
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
        public readonly string folder;
        public readonly string recording;
        public RecordingSelectedEvent(string character, string thisRecording)
        {
            folder = character;
            recording = thisRecording;
        }
    }
}
