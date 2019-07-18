package md5626862a2f008a84342f64c76df28a29b;


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
		mono.android.Runtime.register ("travelAppRecyclerViewer.SubPrizeHolder, travelAppRecyclerViewer", SubPrizeHolder.class, __md_methods);
	}


	public SubPrizeHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == SubPrizeHolder.class)
			mono.android.TypeManager.Activate ("travelAppRecyclerViewer.SubPrizeHolder, travelAppRecyclerViewer", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
