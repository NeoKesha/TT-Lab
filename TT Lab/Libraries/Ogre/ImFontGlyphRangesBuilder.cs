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

public class ImFontGlyphRangesBuilder : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImFontGlyphRangesBuilder(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImFontGlyphRangesBuilder obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImFontGlyphRangesBuilder obj) {
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

  ~ImFontGlyphRangesBuilder() {
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
          ImGuiPINVOKE.delete_ImFontGlyphRangesBuilder(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_ImVectorT_unsigned_int_t UsedChars {
    set {
      ImGuiPINVOKE.ImFontGlyphRangesBuilder_UsedChars_set(swigCPtr, SWIGTYPE_p_ImVectorT_unsigned_int_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImFontGlyphRangesBuilder_UsedChars_get(swigCPtr);
      SWIGTYPE_p_ImVectorT_unsigned_int_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_ImVectorT_unsigned_int_t(cPtr, false);
      return ret;
    } 
  }

  public ImFontGlyphRangesBuilder() : this(ImGuiPINVOKE.new_ImFontGlyphRangesBuilder(), true) {
  }

  public void Clear() {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_Clear(swigCPtr);
  }

  public bool GetBit(uint n) {
    bool ret = ImGuiPINVOKE.ImFontGlyphRangesBuilder_GetBit(swigCPtr, n);
    return ret;
  }

  public void SetBit(uint n) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_SetBit(swigCPtr, n);
  }

  public void AddChar(ushort c) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_AddChar(swigCPtr, c);
  }

  public void AddText(string text, string text_end) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_AddText__SWIG_0(swigCPtr, text, text_end);
  }

  public void AddText(string text) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_AddText__SWIG_1(swigCPtr, text);
  }

  public void AddRanges(SWIGTYPE_p_unsigned_short ranges) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_AddRanges(swigCPtr, SWIGTYPE_p_unsigned_short.getCPtr(ranges));
  }

  public void BuildRanges(SWIGTYPE_p_ImVectorT_unsigned_short_t out_ranges) {
    ImGuiPINVOKE.ImFontGlyphRangesBuilder_BuildRanges(swigCPtr, SWIGTYPE_p_ImVectorT_unsigned_short_t.getCPtr(out_ranges));
  }

}

}
