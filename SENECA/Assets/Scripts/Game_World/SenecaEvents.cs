using ChrsUtils.ChrsEventSystem.GameEvents;

#region SenecaEvents.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    This script serves as a holdiong place for all events in the game. In short a script can assign a delegate        */
/*    To one of these classes and when using Services.Events.Fire() all functions attached to that class will be        */
/*    executed                                                                                                          */
/*                                                                                                                      */
/*    Event List as of 5/20/2017:                                                                                       */
/*           AstridTalksToHARTOEvent                                                                                    */
/*           BeginDialogueEvent                                                                                         */
/*           BeginGameEvent                                                                                             */
/*           BeginTutorialEvent                                                                                         */
/*           ClosingHARTOForTheFirstTimeEvent                                                                           */
/*           DisablePlayerMovementEvent                                                                                 */
/*           EmotionalInputReceived                                                                                     */
/*           EmotionSelectedEvent                                                                                       */
/*           EndDialogueEvent                                                                                           */
/*           EndGameEvent                                                                                               */
/*           InteractableEvent                                                                                          */
/*           MoveMomEvent                                                                                               */
/*           PuzzleCompletedEvent                                                                                       */
/*           RecordingFolderSelectedEvent                                                                               */
/*           RecordingIsOverEvent                                                                                       */
/*           RecordingModeToggledEvent                                                                                  */
/*           RecordingSelectedEvent                                                                                     */
/*           SceneChangeEvent                                                                                           */
/*           TABUIButtonAppearEvent                                                                                     */
/*           ToggleHARTOEvent                                                                                           */
/*           TopicSelectedEvent                                                                                         */
/*           WaitingForEmotionalInputEvent                                                                              */
/*           WASDUIAppearEvent                                                                                          */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
namespace SenecaEvents
{
    public class AstridTalksToHARTOEvent : GameEvent
    {
        public readonly bool talkingToHARTO;
        public AstridTalksToHARTOEvent(bool _talkingToHARTO)
        {
            talkingToHARTO = _talkingToHARTO;
        }
    }

    public class BeginDialogueEvent : GameEvent
    {
        public BeginDialogueEvent() {   }
    }

    public class BeginGameEvent : GameEvent
    {
        public BeginGameEvent() {   }
    }

    public class BeginTutorialEvent : GameEvent
    {
        public BeginTutorialEvent() {   }
    }

    public class ClosingHARTOForTheFirstTimeEvent : GameEvent
    {
        public ClosingHARTOForTheFirstTimeEvent() { }
    }

    public class DisablePlayerMovementEvent : GameEvent
    {
        public readonly bool disableMovement;
        public DisablePlayerMovementEvent(bool b)
        {
            disableMovement = b;
        }
    }

    public class EmotionalInputReceived : GameEvent
    {
        public EmotionalInputReceived() {   }
    }

    public class EmotionSelectedEvent : GameEvent
    {
        public readonly Emotions emotion;

        public EmotionSelectedEvent(Emotions currentEmotion)
        {
            emotion = currentEmotion;
        }
    }

    public class EndDialogueEvent : GameEvent
    {
        public readonly string topicName;
        public EndDialogueEvent(string topic)
        {
            topicName = topic;
        }
    }

    public class EndGameEvent : GameEvent
    {
        public EndGameEvent() { }
    }

    public class InteractableEvent : GameEvent
    {
        public readonly bool talkingToHARTO;
        public readonly bool armUp;
        public readonly bool disableMovement;
        public InteractableEvent(bool _talkingToHARTO, bool _armUp, bool _disableMovement)
        {
            talkingToHARTO = _talkingToHARTO;
            armUp = _armUp;
            disableMovement = _disableMovement;
        }
    }

    public class MoveMomEvent : GameEvent
    {
        public MoveMomEvent() {  }
    }

    public class PuzzleCompletedEvent : GameEvent
    {
        public PuzzleCompletedEvent() { }
    }

    public class RecordingFolderSelectedEvent : GameEvent
    {
        public readonly string folder;
        public RecordingFolderSelectedEvent(string selectedFolder)
        {
            folder = selectedFolder;
        }
    }

    public class RecordingIsOverEvent : GameEvent
    {
        public RecordingIsOverEvent() { }
    }

    public class RecordingModeToggledEvent : GameEvent
    {
        public RecordingModeToggledEvent() { }
    }

    public class RecordingSelectedEvent : GameEvent
    {
        public readonly string recording;
        public RecordingSelectedEvent(string thisRecording)
        {
            recording = thisRecording;
        }
    }

    public class SceneChangeEvent : GameEvent
    {
        public readonly string sceneName;
        public SceneChangeEvent(string name)
        {
            sceneName = name;
        }
    }

    public class TABUIButtonAppearEvent : GameEvent
    {
        public TABUIButtonAppearEvent() {   }
    }

    public class ToggleHARTOEvent : GameEvent
    {
        public ToggleHARTOEvent() { }
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

    public class WaitingForEmotionalInputEvent : GameEvent
    {
        public WaitingForEmotionalInputEvent() { }
    }

    public class WASDUIAppearEvent : GameEvent
    {
    	public WASDUIAppearEvent() {    }

    }
}
