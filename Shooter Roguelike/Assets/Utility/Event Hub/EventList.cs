namespace Utility.EventCommunication
{
	public static class EventList
	{
		public const string Empty = "Empty";
		public const string RoomGenerationCompleted = "Room Generation Completed";
		public const string ShakeCamera = "Shake Camera";

		#region Transition Screen
		public const string TransitionOn = "TransitionOn";
		public const string TransitionOff = "TransitionOff";
		#endregion

		#region Audio
		public const string AudioPlayOneTime = "AudioPlayOneTime";
		public const string AudioPlayLoop = "AudioPlayLoop";
		public const string AudioPlayIntroLoop = "AudioPlayIntroLoop";
		public const string DestroyAudioHub = "DestroyAudioHub";
		public const string AudioStop = "AudioStop";
		public const string AudioMute = "AudioMute";
		#endregion
	}
}
