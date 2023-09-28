using System;
using System.Collections.Generic;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public class Package : Folder
    {
        public Boolean Enabled { get; set; }
        public List<LabURI> Dependencies { get; private set; } = new();

        public Package() : base()
        {
            Enabled = true;
        }

        public Package(String name, Folder? parent = null) : base((LabURI)$"res://{name}", name, null, parent)
        {
            Enabled = true;
        }

        public void AddDependency(LabURI uri)
        {
            Dependencies.Add(uri);
        }

        public void RemoveDependency(LabURI uri)
        {
            Dependencies.Remove(uri);
        }

        public override AbstractAssetData GetData()
        {
            return base.GetData();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }
    }
}
