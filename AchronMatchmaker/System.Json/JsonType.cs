namespace System.Json
{
#if XAMARIN_AUTH_INTERNAL
    internal enum JsonType
#else
    public enum JsonType
#endif 	
	{
		String,
		Number,
		Object,
		Array,
		Boolean,
	}
}