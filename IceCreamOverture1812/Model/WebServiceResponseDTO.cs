using System;
namespace CocoVendorApp
{
	public class WebServiceResponseJsonDTO
	{ 
		public WebServiceResponseDTO<object> WebServiceResponseDTO
		{
			get;
			set;
		}
	}

	public class WebServiceResponseDTO<T>
	{
		public bool error
		{
			get;
			set;
		}

		public string message
		{
			get;
			set;
		}

		public string apiKey
		{
			get;
			set;
		}

		public string email
		{
			get;
			set;
		}

		public string nome_lido
		{
			get;
			set;
		}

		public int id_utente
		{
			get;
			set;
		}

		public T data { get; set; }

		public WebServiceResponseDTO()
		{
		}
	}
}
