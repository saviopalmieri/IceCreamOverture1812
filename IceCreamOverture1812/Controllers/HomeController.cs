using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocoVendorApp;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamOverture1812.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistrateUser(int count)
        {
            count = count + 1;

			var mail = "scarethisvengeance" + count.ToString() + "@mail.com";
			var password = "scarethisvengeance" + count.ToString();

            try
            {
				var result = RegistrationDAO.Instance.RegisterUser(mail, password);

                if (result != null && !result.error)
                {
                    var response = RegistrationDAO.Instance.LoginUser(mail, password);

                    if (response != null && !result.error)
                    {
                        Random rnd = new Random();

                        var finalresult = InfoLidoDAO.Instance.SetupCompletoLido(response.data.api_key, mail, new InfoLidoDTO()
                        {
                            name = "scarethisvengeance" + count,
                            email_paypal = mail,
                            address = "AAAAAAAA",
                            city = "AAAAAAA",
                            lat = (rnd.NextDouble() * (44.799769 - 40.114222) + 40.114222).ToString(),
                            lng = (rnd.NextDouble() * (15.096095 - 11.617012) + 11.617012).ToString(),
                            cabana_price = 1,
                            umbrella_price = 1,
                            sun_bed_price = 1,
                            chair_price = 1,
                            lido_zone_array = new List<InfoFilaDTO>{new InfoFilaDTO(){ name = 1, chair_qty = count, sun_bed_qty = 1, umbrella_qty = 1 }}
                        });

                        if (finalresult != null && !finalresult.error)
                        {
                            var finalresultImage = InfoLidoDAO.Instance.SetImageLido(response.data.api_key,
                                                                                     mail,
                                                                                     System.IO.File.OpenRead(Url.Content("~/images/v_for_vendetta_mask.png")));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Content("ok");
        }

        public IActionResult FatalityAction()
        {
            try
            {
                var result = RegistrationDAO.Instance.RegisterUserClient("medusa@medusa.it", "fatality", "Ragnarock", "Ragnarock");
            }
            catch (Exception ex)
            {
                return Content("failed");
            }
            return Content("ok");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
