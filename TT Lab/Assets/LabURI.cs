﻿using Newtonsoft.Json;
using System;

namespace TT_Lab.Assets
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class LabURI
    {
        [JsonProperty(Required = Required.Always)]
        private readonly String _uri;

        public static implicit operator String(LabURI labURI) => labURI._uri;
        public static explicit operator LabURI(String uri) => new(uri);

        [JsonConstructor]
        public LabURI(String uri)
        {
            _uri = uri;
        }

        public override String ToString() => _uri;

        public static LabURI Empty { get => new("res://EMPTY"); }

        public static Boolean operator ==(LabURI? labURI, LabURI? other)
        {
            if (labURI is null && other is null) return true;
            if (labURI is null) return false;
            return labURI.Equals(other);
        }

        public static Boolean operator !=(LabURI? labURI, LabURI? other)
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

        public String GetUri() { return _uri; }

        public override Int32 GetHashCode()
        {
            return _uri.GetHashCode();
        }
    }
}
