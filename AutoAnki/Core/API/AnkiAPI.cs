using AutoAnki.Core.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoAnki.Core.API
{
    public static class AnkiAPI
    {
        private static string _endpoint => $"http://{Configuration.Instance.AnkiConnectIP}:{Configuration.Instance.AnkiConnectPort}";

        // ------------------------------------------------------------
        // INTERNAL: Build JSON
        // ------------------------------------------------------------
        private static string BuildJson(string action, object parameters)
        {
            var req = new AnkiRequest();
            req.Action = action;
            req.Version = 5;
            req.Params = parameters;

            var options = new JsonSerializerOptions();
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            return JsonSerializer.Serialize(req, options);
        }

        // ------------------------------------------------------------
        // 1. GET VERSION (SYNC)
        // ------------------------------------------------------------
        public static int GetVersion()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);

                    string json = BuildJson("version", new EmptyParams());
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                        client.PostAsync(_endpoint, content).GetAwaiter().GetResult();

                    if (!response.IsSuccessStatusCode)
                        return 0;

                    string raw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    AnkiResponse<int> result = JsonSerializer.Deserialize<AnkiResponse<int>>(raw);

                    if (result == null || result.Error != null)
                        return 0;

                    return result.Result;
                }
            }
            catch
            {
                return 0;
            }
        }

        // ------------------------------------------------------------
        // 2. FIND NOTES (SYNC)
        // ------------------------------------------------------------
        public static List<long> FindNotes(string query)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);

                    var p = new FindNotesParams();
                    p.Query = query;

                    string json = BuildJson("findNotes", p);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                        client.PostAsync(_endpoint, content).GetAwaiter().GetResult();

                    if (!response.IsSuccessStatusCode)
                        return new List<long>();

                    string raw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    AnkiResponse<List<long>> result =
                        JsonSerializer.Deserialize<AnkiResponse<List<long>>>(raw);

                    if (result == null || result.Error != null || result.Result == null)
                        return new List<long>();

                    return result.Result;
                }
            }
            catch
            {
                return new List<long>();
            }
        }

        // ------------------------------------------------------------
        // 3. UPDATE NOTE FIELDS (SYNC)
        // ------------------------------------------------------------
        public static bool UpdateNoteFields(long noteId, Dictionary<string, string> fields)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);

                    var p = new UpdateNoteFieldsParams();
                    p.Note = new UpdateNoteFieldsParams.NoteData();
                    p.Note.Id = noteId;
                    p.Note.Fields = fields;

                    string json = BuildJson("updateNoteFields", p);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                        client.PostAsync(_endpoint, content).GetAwaiter().GetResult();

                    if (!response.IsSuccessStatusCode)
                        return false;

                    string raw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    AnkiResponse<object> result =
                        JsonSerializer.Deserialize<AnkiResponse<object>>(raw);

                    if (result == null || result.Error != null)
                        return false;

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // ------------------------------------------------------------
        // 4. GET LATEST NOTE ID (SYNC) - Where no picture is present
        // ------------------------------------------------------------
        public static long? GetLatestNoteId()
        {
            try
            {
                List<long> notes = FindNotes("added:1 Picture:");

                if (notes.Count < 1)
                    return 0;

                long max = 0;
                foreach (long id in notes)
                {
                    if (id > max)
                        max = id;
                }

                return max;
            }
            catch
            {
                return null;
            }
        }

        // ------------------------------------------------------------
        // 5. GET NOTE INFO (SYNC)
        // ------------------------------------------------------------
        public static NoteInfo? GetNoteById(long noteId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1);

                    var p = new NotesInfoParams();
                    p.Notes.Add(noteId);

                    string json = BuildJson("notesInfo", p);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                        client.PostAsync(_endpoint, content).GetAwaiter().GetResult();

                    if (!response.IsSuccessStatusCode)
                        return null;

                    string raw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    AnkiResponse<List<NoteInfo>> result =
                        JsonSerializer.Deserialize<AnkiResponse<List<NoteInfo>>>(raw);

                    if (result == null || result.Error != null || result.Result == null)
                        return null;

                    return result.Result.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
    }

    // ------------------------------------------------------------
    // REQUEST + RESPONSE MODELS
    // ------------------------------------------------------------
    public class AnkiRequest
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("params")]
        public object Params { get; set; }

        public AnkiRequest()
        {
            Action = "";
            Params = new EmptyParams();
        }
    }

    public class AnkiResponse<T>
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        public AnkiResponse()
        {
            Result = default(T);
            Error = null;
        }
    }

    // ------------------------------------------------------------
    // PARAMETER OBJECTS
    // ------------------------------------------------------------
    public class EmptyParams { }

    public class FindNotesParams
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        public FindNotesParams()
        {
            Query = "";
        }
    }

    public class UpdateNoteFieldsParams
    {
        [JsonPropertyName("note")]
        public NoteData Note { get; set; }

        public UpdateNoteFieldsParams()
        {
            Note = new NoteData();
        }

        public class NoteData
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("fields")]
            public Dictionary<string, string> Fields { get; set; }

            public NoteData()
            {
                Fields = new Dictionary<string, string>();
            }
        }
    }

    public class NotesInfoParams
    {
        [JsonPropertyName("notes")]
        public List<long> Notes { get; set; }

        public NotesInfoParams()
        {
            Notes = new List<long>();
        }
    }

    public class NoteInfo
    {
        [JsonPropertyName("noteId")]
        public long NoteId { get; set; }

        [JsonPropertyName("modelName")]
        public string ModelName { get; set; } = "";

        [JsonPropertyName("fields")]
        public Dictionary<string, FieldInfo> Fields { get; set; } = [];

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();
    }

    public class FieldInfo
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = "";

        [JsonPropertyName("order")]
        public int Order { get; set; }
    }

}
