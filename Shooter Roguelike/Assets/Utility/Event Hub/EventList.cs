namespace Utility.EventCommunication
{
	public static class EventList
	{
		public const string Empty = "Empty";

		public const string ReactionToPlayer = "Reaction To Player";
		public const string EndGame = "End Game";

		public const string ItemCatch = "Item Catch";

		public const string OnClickGround = "On Click Ground";
		public const string OnClickEnemy = "On Click Enemy";
		public const string EnemyKilled = "Enemy Killed";

		public const string RoomGenerationCompleted = "Room Generation Completed";
		public const string ShakeCamera = "Shake Camera";

		public const string AddUpdatable = "Add Updatable";

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
