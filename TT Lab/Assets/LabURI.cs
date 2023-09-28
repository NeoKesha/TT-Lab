using Newtonsoft.Json;
using System;
using System.Security.Policy;

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

        public static Boolean operator==(LabURI? labURI, LabURI? other)
        {
            if (labURI is null && other is null) return true;
            if (labURI is null) return false;
            if (other is null) return false;
            return labURI.Equals(other);
        }

        public static Boolean operator!=(LabURI? labURI, LabURI? other)
        {
            return !(labURI == other);
        }

        public override Boolean Equals(Object? obj)
        {
            return Equals(obj as LabURI);
        }

        public Boolean Equals(LabURI? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return other._uri == _uri;
        }

        public string GetUri() { return _uri; }

        public override int GetHashCode()
        {
            return _uri.GetHashCode();
        }
    }
}
