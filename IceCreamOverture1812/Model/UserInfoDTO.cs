using System;
using System.Collections.Generic;

namespace CocoVendorApp
{
	public class UserInfoDTO
	{
		public string mail
		{
			get;
			set;
		}

		public string password
		{
			get;
			set;
		}

		public string apiKey
		{
			get;
			set;
		}
	}

	public class AvatarDTO
	{
		public int id { get; set; }
		public int image_width { get; set; }
		public string relative_file_url { get; set; }
		public int image_height { get; set; }
		public string human_readable_size { get; set; }
		public string mime_type { get; set; }
		public string file_name_with_extension { get; set; }
		public string slug { get; set; }
		public string original_name { get; set; }
		public string file_name { get; set; }
		public string file_extension { get; set; }
		public string created { get; set; }
		public string updated { get; set; }
	}

	public class UserWebServiceDTO
	{
		public InfoLidoDTO lido { get; set; }
		public IList<BookingDTO> booking_array { get; set;}

		public int id { get; set; }
		public string email { get; set; }
		public string telephone { get; set; }
		public bool from_facebook { get; set; }
		public string api_key { get; set; }
		public bool vendor { get; set; }
		//public InfoLidoDTO lido { get; set; }
		public AvatarDTO avatar { get; set; }
		public string name { get; set; }
		public string surname { get; set; }

		public string NameSurname
		{
			get
			{
				return name + " " + surname;
			}
		}
	}
}
