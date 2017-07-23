using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;

namespace CocoVendorApp
{
	public class InfoLidoDAO
	{
		private static InfoLidoDAO mInstance;

		public static InfoLidoDAO Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new InfoLidoDAO();
				}
				return mInstance;
			}
		}

		public InfoLidoDAO()
		{
		}

		private Task<HttpResponseMessage> ExecuteRequest(ConnectionHelper.WebServiceCallType calltype, IList<KeyValuePair<string, string>> paramList, string url, string apikey = "")
		{
			var uri = new Uri(ConnectionHelper.AppUrl + url);

			var client = new HttpClient();

			if (!string.IsNullOrEmpty(apikey))
			{
				//var byteArray = Encoding.UTF8.GetBytes(apikey);
				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(" ", apikey);
				client.DefaultRequestHeaders.Add("Autenticate", apikey);
			}

			if (calltype == ConnectionHelper.WebServiceCallType.Get)
			{
				return client.GetAsync(uri);
			}
			else
			{
				var content = new System.Net.Http.FormUrlEncodedContent(paramList);
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

				//var byteArray = Encoding.UTF8.GetBytes(apikey);
				//content.Headers.Add("Authorization", Convert.ToBase64String(byteArray));

				if (calltype == ConnectionHelper.WebServiceCallType.Post)
				{
					return client.PostAsync(uri, content);
				}
				else if (calltype == ConnectionHelper.WebServiceCallType.Put)
				{
					return client.PutAsync(uri, content);
				}
				else
				{
					client.BaseAddress = new Uri("");
					return client.DeleteAsync(url);
				}
			}
		}

		private Task<HttpResponseMessage> ExecuteRequestImage(Stream filestream, string url, string apikey = "")
		{
			var uri = new Uri(ConnectionHelper.AppUrl + url);

			var client = new HttpClient();

			var content = new MultipartFormDataContent("UPLOADAPP");

			var bytes = new byte[filestream.Length];
			using (var memoryStream = new MemoryStream())
			{
				filestream.CopyTo(memoryStream);
				bytes = memoryStream.ToArray();
			}

			var fileContent = new ByteArrayContent(bytes);

			//var imageContent = new ByteArrayContent(bytes);
			//imageContent.Headers.ContentType = 
			//        MediaTypeHeaderValue.Parse("image/jpeg");

			//   content.Add(imageContent, "image", "image.jpg");

			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse ("application/octet-stream");
			fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = "file",
				FileName = "my_uploaded_image.jpg"
			};

			//string boundary = "---8d0f01e6b3b5dafaaadaad";
			//MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
			//multipartContent.Add (fileContent);

			//content.Add(new StreamContent(filestream), "file");

			if (!string.IsNullOrEmpty(apikey))
			{
				//var byteArray = Encoding.UTF8.GetBytes(apikey);
				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(" ", apikey);
				client.DefaultRequestHeaders.Add("Autenticate", apikey);
			}

			content.Add(fileContent, "file");

			//var byteArray = Encoding.UTF8.GetBytes(apikey);
			//content.Headers.Add("Authorization", Convert.ToBase64String(byteArray));

			return client.PostAsync(uri, content);
		}

		private Task<HttpResponseMessage> ExecuteDelete(string url, string apikey = "")
		{
			var client = new HttpClient();

			var content = new System.Net.Http.FormUrlEncodedContent(null);
			content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

			if (!string.IsNullOrEmpty(apikey))
			{
				//var byteArray = Encoding.UTF8.GetBytes(apikey);
				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(" ", apikey);
				client.DefaultRequestHeaders.Add("Autenticate", apikey);
			}

			client.BaseAddress = new Uri(ConnectionHelper.AppUrl);
			return client.DeleteAsync(url);
		}

		private Task<HttpResponseMessage> ExecuteRequestJson(ConnectionHelper.WebServiceCallType calltype, string jsonval, string url, string apikey = "")
		{
			var uri = new Uri(ConnectionHelper.AppUrl + url);

			var client = new HttpClient();

			if (!string.IsNullOrEmpty(apikey))
			{
				//var byteArray = Encoding.UTF8.GetBytes(apikey);
				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(" ", apikey);
				client.DefaultRequestHeaders.Add("Autenticate", apikey);
			}

			if (calltype == ConnectionHelper.WebServiceCallType.Post)
			{
				return client.PostAsync(uri, new StringContent(jsonval, Encoding.UTF8, "application/json"));
			}
			else if (calltype == ConnectionHelper.WebServiceCallType.Put)
			{
				return client.PutAsync(uri, new StringContent(jsonval, Encoding.UTF8, "application/json"));
			}
			else
			{
				return client.PutAsync(uri, new StringContent(jsonval, Encoding.UTF8, "application/json"));
			}
		}

        public WebServiceResponseDTO<InfoLidoDTO> SetupCompletoLido(string apiKey, string email, InfoLidoDTO mInfoLido)
        {
            string url = "vendor/lido-complete-setup";

            //long idLido = GetIdUtenteLido(apiKey);

            //var listParam = new List<KeyValuePair<string, string>>();
            //listParam.Add(new KeyValuePair<string, string>("name", mInfoLido.name));
            //listParam.Add(new KeyValuePair<string, string>("telephone", mInfoLido.telephone));
            ////listParam.Add(new KeyValuePair<string, string>("nomelido", mInfoLido.nomelido));
            //listParam.Add(new KeyValuePair<string, string>("address", mInfoLido.address));
            //listParam.Add(new KeyValuePair<string, string>("city", mInfoLido.city));
            //listParam.Add(new KeyValuePair<string, string>("lat", mInfoLido.lat));
            //listParam.Add(new KeyValuePair<string, string>("lng", mInfoLido.lng));
            //listParam.Add(new KeyValuePair<string, string>("open_season_date", mInfoLido.open_season_date.ToString("yyyyMMdd")));
            //listParam.Add(new KeyValuePair<string, string>("close_season_date", mInfoLido.close_season_date.ToString("yyyyMMdd")));
            //listParam.Add(new KeyValuePair<string, string>("cabana_qty", mInfoLido.cabana_qty.ToString()));
            //listParam.Add(new KeyValuePair<string, string>("lido_zone_array", JsonConvert.SerializeObject((from x in mInfoLido.lido_zone_array
            //																							   select new
            //																							   {
            //																								   name = x.IdFila,
            //																								   x.umbrella_qty,
            //																								   x.sun_bed_qty,
            //																								   x.chair_qty
            //																							   }).ToList()
            //	                                                                                         )));
            //listParam.Add(new KeyValuePair<string, string>("cabana_price", mInfoLido.cabana_price.ToString()));
            //listParam.Add(new KeyValuePair<string, string>("sun_bed_price", mInfoLido.sun_bed_price.ToString()));
            //listParam.Add(new KeyValuePair<string, string>("umbrella_price", mInfoLido.umbrella_price.ToString()));
            //listParam.Add(new KeyValuePair<string, string>("chair_price", mInfoLido.chair_price.ToString()));
            //listParam.Add(new KeyValuePair<string, string>("email_paypal", mInfoLido.email_paypal));
            //listParam.Add(new KeyValuePair<string, string>("lido_service_array", JsonConvert.SerializeObject((from x in mInfoLido.lido_service_array select int.Parse(x.IdServizio)).ToList())));

            var jsonToRequest = JsonConvert.SerializeObject(new
            {
                mInfoLido.name,
                mInfoLido.telephone,
                mInfoLido.address,
                mInfoLido.city,
                mInfoLido.lat,
                mInfoLido.lng,
                open_season_date = mInfoLido.open_season_date.ToString("yyyyMMdd"),
                close_season_date = mInfoLido.close_season_date.ToString("yyyyMMdd"),
                mInfoLido.cabana_qty,
                mInfoLido.cabana_note,
                lido_zone_array = (from x in mInfoLido.lido_zone_array
                                   select new
                                   {
                                       name = x.IdFila,
                                       x.umbrella_qty,
                                       x.sun_bed_qty,
                                       x.chair_qty
                                   }).ToList(),
                mInfoLido.cabana_price,
                mInfoLido.sun_bed_price,
                mInfoLido.umbrella_price,
                mInfoLido.chair_price,
                mInfoLido.email_paypal,
                lido_service_array = (from x in mInfoLido.lido_service_array select int.Parse(x.IdServizio)).ToList(),
                mInfoLido.user_name,
                mInfoLido.user_surname
            });

            //listParam.Add(new KeyValuePair<string, string>("idutente", idLido.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				//response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apiKey);
				response = ExecuteRequestJson(ConnectionHelper.WebServiceCallType.Post, jsonToRequest, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<InfoLidoDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
        }

		public WebServiceResponseDTO<UserWebServiceDTO> SetInfoLido(string apiKey, string email, InfoLidoDTO mInfoLido)
		{
			string url = "vendor/lido/info";

			//long idLido = GetIdUtenteLido(apiKey);

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("name", mInfoLido.name));
			listParam.Add(new KeyValuePair<string, string>("telephone", mInfoLido.telephone));
			//listParam.Add(new KeyValuePair<string, string>("nomelido", mInfoLido.nomelido));
			listParam.Add(new KeyValuePair<string, string>("address", mInfoLido.address));
			listParam.Add(new KeyValuePair<string, string>("city", mInfoLido.city));
			listParam.Add(new KeyValuePair<string, string>("lat", mInfoLido.lat));
			listParam.Add(new KeyValuePair<string, string>("lng", mInfoLido.lng));
			listParam.Add(new KeyValuePair<string, string>("min_day_forewarning", mInfoLido.min_day_forewarning.ToString()));
			listParam.Add(new KeyValuePair<string, string>("open_season_date", mInfoLido.open_season_date.ToString("yyyyMMdd")));
			listParam.Add(new KeyValuePair<string, string>("close_season_date", mInfoLido.close_season_date.ToString("yyyyMMdd")));
			//listParam.Add(new KeyValuePair<string, string>("idutente", idLido.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<UserWebServiceDTO> SetListinoPrezzi(string apikey, InfoLidoDTO mInfoLido)
		{
			string url = "vendor/lido/prices";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("cabana_price", mInfoLido.cabana_price.ToString()));
			listParam.Add(new KeyValuePair<string, string>("sun_bed_price", mInfoLido.sun_bed_price.ToString()));
			listParam.Add(new KeyValuePair<string, string>("umbrella_price", mInfoLido.umbrella_price.ToString()));
			listParam.Add(new KeyValuePair<string, string>("chair_price", mInfoLido.chair_price.ToString()));
			listParam.Add(new KeyValuePair<string, string>("cabana_note", mInfoLido.cabana_note));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<UserWebServiceDTO> SetMailPaypal(string apikey, string email, InfoLidoDTO mInfoLido)
		{
			string url = "vendor/lido/paypal";

			//long idLido = GetIdLido(email, apikey);

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("email_paypal", mInfoLido.email_paypal));
			//listParam.Add(new KeyValuePair<string, string>("id_lido", idLido.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<UserWebServiceDTO> SetServiziLido(string apikey, string email, InfoLidoDTO mInfoLido)
		{
			string url = "vendor/lido/services";

			//long idLido = GetIdLido(email, apikey);

			//PulisciServizi(apikey, email);

			var json = JsonConvert.SerializeObject(new { lido_service_array = (from x in mInfoLido.lido_service_array select int.Parse(x.IdServizio)).ToList() });

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequestJson(ConnectionHelper.WebServiceCallType.Post, json, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<UserWebServiceDTO> SetFileLido(string apikey, string email, InfoLidoDTO mInfoLido)
		{
			string url = "vendor/lido-setup";

			//long idLido = GetIdLido(email, apikey);

			var json = JsonConvert.SerializeObject(new
			{
				cabana_qty = mInfoLido.cabana_qty,
				//cabana_note = mInfoLido.cabana_note,
				lido_zone_array = (from x in mInfoLido.lido_zone_array
								   select new
								   {
									   name = x.IdFila,
									   x.umbrella_qty,
									   x.sun_bed_qty,
									   x.chair_qty
								   }).ToList()
			});

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequestJson(ConnectionHelper.WebServiceCallType.Post, json, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }

		}

		public InfoLidoDTO GetInfoLido(string email, string apikey)
		{
			string url = "vendor/lido-setup";
			//long idLido = GetIdLido(email, apikey);

			//var listParam = new List<KeyValuePair<string, string>>();
			//listParam.Add(new KeyValuePair<string, string>("id_lido", idLido.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Get, null, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result).data;
            }
            catch (Exception ex)
            {
                return null;
            }

			//if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
			//{
			//	var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
			//	result = new InfoLidoDTO(JsonConvert.DeserializeObject<InfoLidoWebServiceDTO>(ctn.Result));

			//	var listZone = GetZonelido(email, apikey);

			//	result.lido_zone_array = new List<InfoFilaDTO>();

			//	if (listZone != null && listZone.Count > 0)
			//	{
			//		result.cabana_qty = listZone.First().cabana_qty;
			//	}

			//	foreach (var zona in listZone)
			//	{
			//		result.lido_zone_array.Add(new InfoFilaDTO
			//		{
			//			sun_bed_qty = zona.sun_bed_qty,
			//			umbrella_qty = zona.umbrella_qty,
			//			chair_qty = zona.chair_qty,
			//			NomeFila = (listZone.Count == 1 ? "Zona Unica" : "Fila " + zona.zone_id.ToString())
			//		});
			//	}

			//	return result;
			//}
			//else
			//{
			//	return null;
			//}
		}

		public WebServiceResponseDTO<IList<DisponibilitaDTO>> GetDisponibilitaLido(string apikey, string email, DateTime startdate, DateTime enddate)
		{
			string url = "vendor/availability";
			//long idLido = GetIdLido(email, apikey);

			var listParam = new List<KeyValuePair<string, string>>();
			//listParam.Add(new KeyValuePair<string, string>("id_lido", idLido.ToString()));
			listParam.Add(new KeyValuePair<string, string>("start_date", startdate.ToString("yyyyMMdd")));
			listParam.Add(new KeyValuePair<string, string>("end_date", enddate.ToString("yyyyMMdd")));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<IList<DisponibilitaDTO>>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<IList<DisponibilitaDTO>>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<object> AggiornaDisponibilitaLido(string apikey, string email, DisponibilitaDTO disp, int firstzone = 0)
		{
			string url = "vendor/booking/insert-fake";
			//long idLido = GetIdLido(email, apikey);
			//long idUtenteLido = GetIdUtenteLido(apikey);

			var dispToParse = (disp.lido_zone_availability_array.Count > 0 ? (new
			{
				booking_array = (from x in disp.lido_zone_availability_array
								 select new
								 {
									 //id_lido = (int)idLido,
									 //id_utente = (int)idUtenteLido,
									 zone_id = x.lido_zone.id,
									 umbrella_qty = x.umbrella_availability,
									 sun_bed_qty = x.sun_bed_availability,
									 cabana_qty = (disp.lido_zone_availability_array.IndexOf(x) == 0 ? disp.cabana_availability : 0),
									 chair_qty = x.chair_availability,
									 date = x.start_date
								 }).ToList()
			}) : (new
			{
				booking_array = (new[] {
				new
				{
					//id_lido = (int)idLido,
					//id_utente = (int)idUtenteLido,
					zone_id = firstzone,
					umbrella_qty = 0,
					sun_bed_qty = 0,
					cabana_qty = disp.cabana_availability,
					chair_qty = 0,
					date = disp.start_date
				}
				}).ToList()
			}));

			var json = JsonConvert.SerializeObject(dispToParse);

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequestJson(ConnectionHelper.WebServiceCallType.Put, json, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<object>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<object>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<InfoLidoDTO> SetImageLido(string apikey, string mail, Stream imagestream)
		{
			string url = "vendor/upload-photo";

			//long idLido = GetIdLido(email, apikey);

			//var listParam = new List<KeyValuePair<string, string>>();
			//listParam.Add(new KeyValuePair<string, string>("id_lido", idLido.ToString()));
			//listParam.Add(new KeyValuePair<string, string>("ncabine", mInfoLido.NCabine.ToString()));

			//var bytes = new byte[imagestream.Length];
			//using (var memoryStream = new MemoryStream())
			//{
			//	imagestream.CopyTo(memoryStream);
			//	bytes = memoryStream.ToArray();
			//}

			//var stream = imagestream;
			//var bytes = new byte[stream.Length];
			//stream.WriteAsync(bytes, 0, (int)stream.Length);
			//string base64 = System.Convert.ToBase64String(bytes);

			//var listParam = new List<KeyValuePair<string, string>>();
			//listParam.Add(new KeyValuePair<string, string>("data", "image/jpeg;base64," + base64 ));

			//var json = JsonConvert.SerializeObject(new { data = base64 });

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequestImage(imagestream, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<InfoLidoDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

        public WebServiceResponseDTO<BookingDTO> CheckBooking(string apiKey, int bookingId)
        {
			string url = "vendor/check-booking";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("booking_id", bookingId.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<BookingDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<BookingDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
        }

		public WebServiceResponseDTO<IList<BookingDTO>> GetLidoBookings(string apiKey)
		{
			string url = "vendor/lido/bookings";

			var listParam = new List<KeyValuePair<string, string>>();

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Get, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<IList<BookingDTO>>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<IList<BookingDTO>>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<InfoLidoDTO> DeleteLastLidoZone(string apiKey)
		{
			string url = "vendor/lido/delete-last-lido-zone";

			var listParam = new List<KeyValuePair<string, string>>();

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Get, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<InfoLidoDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<object> UserPasswordReset(string email)
		{
			string url = "user-password-reset";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("email", email));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, string.Empty);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<object>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<object>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<UserWebServiceDTO> UserPasswordChange(string apikey, string oldPass, string newPass)
		{
			string url = "user/change-password";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("old_password", oldPass));
			listParam.Add(new KeyValuePair<string, string>("new_password", newPass));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apikey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }

		}

		public WebServiceResponseDTO<UserWebServiceDTO> UpdateUserInfo(string apiKey, UserWebServiceDTO userInfo)
		{
			string url = "user/info";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("name", userInfo.name));
			listParam.Add(new KeyValuePair<string, string>("surname", userInfo.surname));
			listParam.Add(new KeyValuePair<string, string>("email", userInfo.email));
			listParam.Add(new KeyValuePair<string, string>("telephone", userInfo.telephone));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<UserWebServiceDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<InfoLidoDTO> UploadGalleryImage(string apiKey, Stream imageStream)
		{ 
			string url = "vendor/lido/add-photo-to-gallery";

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequestImage(imageStream, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<InfoLidoDTO>(){ error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}

		public WebServiceResponseDTO<InfoLidoDTO> RemoveGalleryImage(string apiKey, int idImage)
		{
			string url = "vendor/lido/remove-from-gallery";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("image_id", idImage.ToString()));

            try
            {
				Task<HttpResponseMessage> response = null;
				response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url, apiKey);

				var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebServiceResponseDTO<InfoLidoDTO>>(ctn.Result);
            }
            catch (Exception ex)
            {
                return new WebServiceResponseDTO<InfoLidoDTO>() { error = true, message = ConnectionHelper.ConnectionErrorString, data = null };
            }
		}
	}
}
