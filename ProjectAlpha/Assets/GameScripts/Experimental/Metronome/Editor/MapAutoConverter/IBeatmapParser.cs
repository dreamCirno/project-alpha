namespace ProjectAlpha
{
    public interface IBeatmapParser
    {
        MetronomeMap Parse(string content);
    }
}