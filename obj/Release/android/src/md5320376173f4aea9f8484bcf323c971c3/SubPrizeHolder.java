package md5320376173f4aea9f8484bcf323c971c3;


public class SubPrizeHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("travelAppRecyclerViewer.SubPrizeHolder, travelAppRecyclerViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SubPrizeHolder.class, __md_methods);
	}


	public SubPrizeHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == SubPrizeHolder.class)
			mono.android.TypeManager.Activate ("travelAppRecyclerViewer.SubPrizeHolder, travelAppRecyclerViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

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
