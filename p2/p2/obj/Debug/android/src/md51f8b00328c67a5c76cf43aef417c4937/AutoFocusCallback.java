package md51f8b00328c67a5c76cf43aef417c4937;


public class AutoFocusCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.Camera.AutoFocusCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAutoFocus:(ZLandroid/hardware/Camera;)V:GetOnAutoFocus_ZLandroid_hardware_Camera_Handler:Android.Hardware.Camera/IAutoFocusCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Rb.Forms.Barcode.Droid.View.AutoFocusCallback, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", AutoFocusCallback.class, __md_methods);
	}


	public AutoFocusCallback ()
	{
		super ();
		if (getClass () == AutoFocusCallback.class)
			mono.android.TypeManager.Activate ("Rb.Forms.Barcode.Droid.View.AutoFocusCallback, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public AutoFocusCallback (android.hardware.Camera p0)
	{
		super ();
		if (getClass () == AutoFocusCallback.class)
			mono.android.TypeManager.Activate ("Rb.Forms.Barcode.Droid.View.AutoFocusCallback, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Hardware.Camera, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onAutoFocus (boolean p0, android.hardware.Camera p1)
	{
		n_onAutoFocus (p0, p1);
	}

	private native void n_onAutoFocus (boolean p0, android.hardware.Camera p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
