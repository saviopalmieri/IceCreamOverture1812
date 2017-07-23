using System;
using System.Linq;

namespace CocoVendorApp
{
	public static class ConnectionHelper
	{
		public static string AppUrl
		{
			get
			{
				#if DEBUG
					return "https://cocoapp.it/api/v2/web/app_dev.php/";
				#else
					return "https://cocoapp.it/api/v2/web/";
				#endif
			}
		}

		public static string AppRealUrl
		{ 
			get
			{
				return "https://cocoapp.it/api/v2/web/";
			}
		}

		public static string AppName
		{
			get
			{
				return "CocoAppVendor1";
			}
		}

		public static string AppNameOld
		{
			get
			{
				return "CocoAppVendor";
			}
		}

		public static string ConnectionErrorString
		{
			get
			{
				return "Stiamo riscontrando problemi con la tua connessione Internet.\\nControlla la tua connessione e riprova.";
			}
		}

		

		public enum WebServiceCallType
		{
			Post,
			Put,
			Get,
			Delete
		}
	}
}
