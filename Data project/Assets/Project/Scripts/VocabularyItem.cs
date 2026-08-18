using Newtonsoft.Json;

public class VocabularyItem
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("word")]
    public string Word;

    [JsonProperty("pronunciation")]
    public string Pronunciation;

    [JsonProperty("game_pronunciation")]
    public int HamePronunciation;

    [JsonProperty("game_meaning")]
    public string HameMeaning;

    [JsonProperty("meaning")]
    public string Meaning;

    [JsonProperty("type")]
    public string Type;

    [JsonProperty("explain")]
    public string Explain;

    [JsonProperty("example")]
    public string Example;

    [JsonProperty("example_meaning")]
    public string ExampleMeaning;

    [JsonProperty("audio")]
    public string Audio;

    [JsonProperty("full_meaning")]
    public string FullMeaning;

    [JsonProperty("category")]
    public int Category;

    [JsonProperty("similar_pronunciation")]
    public string SimilarPronunciation;

    [JsonProperty("game_write")]
    public string GameWrite;
}