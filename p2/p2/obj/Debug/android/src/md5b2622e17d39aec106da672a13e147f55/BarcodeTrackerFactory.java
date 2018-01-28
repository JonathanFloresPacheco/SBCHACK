package md5b2622e17d39aec106da672a13e147f55;


public class BarcodeTrackerFactory
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.vision.MultiProcessor.Factory
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_create:(Ljava/lang/Object;)Lcom/google/android/gms/vision/Tracker;:GetCreate_Ljava_lang_Object_Handler:Android.Gms.Vision.MultiProcessor/IFactoryInvoker, Xamarin.GooglePlayServices.Vision\n" +
			"";
		mono.android.Runtime.register ("Rb.Forms.Barcode.Droid.BarcodeTrackerFactory, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", BarcodeTrackerFactory.class, __md_methods);
	}


	public BarcodeTrackerFactory ()
	{
		super ();
		if (getClass () == BarcodeTrackerFactory.class)
			mono.android.TypeManager.Activate ("Rb.Forms.Barcode.Droid.BarcodeTrackerFactory, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public com.google.android.gms.vision.Tracker create (java.lang.Object p0)
	{
		return n_create (p0);
	}

	private native com.google.android.gms.vision.Tracker n_create (java.lang.Object p0);

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
