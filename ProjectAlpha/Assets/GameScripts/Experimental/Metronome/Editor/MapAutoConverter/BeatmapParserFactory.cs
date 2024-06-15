namespace ProjectAlpha
{
    public static class BeatmapParserFactory
    {
        public static IBeatmapParser CreateOsuParser()
        {
            return new OsuBeatmapParser();
        }

        public static IBeatmapParser CreateCryptOfTheNecroDancerParser()
        {
            return new CryptOfTheNecroDancerParser();
        }
    }
}