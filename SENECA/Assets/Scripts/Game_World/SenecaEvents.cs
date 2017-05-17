using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;

namespace SenecaEvents
{
    public class SceneChangeEvent : GameEvent
    {
        public readonly string sceneName;
        public SceneChangeEvent(string name)
        {
            sceneName = name;
        }
    }

    public class TopicSelectedEvent : GameEvent
    {
        public readonly string topicName;
        public readonly string npcName;

        public TopicSelectedEvent(string hartoTopic, string npc) 
	    {
            topicName = hartoTopic;
            npcName = npc;
        }
    }

    public class ToggleHARTOEvent : GameEvent
    {
        public ToggleHARTOEvent ()
		{
        }
    }

    public class ClosingHARTOForTheFirstTimeEvent : GameEvent
    {
        public ClosingHARTOForTheFirstTimeEvent()
        {
            
        }
    }

    public class TABUIButtonAppearEvent : GameEvent
    {
        public TABUIButtonAppearEvent ()
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
        public readonly string topicName;
        public EndDialogueEvent (string topic)
        {
            topicName = topic;
        }
    }

    public class DisablePlayerMovementEvent : GameEvent
    {
        public readonly bool disableMovement;
        public DisablePlayerMovementEvent(bool b)
        {
            disableMovement = b;
        }
    }

    public class EmotionSelectedEvent : GameEvent
    {
        public readonly Emotions emotion;

        public EmotionSelectedEvent(Emotions currentEmotion) 
	    {
            //this.hartoEmotion = hartoEmotion;
            emotion = currentEmotion;
        }
    }

    public class MoveMomEvent : GameEvent
    {
        public MoveMomEvent()
        {

        }
    }

    public class BeginTutorialEvent : GameEvent
    {
        public BeginTutorialEvent()
        {
            
        }
    }

    public class WaitingForEmotionalInputEvent : GameEvent
    {
        public WaitingForEmotionalInputEvent()
        {

        }
    }

    public class EmotionalInputReceived : GameEvent
    {
        public EmotionalInputReceived()
        {
            
        }
    }

    public class BeginGameEvent : GameEvent
    {
        public BeginGameEvent()
        {
        }
    }

    public class AstridTalksToHARTOEvent :GameEvent
    {
        public readonly bool talkingToHARTO;
        public AstridTalksToHARTOEvent(bool _talkingToHARTO)
        {
            talkingToHARTO = _talkingToHARTO;
        }
    }

    public class InteractableEvent : GameEvent
    {
        public readonly bool talkingToHARTO;
        public readonly bool armUp;
        public InteractableEvent(bool _talkingToHARTO, bool _armUp)
        {
            talkingToHARTO = _talkingToHARTO;
            armUp = _armUp;
        }
    }

    public class EndGameEvent : GameEvent
    {
        public EndGameEvent()
        {
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

    public class RecordingIsOverEvent : GameEvent
    {
        public RecordingIsOverEvent()
        {
            
        }
    }

    public class PuzzleCompletedEvent : GameEvent
    {
        public PuzzleCompletedEvent()
        {
            
        }
    }
}
