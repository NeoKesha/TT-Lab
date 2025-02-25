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

public class LayerInstanceList : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<Terrain.LayerInstance>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal LayerInstanceList(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LayerInstanceList obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(LayerInstanceList obj) {
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

  ~LayerInstanceList() {
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
          OgreTerrainPINVOKE.delete_LayerInstanceList(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public LayerInstanceList(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (Terrain.LayerInstance element in c) {
      this.Add(element);
    }
  }

  public LayerInstanceList(global::System.Collections.Generic.IEnumerable<Terrain.LayerInstance> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (Terrain.LayerInstance element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public Terrain.LayerInstance this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < 0 || (uint)value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
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

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(Terrain.LayerInstance[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(Terrain.LayerInstance[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, Terrain.LayerInstance[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public Terrain.LayerInstance[] ToArray() {
    Terrain.LayerInstance[] array = new Terrain.LayerInstance[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<Terrain.LayerInstance> global::System.Collections.Generic.IEnumerable<Terrain.LayerInstance>.GetEnumerator() {
    return new LayerInstanceListEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new LayerInstanceListEnumerator(this);
  }

  public LayerInstanceListEnumerator GetEnumerator() {
    return new LayerInstanceListEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class LayerInstanceListEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<Terrain.LayerInstance>
  {
    private LayerInstanceList collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public LayerInstanceListEnumerator(LayerInstanceList collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public Terrain.LayerInstance Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (Terrain.LayerInstance)currentObject;
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
        currentObject = collectionRef[currentIndex];
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

  public LayerInstanceList() : this(OgreTerrainPINVOKE.new_LayerInstanceList__SWIG_0(), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public LayerInstanceList(LayerInstanceList other) : this(OgreTerrainPINVOKE.new_LayerInstanceList__SWIG_1(LayerInstanceList.getCPtr(other)), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Clear() {
    OgreTerrainPINVOKE.LayerInstanceList_Clear(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Add(Terrain.LayerInstance x) {
    OgreTerrainPINVOKE.LayerInstanceList_Add(swigCPtr, Terrain.LayerInstance.getCPtr(x));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = OgreTerrainPINVOKE.LayerInstanceList_size(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private bool empty() {
    bool ret = OgreTerrainPINVOKE.LayerInstanceList_empty(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint capacity() {
    uint ret = OgreTerrainPINVOKE.LayerInstanceList_capacity(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void reserve(uint n) {
    OgreTerrainPINVOKE.LayerInstanceList_reserve(swigCPtr, n);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public LayerInstanceList(int capacity) : this(OgreTerrainPINVOKE.new_LayerInstanceList__SWIG_2(capacity), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  private Terrain.LayerInstance getitemcopy(int index) {
    Terrain.LayerInstance ret = new Terrain.LayerInstance(OgreTerrainPINVOKE.LayerInstanceList_getitemcopy(swigCPtr, index), true);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private Terrain.LayerInstance getitem(int index) {
    Terrain.LayerInstance ret = new Terrain.LayerInstance(OgreTerrainPINVOKE.LayerInstanceList_getitem(swigCPtr, index), false);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, Terrain.LayerInstance val) {
    OgreTerrainPINVOKE.LayerInstanceList_setitem(swigCPtr, index, Terrain.LayerInstance.getCPtr(val));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(LayerInstanceList values) {
    OgreTerrainPINVOKE.LayerInstanceList_AddRange(swigCPtr, LayerInstanceList.getCPtr(values));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public LayerInstanceList GetRange(int index, int count) {
    global::System.IntPtr cPtr = OgreTerrainPINVOKE.LayerInstanceList_GetRange(swigCPtr, index, count);
    LayerInstanceList ret = (cPtr == global::System.IntPtr.Zero) ? null : new LayerInstanceList(cPtr, true);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, Terrain.LayerInstance x) {
    OgreTerrainPINVOKE.LayerInstanceList_Insert(swigCPtr, index, Terrain.LayerInstance.getCPtr(x));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, LayerInstanceList values) {
    OgreTerrainPINVOKE.LayerInstanceList_InsertRange(swigCPtr, index, LayerInstanceList.getCPtr(values));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    OgreTerrainPINVOKE.LayerInstanceList_RemoveAt(swigCPtr, index);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    OgreTerrainPINVOKE.LayerInstanceList_RemoveRange(swigCPtr, index, count);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public static LayerInstanceList Repeat(Terrain.LayerInstance value, int count) {
    global::System.IntPtr cPtr = OgreTerrainPINVOKE.LayerInstanceList_Repeat(Terrain.LayerInstance.getCPtr(value), count);
    LayerInstanceList ret = (cPtr == global::System.IntPtr.Zero) ? null : new LayerInstanceList(cPtr, true);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    OgreTerrainPINVOKE.LayerInstanceList_Reverse__SWIG_0(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Reverse(int index, int count) {
    OgreTerrainPINVOKE.LayerInstanceList_Reverse__SWIG_1(swigCPtr, index, count);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, LayerInstanceList values) {
    OgreTerrainPINVOKE.LayerInstanceList_SetRange(swigCPtr, index, LayerInstanceList.getCPtr(values));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
