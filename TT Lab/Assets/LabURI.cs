using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using Caliburn.Micro;
using TT_Lab.Project;

namespace TT_Lab.Assets
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LabURI : IEquatable<LabURI>, IComparable
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
        public Int32 CompareTo(object? obj)
        {
            var labURI = obj as LabURI;
            if (labURI == null) return 1;

            return _uri.ToString(CultureInfo.InvariantCulture).CompareTo(labURI._uri.ToString(CultureInfo.InvariantCulture));
        }

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

        private String DebuggerDisplay
        {
            get
            {
                if (this == Empty) return "Empty";
                if (IoC.Get<ProjectManager>().OpenedProject == null) return _uri;
                return IoC.Get<ProjectManager>().OpenedProject == null ? _uri : AssetManager.Get().GetAsset(this).Name;
            }
        }
    }
}
