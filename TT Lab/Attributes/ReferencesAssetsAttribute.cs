using System;

namespace TT_Lab.Attributes;

/// <summary>
/// Classes marked by this attribute will be searched if they reference any assets in the project
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ReferencesAssetsAttribute : Attribute
{
}