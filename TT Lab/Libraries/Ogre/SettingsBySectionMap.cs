//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace org.ogre {

public class SettingsBySectionMap : global::System.IDisposable 
    , global::System.Collections.Generic.IDictionary<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SettingsBySectionMap(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SettingsBySectionMap obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(SettingsBySectionMap obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~SettingsBySectionMap() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          OgrePINVOKE.delete_SettingsBySectionMap(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }


  public SWIGTYPE_p_std__multimapT_std__string_std__string_t this[string key] {
    get {
      return getitem(key);
    }

    set {
      setitem(key, value);
    }
  }

  public bool TryGetValue(string key, out SWIGTYPE_p_std__multimapT_std__string_std__string_t value) {
    if (this.ContainsKey(key)) {
      value = this[key];
      return true;
    }
    value = default(SWIGTYPE_p_std__multimapT_std__string_std__string_t);
    return false;
  }

  public bool IsEmpty {
    get {
      return empty();
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public global::System.Collections.Generic.ICollection<string> Keys {
    get {
      global::System.Collections.Generic.ICollection<string> keys = new global::System.Collections.Generic.List<string>();
      int size = this.Count;
      if (size > 0) {
        global::System.IntPtr iter = create_iterator_begin();
        for (int i = 0; i < size; i++) {
          keys.Add(get_next_key(iter));
        }
        destroy_iterator(iter);
      }
      return keys;
    }
  }

  public global::System.Collections.Generic.ICollection<SWIGTYPE_p_std__multimapT_std__string_std__string_t> Values {
    get {
      global::System.Collections.Generic.ICollection<SWIGTYPE_p_std__multimapT_std__string_std__string_t> vals = new global::System.Collections.Generic.List<SWIGTYPE_p_std__multimapT_std__string_std__string_t>();
      foreach (global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t> pair in this) {
        vals.Add(pair.Value);
      }
      return vals;
    }
  }

  public void Add(global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t> item) {
    Add(item.Key, item.Value);
  }

  public bool Remove(global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t> item) {
    if (Contains(item)) {
      return Remove(item.Key);
    } else {
      return false;
    }
  }

  public bool Contains(global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t> item) {
    if (this[item.Key] == item.Value) {
      return true;
    } else {
      return false;
    }
  }

  public void CopyTo(global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>[] array) {
    CopyTo(array, 0);
  }

  public void CopyTo(global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>[] array, int arrayIndex) {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (arrayIndex+this.Count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");

    global::System.Collections.Generic.IList<string> keyList = new global::System.Collections.Generic.List<string>(this.Keys);
    for (int i = 0; i < keyList.Count; i++) {
      string currentKey = keyList[i];
      array.SetValue(new global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>(currentKey, this[currentKey]), arrayIndex+i);
    }
  }

  global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>> global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>>.GetEnumerator() {
    return new SettingsBySectionMapEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new SettingsBySectionMapEnumerator(this);
  }

  public SettingsBySectionMapEnumerator GetEnumerator() {
    return new SettingsBySectionMapEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class SettingsBySectionMapEnumerator : global::System.Collections.IEnumerator,
      global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>>
  {
    private SettingsBySectionMap collectionRef;
    private global::System.Collections.Generic.IList<string> keyCollection;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public SettingsBySectionMapEnumerator(SettingsBySectionMap collection) {
      collectionRef = collection;
      keyCollection = new global::System.Collections.Generic.List<string>(collection.Keys);
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t> Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        string currentKey = keyCollection[currentIndex];
        currentObject = new global::System.Collections.Generic.KeyValuePair<string, SWIGTYPE_p_std__multimapT_std__string_std__string_t>(currentKey, collectionRef[currentKey]);
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
      currentIndex = -1;
      currentObject = null;
    }
  }


  public SettingsBySectionMap() : this(OgrePINVOKE.new_SettingsBySectionMap__SWIG_0(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SettingsBySectionMap(SettingsBySectionMap other) : this(OgrePINVOKE.new_SettingsBySectionMap__SWIG_1(SettingsBySectionMap.getCPtr(other)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = OgrePINVOKE.SettingsBySectionMap_size(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private bool empty() {
    bool ret = OgrePINVOKE.SettingsBySectionMap_empty(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Clear() {
    OgrePINVOKE.SettingsBySectionMap_Clear(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  private SWIGTYPE_p_std__multimapT_std__string_std__string_t getitem(string key) {
    SWIGTYPE_p_std__multimapT_std__string_std__string_t ret = new SWIGTYPE_p_std__multimapT_std__string_std__string_t(OgrePINVOKE.SettingsBySectionMap_getitem(swigCPtr, key), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(string key, SWIGTYPE_p_std__multimapT_std__string_std__string_t x) {
    OgrePINVOKE.SettingsBySectionMap_setitem(swigCPtr, key, SWIGTYPE_p_std__multimapT_std__string_std__string_t.getCPtr(x));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool ContainsKey(string key) {
    bool ret = OgrePINVOKE.SettingsBySectionMap_ContainsKey(swigCPtr, key);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Add(string key, SWIGTYPE_p_std__multimapT_std__string_std__string_t value) {
    OgrePINVOKE.SettingsBySectionMap_Add(swigCPtr, key, SWIGTYPE_p_std__multimapT_std__string_std__string_t.getCPtr(value));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Remove(string key) {
    bool ret = OgrePINVOKE.SettingsBySectionMap_Remove(swigCPtr, key);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private global::System.IntPtr create_iterator_begin() {
    global::System.IntPtr ret = OgrePINVOKE.SettingsBySectionMap_create_iterator_begin(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private string get_next_key(global::System.IntPtr swigiterator) {
    string ret = OgrePINVOKE.SettingsBySectionMap_get_next_key(swigCPtr, swigiterator);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void destroy_iterator(global::System.IntPtr swigiterator) {
    OgrePINVOKE.SettingsBySectionMap_destroy_iterator(swigCPtr, swigiterator);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
