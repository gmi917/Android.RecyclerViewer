package md5626862a2f008a84342f64c76df28a29b;


public class AwardHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("travelAppRecyclerViewer.AwardHolder, travelAppRecyclerViewer", AwardHolder.class, __md_methods);
	}


	public AwardHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == AwardHolder.class)
			mono.android.TypeManager.Activate ("travelAppRecyclerViewer.AwardHolder, travelAppRecyclerViewer", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
