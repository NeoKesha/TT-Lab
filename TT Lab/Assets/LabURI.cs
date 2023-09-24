using Newtonsoft.Json;
using System;

namespace TT_Lab.Assets
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class LabURI
    {
        [JsonProperty(Required = Required.Always)]
        private readonly string _uri;

        public static implicit operator String(LabURI labURI) => labURI._uri;
        public static explicit operator LabURI(string uri) => new(uri);

        public LabURI(string uri)
        {
            _uri = uri;
        }

        public override string ToString() => _uri;

        public static LabURI Empty { get; } = new LabURI("res://EMPTY");

        public string GetUri() { return _uri; }
    }
}
