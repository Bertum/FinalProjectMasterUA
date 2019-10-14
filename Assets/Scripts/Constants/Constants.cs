public static class Constants
{
    public const string LANGUAGESELECTED = "LanguageSelected";
    public const string LANGUAGEINDEXSELECTED = "LanguageIndexSelected";
    public const string SCENETOLOAD = "SceneToLoad";
    public const string MUSICVOLUME = "MusicVolume";
    public const string EFFECTSVOLUME = "EffectsVolume";
    public enum EJobType { Rookie, Official, Cook, Searcher, Pilot, Medic, Carpenter};
    public enum EEndBattleStatus { Ongoing, PlayerWon, PlayerLost};
    public enum EResourceType { Food, Water, Medicine, Gold, Wood };
    public const string NEWGAME = "NewGame";
    public struct DayTime {
        public const float Night = 0.5f;
        public const float MidDay = 0.75f;
        public const float Day = 1f;
        public const float Afternoon = 1.25f;
    }
}
