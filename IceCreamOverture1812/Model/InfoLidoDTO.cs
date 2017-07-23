using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CocoVendorApp
{
	public class InfoFilaDTO
	{ 
		public string NomeFila
		{
			get;
			set;
		}

		public int id { get; set; }
		public int name { get; set; }

		public int IdFila
		{
			get 
			{
				if (name > 0)
				{
					return name;
				}
				else if (NomeFila.ToUpper() == "ZONA UNICA")
				{
					return 1;
				}
				else
				{
					return int.Parse(NomeFila.Split(' ')[1]);
				}
			}
		}

		public int umbrella_qty { get; set; }
		public int sun_bed_qty { get; set; }
		public int chair_qty { get; set; }
		public int max_umbrella_qty { get; set; }
		public int max_sun_bed_qty { get; set; }
		public int max_chair_qty { get; set; }
        public InfoLidoDTO lido { get; set; }
		public IList<int> ListUmbrellaAvailability
		{
			get
			{
				var result = new List<int>();
				for (int i = 0; i <= max_umbrella_qty; i++)
				{
					result.Add(i);
				}
				return result;
			}
		}

		public IList<int> ListSunBedAvailability
		{
			get
			{
				var result = new List<int>();
				for (int i = 0; i <= max_sun_bed_qty; i++)
				{
					result.Add(i);
				}
				return result;
			}
		}

		public IList<int> ListChairAvailability
		{
			get
			{
				var result = new List<int>();
				for (int i = 0; i <= max_chair_qty; i++)
				{
					result.Add(i);
				}
				return result;
			}
		}
	}

	public class ServicesItem
	{
		public string ImageUrl { get; set; }
		public string name { get; set; }
		public bool Active { get; set; }
		public int id { get; set; }
		public string IdServizio
		{
			get 
			{
				if (id > 0)
				{
					return id.ToString();
				}
				else
				{
					var _name = ImageUrl.Split('.')[0];
					return _name.Replace("servizio", string.Empty);
				}
			}
		}
	}

	public class InfoLidoWebServiceDTO
	{ 
		public InfoLidoWebServiceDTO() {}

		public string nomelido { get; set; }
		public string indirizzolido { get; set; }
		public string cittalido { get; set; }
		public string telefono { get; set; }
		public string email_paypal { get; set; }
		public string prezzo_lettini { get; set; }
		public string prezzo_ombrelloni { get; set; }
		public string prezzo_cabine { get; set; }
		public string prezzo_sdraio { get; set; }
		public int numero_recensioni { get; set; }
		public string media_recensioni { get; set; }
		public IList<int> nome_servizio { get; set; }
		public string data_apertura { get; set; }
		public string data_chiusura { get; set; }
		public string url_immagine { get; set; }
	}

	public class InfoLidoDTO
	{
		public InfoLidoDTO()
		{
		}

		public InfoLidoDTO(InfoLidoWebServiceDTO infoLidoWebSevice)
		{
			name = infoLidoWebSevice.nomelido;
			address = infoLidoWebSevice.indirizzolido;
			city = infoLidoWebSevice.cittalido;
			telephone = infoLidoWebSevice.telefono;
			email_paypal = infoLidoWebSevice.email_paypal;
			sun_bed_price = decimal.Parse(infoLidoWebSevice.prezzo_lettini);
			chair_price = decimal.Parse(infoLidoWebSevice.prezzo_sdraio);
			umbrella_price = decimal.Parse(infoLidoWebSevice.prezzo_ombrelloni);
			cabana_price = decimal.Parse(infoLidoWebSevice.prezzo_cabine);
			open_season_date = DateTime.ParseExact(infoLidoWebSevice.data_apertura, "yyyy-MM-dd", null);
			close_season_date = DateTime.ParseExact(infoLidoWebSevice.data_chiusura, "yyyy-MM-dd", null);
			//ImgLidoPath = infoLidoWebSevice.url_immagine;
			lido_service_array = new List<ServicesItem>();
			if (infoLidoWebSevice.nome_servizio != null)
			{
				foreach (var s in infoLidoWebSevice.nome_servizio)
				{
					lido_service_array.Add(new ServicesItem
					{
						ImageUrl = "servizio" + s.ToString() + ".png",
						Active = true
					});
				}	
			}
		}

		public double review_rating_avg { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		private DateTime _open_season_date;
		public DateTime open_season_date
		{
			get
			{
				return _open_season_date.AddHours(-7);
			}
			set
			{
				_open_season_date = value;
			}
		}
		private DateTime _close_season_date;
		public DateTime close_season_date
		{
			get
			{
				return _close_season_date.AddHours(-7);
			}
			set
			{
				_close_season_date = value;
			}
		}
		public UserWebServiceDTO user { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string lng { get; set; }
		public string lat { get; set; }
		public IList<ServicesItem> lido_service_array { get; set; }
		public IList<InfoFilaDTO> lido_zone_array { get; set; }
		public IList<RecensioneDTO> review_array { get; set; }
		public IList<BookingDTO> booking_array { get; set;}

		public int cabana_qty { get; set; }
		public string cabana_note { get; set; }

		public decimal umbrella_price { get; set; }
		public decimal sun_bed_price { get; set; }
		public decimal chair_price { get; set; }
		public decimal cabana_price { get; set; }

		public string email_paypal { get; set; }
		public string user_name { get; set; }
		public string user_surname { get; set; }

		public string user_name_surname
		{
			get
			{
				if (this.user != null)
				{
					return user.name + " " + user.surname;
				}
				else
				{
					return user_name + " " + user_surname;
				}
			}
		}

		public string telephone { get; set; }
		public Stream ImgLidoStream { get; set; }
		public string ImgLidoPath
		{
			get
			{
				if (image != null)
				{
					return ConnectionHelper.AppRealUrl + image.relative_file_url;	
				}
				else
				{
					return string.Empty;
				}
			}
		}

		public ImageDTO image { get; set; }
		public IList<ImageDTO> image_gallery_array { get; set; }
		public int min_day_forewarning { get; set; }
	}

	public class LidoZoneAvailabilityDTO
	{
		public InfoFilaDTO lido_zone { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public int sun_bed_availability { get; set; }
		public int umbrella_availability { get; set; }
		public int chair_availability { get; set; }
	}

	public class DisponibilitaDTO
	{
		public string start_date { get; set; }
		public string end_date { get; set; }
		public int cabana_availability { get; set; }
		public IList<LidoZoneAvailabilityDTO> lido_zone_availability_array { get; set; }
	}

	public class PrenotazioneDTO
	{ 
		public int id { get; set; }
		public DateTime start_date { get; set; }
		public DateTime end_date { get; set; }
		public object user { get; set; }
		public InfoFilaDTO lido_zone { get; set; }
		public int umbrella_qty { get; set; }
		public int sun_bed_qty { get; set; }
		public int cabana_qty { get; set; }
		public int chair_qty { get; set; }
		public bool fake_booking { get; set; }
	}

	public class RecensioneDTO
	{ 
		public int id { get; set; }
		public int rating { get; set; }
		public string title { get; set; }
		public string note { get; set; }
        public UserWebServiceDTO user { get; set; }
		public string created { get; set; }
		public string updated { get; set; }

		public string date_created
		{
			get
			{
				try
				{
					return DateTime.ParseExact(created.Split(' ')[0], "yyyy-MM-dd", null).ToString("d");
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
		}

		public string nome_utente
		{
			get
			{
				if (user == null)
				{
					return string.Empty;
				}
				else
				{
					return user.name + " " + user.surname;
				}
			}
		}

		public string cognome_utente
		{
			get
			{
				if (user == null)
				{
					return string.Empty;
				}
				else
				{
					return user.surname;
				}
			}
		}

		public string img_utente
		{
			get
			{
				if (user == null || user.avatar == null)
				{
					return string.Empty;
				}
				else
				{
					return ConnectionHelper.AppRealUrl + user.avatar.relative_file_url;
				}
			}
		}
	}

	public class BookingDTO
	{ 
		public int id { get; set; }
		private DateTime _start_date;
		public DateTime start_date
		{
			get
			{
				return _start_date;
			}
			set
			{
				_start_date = value;
			}
		}
		private DateTime _end_date;
		public DateTime end_date
		{
			get
			{
				return _end_date;
			}
			set
			{
				_end_date = value;
			}
		}
		public UserWebServiceDTO user { get; set; }
		public InfoFilaDTO lido_zone { get; set; }
		public int umbrella_qty { get; set; }
		public int sun_bed_qty { get; set; }
		public int cabana_qty { get; set; }
		public int chair_qty { get; set; }
		public bool fake_booking { get; set; }
        public ReceiptDTO receipt { get; set; }
		public string created { get; set; }
		public object updated { get; set; }

		public string nome_utente
		{
			get
			{
				if (user == null)
				{
					return string.Empty;
				}
				else
				{
					try
					{
						return user.name + " " + user.surname.Substring(0, 1) + ".";
					}
					catch (Exception ex)
					{
						return string.Empty;
					}
				}
			}
		}

		public string LidoName
		{
			get
			{
                if (lido_zone.lido != null)
                {
                    return lido_zone.lido.name;   
                }
                else
                {
                    return string.Empty;
                }
            }
		}

		public string LidoFullAddress
		{
			get
			{
				if (lido_zone.lido != null)
				{
					return lido_zone.lido.address + " - " + lido_zone.lido.city;
				}
				else
				{
					return string.Empty;
				}
			}
		}

		public DateTime ReferenceDate
		{
			get
			{
				return start_date.Date;
			}
		}

		public string ReferenceDateString
		{
			get
			{
				return start_date.ToString("dd MMMM yyyy");
			}
		}

		public string NReview
		{
			get
			{
				if (lido_zone.lido.review_array != null)
				{
					return lido_zone.lido.review_array.Count.ToString() + " recensioni";
				}
				else
				{
					return "0 recensioni";
				}
			}
		}

		public string PaymentStatusDescription
		{
			get
			{
				if (receipt != null && receipt.PaymentStatusDecoded == PaymentStatus.PAYMENT_SUCCESS)
				{
					return "PAGATO";
				}
				else
				{
					return "NON PAGATO";
				}
			}
		}

		public string PaymentStatusBackColor
		{
			get
			{
				if (receipt != null && receipt.PaymentStatusDecoded == PaymentStatus.PAYMENT_SUCCESS)
				{
					return "#3d83f5";
				}
				else
				{
					return "#FF5B37";
				}
			}
		}

		public string PaymentStatusImage
		{
			get
			{
				if (receipt != null && receipt.PaymentStatusDecoded == PaymentStatus.PAYMENT_SUCCESS)
				{
					return "paymentsuccess.png";
				}
				else
				{
					return "paymentfailed.png";
				}
			}
		}
	}

	public class ReceiptDTO
	{
		public double total { get; set; }
		public int? id { get; set; }
		public BookingDTO booking { get; set; }
		public double product_total { get; set; }
		public double commission_total { get; set; }
		public string coupon_code { get; set; }
		public string coupon_string { get; set; }
		public string payment_status { get; set; }

		public PaymentStatus PaymentStatusDecoded
		{
			get
			{
				switch (payment_status)
				{
					case "ERROR_NO_PAYMENT_INSTRUCTION":
						return PaymentStatus.ERROR_NO_PAYMENT_INSTRUCTION;
					case "PAYMENT_ERROR":
						return PaymentStatus.PAYMENT_ERROR;
					case "PAYMENT_SUCCESS":
						return PaymentStatus.PAYMENT_SUCCESS;
					case "PENDING_PAYMENT":
						return PaymentStatus.PENDING_PAYMENT;
					case "ERROR_NO_RECEIPT":
						return PaymentStatus.ERROR_NO_RECEIPT;
					default:
						return PaymentStatus.PENDING_PAYMENT;
				}
			}
		}
	}

	public enum PaymentStatus
	{
		ERROR_NO_PAYMENT_INSTRUCTION,
		PAYMENT_ERROR,
		PAYMENT_SUCCESS,
		PENDING_PAYMENT,
		ERROR_NO_RECEIPT
	}

	public enum ImageSize
	{
		SMALL = 150,
		MEDIUM = 800,
		LARGE = 1500	
	}

	public class ImageDTO
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
		public object title { get; set; }
		public object alt { get; set; }
		public Dictionary<string, string> relative_src_set { get; set; }

		public string GetImageUrlBySize(ImageSize imageSize)
		{
			var result = string.Empty;

			if (relative_src_set != null && relative_src_set.Count > 1)
			{
				var minDiff = (from x in relative_src_set select Math.Abs((decimal)(((int)imageSize) - int.Parse(x.Key)))).Min();

				result = (from x in relative_src_set where Math.Abs((decimal)(((int)imageSize) - int.Parse(x.Key))) == minDiff select x.Value).FirstOrDefault();
			}
			else
			{
				if (!string.IsNullOrEmpty(relative_file_url))
				{
					result = relative_file_url;
				}
			}

			return result;
		}

		public string ImgViewUrl(ImageSize imageSize)
		{
			var result = "gallery.jpg";

			result = GetImageUrlBySize(imageSize);

			if (!string.IsNullOrEmpty(result))
			{
				result = ConnectionHelper.AppRealUrl + result;
			}
			else
			{
				result = "gallery.jpg";
			}

			return result;
		}
	}
}
