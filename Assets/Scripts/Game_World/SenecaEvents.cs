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
        public readonly FirstPersonController player;

        public TopicSelectedEvent(HARTOTuningv3Script hartoTopic, FirstPersonController player) 
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
}
