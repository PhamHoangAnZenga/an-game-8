using Newtonsoft.Json;

public class VocabularyItem
{
    [JsonProperty("id")]
    public int _id;

    [JsonProperty("word")]
    public string _word;

    [JsonProperty("pronunciation")]
    public string _pronunciation;

    [JsonProperty("game_pronunciation")]
    public int _gamePronunciation;

    [JsonProperty("game_meaning")]
    public string _gameMeaning;

    [JsonProperty("meaning")]
    public string _meaning;

    [JsonProperty("type")]
    public string _type;

    [JsonProperty("explain")]
    public string _explain;

    [JsonProperty("example")]
    public string _example;

    [JsonProperty("example_meaning")]
    public string _exampleMeaning;

    [JsonProperty("audio")]
    public string _audio;

    [JsonProperty("full_meaning")]
    public string _fullMeaning;

    [JsonProperty("category")]
    public int _category;

    [JsonProperty("similar_pronunciation")]
    public string _similarPronunciation;

    [JsonProperty("game_write")]
    public string _gameWrite;
}