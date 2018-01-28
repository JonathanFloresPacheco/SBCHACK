package md5b2622e17d39aec106da672a13e147f55;


public class BarcodeTracker
	extends com.google.android.gms.vision.Tracker
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNewItem:(ILjava/lang/Object;)V:GetOnNewItem_ILjava_lang_Object_Handler\n" +
			"n_onUpdate:(Lcom/google/android/gms/vision/Detector$Detections;Ljava/lang/Object;)V:GetOnUpdate_Lcom_google_android_gms_vision_Detector_Detections_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("Rb.Forms.Barcode.Droid.BarcodeTracker, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", BarcodeTracker.class, __md_methods);
	}


	public BarcodeTracker ()
	{
		super ();
		if (getClass () == BarcodeTracker.class)
			mono.android.TypeManager.Activate ("Rb.Forms.Barcode.Droid.BarcodeTracker, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onNewItem (int p0, java.lang.Object p1)
	{
		n_onNewItem (p0, p1);
	}

	private native void n_onNewItem (int p0, java.lang.Object p1);


	public void onUpdate (com.google.android.gms.vision.Detector.Detections p0, java.lang.Object p1)
	{
		n_onUpdate (p0, p1);
	}

	private native void n_onUpdate (com.google.android.gms.vision.Detector.Detections p0, java.lang.Object p1);

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
