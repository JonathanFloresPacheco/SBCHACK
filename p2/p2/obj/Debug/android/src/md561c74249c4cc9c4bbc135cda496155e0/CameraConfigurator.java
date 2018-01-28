package md561c74249c4cc9c4bbc135cda496155e0;


public class CameraConfigurator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.rebuy.play.services.vision.CameraSource.ConfigurationCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_configure:(Landroid/hardware/Camera;)Landroid/hardware/Camera;:GetConfigure_Landroid_hardware_Camera_Handler:Com.Rebuy.Play.Services.Vision.CameraSource/IConfigurationCallbackInvoker, CameraSourceBindings\n" +
			"";
		mono.android.Runtime.register ("Rb.Forms.Barcode.Droid.Camera.CameraConfigurator, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", CameraConfigurator.class, __md_methods);
	}


	public CameraConfigurator ()
	{
		super ();
		if (getClass () == CameraConfigurator.class)
			mono.android.TypeManager.Activate ("Rb.Forms.Barcode.Droid.Camera.CameraConfigurator, Rb.Forms.Barcode.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.hardware.Camera configure (android.hardware.Camera p0)
	{
		return n_configure (p0);
	}

	private native android.hardware.Camera n_configure (android.hardware.Camera p0);

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
